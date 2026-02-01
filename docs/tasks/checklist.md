Faz 1: Altyapı ve Bağlantıların Kurulması (Entegrasyon Katmanı)
Sistemin dış dünya ile konuşabilmesi için ilk doldurulması gereken tablolardır.
- [x] 1. Workspace (Çalışma Alanı) Tanımı Her şeyin çatısıdır.
• Tablo: workspaces
• Girilmesi Gereken Veri:
    ◦ name: Şirket veya Organizasyon adı (Örn: "Acme Corp Engineering").
    ◦ Not: Tüm diğer veriler bu ID'ye bağlanacaktır.
- [x] 2. Entegrasyonların Tanımlanması (Integrations) GitLab, Jira, Instana gibi araçların tanımlandığı aşamadır.
• Tablo: integrations
• Girilmesi Gereken Veri:
    ◦ workspace_id: Adım 1'de oluşturulan ID.
    ◦ type: Entegrasyon tipi (Örn: gitlab, jira, instana, sonarqube).
    ◦ config (JSON): Burası en kritik kısımdır. Şifreli kimlik bilgilerini içerir.
        ▪ GitLab Örneği: {"base_url": "https://gitlab.com", "api_token": "glpat-xxxx", "api_version": "v4"}
        ▪ Jira Örneği: {"url": "https://company.atlassian.net", "username": "bot@company.com", "api_token": "ATATT-xxxx"}
• Sonuç: Bu adım tamamlandığında sistem raw_events tablosuna ham veri çekmeye başlayabilir.
Faz 2: Organizasyon ve Kimlik Eşleştirme (İnsan Katmanı)
Veriyi çektikten sonra "Bu kodu kim yazdı?" veya "Bu görevi kim yapıyor?" sorularını yanıtlamak için "kimlik haritalaması" yapmanız gerekir.
- [x] 3. Kullanıcıların Oluşturulması (Users) Şirketteki gerçek kişilerin tanımlarıdır.
• Tablo: users
• Girilmesi Gereken Veri:
    ◦ full_name: Gerçek İsim Soyisim (Örn: "Ahmet Yılmaz").
    ◦ email: Şirket e-posta adresi.
- [x] 4. Takım Hiyerarşisinin Kurulması (Teams) Mühendislik organizasyon şemasıdır.
• Tablo: teams
• Girilmesi Gereken Veri:
    ◦ name: Takım adı (Örn: "Backend", "Frontend", "Platform").
    ◦ parent_team_id: Varsa üst takım (Örn: "Backend" takımı "Product Engineering"e bağlı olabilir). Bu, yukarıya doğru raporlama (roll-up) yapmanızı sağlar.
- [x] 5. Takım Üyeliği (Team Members) Kişilerin takımlara atanması.
• Tablo: team_members
• Girilmesi Gereken Veri:
    ◦ user_id ve team_id eşleşmesi.
    ◦ role: Rolü (Örn: "Lead", "Developer", "Product Owner").
    ◦ Önemli: Bir kişi birden fazla takımda olabilir veya tarihçeli olarak takım değiştirmiş olabilir.
- [x] 6. Araç Hesaplarının Eşleştirilmesi (Tool Accounts) - KRİTİK ADIM Sistemin çalışması için en önemli manuel veya yarı-otomatik adımdır. Ahmet Yılmaz'ın GitHub'daki "ayilmaz88" olduğunu sisteme tanıtmanız gerekir.
• Tablo: tool_accounts
• Girilmesi Gereken Veri:
    ◦ user_id: Gerçek kişi ID'si.
    ◦ integration_id: Hangi entegrasyon olduğu (Örn: GitLab entegrasyon ID'si).
    ◦ external_id / username: Dış sistemdeki kullanıcı adı veya ID'si (Örn: GitHub username ayilmaz88 veya Jira accountId 557058:xyz).
    ◦ external_email / metadata: Commit eşleştirmesi için email ve ek veriler (Avatar, Profile URL).
• Neden Gerekli? Bu eşleştirme olmazsa DORA metrikleri hesaplanamaz, çünkü commit'lerin kime ait olduğu bilinemez.
Faz 3: Proje ve Servis Mimarisi (Varlık Katmanı)
Kodun ve işin nerede yaşadığını tanımlarsınız.
- [x] 7. Depoların (Repositories) Tanımlanması Genellikle entegrasyon sonrası otomatik çekilir (raw_events üzerinden), ancak hangilerinin takip edileceği seçilmelidir.
• Tablo: repositories
• Otomatik Dolar: name, url, default_branch GitLab/GitHub'dan gelir.
- [x] 8. Servis Kataloğu (Service Catalog) DORA metriklerinin (Deployment Frequency, MTTR) hesaplandığı temel birimdir. Repo ile Servis her zaman 1:1 olmayabilir (Monorepo durumu).
• Tablo: services
• Girilmesi Gereken Veri:
    ◦ name: Servis adı (Örn: "Payment API").
    ◦ owner_team_id: Bu servisten sorumlu takım.
    ◦ tier: Önem derecesi (Tier-1, Tier-2).
• Bağlantı Tablosu: service_repositories. Hangi servisin hangi repo(lar)da yaşadığını ve path filtrelerini (monorepo için packages/payment/** gibi) burada tanımlarsınız.
- [x] 9. Jira Proje/Board Tanımları
• Tablo: issues ve ilgili bağlantılar.
• Entegrasyon ayarlarında hangi Jira projelerinin (Key: PROJ, ENG) çekileceğini belirtirsiniz. Sistem bu projelerdeki Issue, Sprint ve Epic'leri otomatik olarak issues ve sprints tablolarına doldurur.
Faz 4: Zeka ve Kural Motoru (Konfigürasyon Katmanı)
Sistemin "İyi" veya "Kötü"yü ayırt etmesi için gereken kurallardır.
- [x] 10. Metrik Eşik Değerleri (Metric Thresholds) Sisteme hedeflerinizi öğretirsiniz.
• Tablo: metric_thresholds (Data modelinde belirtilen kural motoru).
• Girilmesi Gereken Veri:
    ◦ metric_name: Örn. "cycle_time".
    ◦ segment: Örn. "enterprise" veya "startup".
    ◦ min_value / max_value: Hangi aralığın "Elite", "High" veya "Low" olduğunu belirten sayısal değerler (Örn: Cycle Time < 24 saat = Elite).
- [ ] 11. Kullanıcı Uygunluğu (User Availability) Metriklerin sapmaması için izin/tatil günleri girilmelidir.
• Tablo: user_availability
• Girilmesi Gereken Veri: Kullanıcı ID'si, başlangıç/bitiş tarihi ve tipi (Yıllık İzin, Raporlu vb.). Bu veriler, Cycle Time hesaplanırken "çalışılmayan günleri" düşmek için kullanılır.
Özet: Çalıştırma Sıralaması
- [x] 1. Workspaces & Integrations: GitLab/Jira API Key'lerini integrations tablosuna gir.
- [x] 2. Raw Data Sync (Otomatik): Sistem raw_events tablosunu doldurmaya başlar.
- [x] 3. Users & Teams: İK yapısını teams ve users tablolarına işle.
- [x] 4. Tool Accounts: tool_accounts tablosunda "Jira User X = Nexus User Y" eşleşmesini yap.
- [x] 5. Services: Hangi repoların hangi servise (services) ait olduğunu ve sahiplerini tanımla.
- [x] 6. Thresholds: metric_thresholds tablosuna başarı kriterlerini (KPI) gir.
Bu adımlardan sonra sistem pull_requests, deployments ve issues tablolarını raw_events üzerinden işleyerek (ETL) dolduracak ve dashboardlarınız canlanacaktır.
DORA metriklerini veri modelinde nasıl hesaplarız?
Yapay zeka kaynaklı teknik borçlar nasıl takip edilmeli?
Tool Accounts eşleştirmesi otomatik hale getirilebilir mi?
jirada hangi board hangi takıma ait olduğunu nerede tutuyoruz?

Locating Jira Board Mapping...
