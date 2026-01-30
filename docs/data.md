# 🗄️ Nexus Veri Modeli ve Mimarisi

Bu doküman, güncellenen `diagram.md` dosyasında tanımlanan veritabanı şemasının detaylı açıklamasını içerir. Model, esnek kural motoru, gelişmiş içgörü yapısı ve gerçek hayat senaryolarını (izin, rapor vb.) kapsayacak şekilde genişletilmiştir.

## 1. Entegrasyon Katmanı ve Veri Girişi
Sistemin dış dünya ile bağlantı noktasıdır. Veriler "Önce Yükle, Sonra Dönüştür" (ELT) prensibiyle işlenir.

*   `integrations`: Hangi araçların (GitHub, Jira, Instana vb.) bağlı olduğunu tutar.
*   `raw_events`: Tüm entegrasyonlardan gelen ham verinin (JSON) saklandığı veri gölü. Geriye dönük analiz için her şeyin orijinal hali burada tutulur.

## 2. Kaynak Kontrolü ve Kod Kalitesi (Source Control)
Kod tabanındaki aktivitelerin derinlemesine analizi için tasarlanmıştır.

*   **`pull_requests`**: Birleştirme süreci, boyut ve AI katkısı gibi temel metriklerin kaynağıdır.
    *   `cycle_time_minutes`: Kodun yazılmasından merge edilmesine kadar geçen hesaplanmış süre.
*   **`pull_request_reviews`**: Kod inceleme sürecinin detaylarını tutar.
    *   **Ne İçin Lazım?** Sadece "onaylandı" bilgisini değil, "Reviewer Load" (inceleyen üzerindeki yük), "Pickup Time" (incelemenin başlaması için geçen süre) ve "Review Time" metriklerini hesaplamak için kullanılır.
*   **`code_changes`**: Commit bazlı dosya değişiklikleri.
    *   `is_refactor` / `is_rework`: Kodun evrimini ve teknik borç ödemelerini takip etmek için heuristic analizle doldurulur.

## 3. Proje Yönetimi ve Akış (Flow & Planning)
İşin sadece sonucunu değil, nasıl aktığını ölçmek için tasarlanmıştır (Flow Efficiency).

*   **`issues` ve `sprints`**: Temel görev ve zaman planlaması.
*   **`issue_events` (Kritik tablo)**: Bir işin statü değişikliklerinin tarihçesini tutar (Örn: Todo -> In Progress -> Blocked).
    *   **Ne İşe Yarar?** "Flow Efficiency" (İşin ne kadar süre aktif yapıldığı vs. beklediği) ve "Dependency Wait" (Bağımlılık bekleme süresi) hesaplamaları buradan yapılır.
*   **`issue_sprint_links`**: İşlerin sprintlerle ilişkisi.
    *   `added_at`: İşin sprinte eklenme zamanı. Eğer bu tarih sprint başlangıcından sonraysa, **"Scope Creep"** (Kapsam Sapması) olarak işaretlenir.

## 4. SRE ve Operasyonel Sağlık (DORA & Reliability)
Sistemin stabilitesi ve olaylara müdahale hızını ölçer.

*   **`deployments`**: Hangi kodun (`commit_id`), hangi ortama (`environment`), ne zaman ve kim tarafından çıktığını tutar. DORA "Deployment Frequency" metriğinin kaynağıdır.
*   **`incidents`**: Kesinti ve olay yönetimi.
    *   `detected_at`: Sistemin (örn. Instana) sorunu fark ettiği an (MTTD hesabı için).
    *   `acknowledged_at`: Bir mühendisin olayı üstlendiği an (MTTA hesabı için).
    *   `end_time` - `start_time`: Olayın toplam süresi (MTTR).

## 5. Kimlik, Takım ve Mimarisi

*   **`services` ve `service_repositories`**: Mikroservis mimarisinde hangi servisin hangi repo(lar)da yaşadığını tanımlar. `path_filter` alanı ile monorepo desteği sağlar.
*   **`users`, `teams`, `team_members`**: Hiyerarşik takım yapısı ve kullanıcı yönetimi.
*   **`tool_accounts`**: Kişilerin farklı araçlardaki (GitHub username, Jira email vb.) kimliklerini tek bir `user_id` altında birleştirir.
*   **`user_availability`**: İzin, rapor ve tatil günlerini tutarak metriklerin (örn. Velocity) daha adil hesaplanmasını sağlar.

## 6. Metrik Tanımları ve İçgörüler

*   **`metric_definitions` & `metric_thresholds`**: Hangi metriğin nasıl hesaplanacağını ve şirketin boyutuna/segmentine göre "İyi/Kötü" eşik değerlerini belirler.
*   **`insights`**: Veri analizi sonucu üretilen uyarılar (Örn: "Cycle Time son 2 haftada %30 arttı"). Polimorfik yapısı sayesinde hem kişilere hem takımlara uyarı üretebilir.
