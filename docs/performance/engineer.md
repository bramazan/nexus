# Software Engineer — Performans Değerlendirme El Kitabı
**Versiyon:** 2.0
**Son Güncelleme:** 2026-02
**Kapsam:** Tüm Software Engineer rolleri (Backend, Frontend, Fullstack, Mobile)

---

## Felsefe

Bu el kitabı, mühendisleri **sıralamak** için değil, **büyümelerini hızlandırmak**
için tasarlandı. Buradaki her metrik bir yargı aracı değil, bir **konuşma
başlatıcıdır**.

Bir metrik tek başına hiçbir şey söylemez. Bağlamı olmayan veri, gürültüdür.

---

## Metrik Katmanları

72 metriklik envanter organizasyonel sağlığı izlemek içindir. Performans
değerlendirmesinde ise **3 katmanlı** bir yapı kullanırız:

### 🎯 L1 — North Star Metrikleri (Her 1:1'de konuşulur)

Bu metrikler **tüm mühendisler** için ortaktır. Disiplin fark etmez.

| # | Metrik | Neden Önemli | Sağlıklı Aralık |
|---|--------|-------------|-----------------|
| 1 | **Cycle Time** | Bir işin başlangıçtan prod'a kadar geçen süre. Sistemik engellerin en net göstergesi. | Junior: <48h, Mid: <24h, Senior: <16h, Architect: Takım ortalamasını iyileştirmek |
| 2 | **Change Failure Rate** | Prod'a giden değişikliklerin kaçı sorun yarattı? Kalite ve güvenin göstergesi. | <%5 |
| 3 | **Rework Rate** | 21 gün içinde aynı dosyalara tekrar dokunma oranı. Tasarım kalitesinin proxy'si. | <%8 |
| 4 | **Delivery Impact** | Teslim edilen işin **müşteri/iş sonucu** üzerindeki etkisi. Yönetici + ürün ile birlikte değerlendirilir. | Nitel — aşağıda açıklandı |

> **Delivery Impact Nedir?**
> Bu, sayısal bir metrik değil, bir **değerlendirme sorusudur**:
> *"Bu ay teslim ettiğin işlerden hangisi müşteriye veya iş sonucuna
> en çok etki etti? Bu etkiyi nasıl biliyorsun?"*
>
> Beklentiler seviyeye göre değişir:
> - **Junior:** "Verilen task'ı doğru ve zamanında teslim ettim"
> - **Mid:** "Alternatif yaklaşımları değerlendirip en uygununu seçtim"
> - **Senior:** "Teknik kararımın iş metriğine etkisini ölçebildim"
> - **Architect:** "Birden fazla takımı etkileyen bir teknik strateji belirledim"

---

### 🔍 L2 — Gelişim Metrikleri (Çeyreklik görüşmede derinlemesine bakılır)

Bu metrikler, L1'deki bir trendin **neden** o yönde gittiğini anlamak için
kullanılır. Tek başlarına değerlendirilmez, her zaman bağlamla birlikte okunur.

| # | Metrik | Ne Zaman Bakılır |
|---|--------|-----------------|
| 5 | **PR Size (Median LOC)** | Cycle Time yüksekse → PR'lar çok mu büyük? |
| 6 | **Review Responsiveness** | Başkalarının PR'larına ilk anlamlı yanıt süresi. Takım çarpan etkisinin göstergesi. |
| 7 | **Focus Time Oranı** | 2+ saat kesintisiz blokların toplam çalışma süresine oranı. |
| 8 | **Planning Accuracy** | Sprint taahhüt vs tamamlama oranı. ±%20 bandı sağlıklı. |
| 9 | **Test Coverage Delta** | Mutlak coverage değil, **dokunulan kodun** test edilme oranı. |
| 10 | **Deployment Frequency** | Bireyin prod'a gönderme sıklığı. Küçük batch'ler = düşük risk. |
| 11 | **Documentation Contribution** | Yazılan/güncellenen teknik doküman sayısı. Bilgi paylaşım kültürü. |
| 12 | **AI Asistan Etkinliği** | AI ile üretilen kodun kabul/red oranı ve rework'e dönüşme yüzdesi. (⚠️ Beta) |

---

### 🔬 L3 — Teşhis Metrikleri

Envanterdeki 72 metriğin geri kalanı bu katmandadır. Bir L1 veya L2 metriğinde
anomali görüldüğünde **root cause analizi** için kullanılır. Performans
değerlendirmesinde **asla** doğrudan referans verilmez.

Örnekler:
- MTTR, Hotfix Rate → Change Failure Rate kötüleştiyse
- Meeting Load, Context Switch Count → Focus Time düştüyse
- Dependency Wait Time → Cycle Time açıklanamıyorsa
- Unreviewed PR Age → Review Responsiveness sorunluysa

---

## Kariyer Seviyesi Bağlantısı

Her metrik, her seviyede **aynı şeyi ifade etmez**:

| Metrik | Junior | Mid | Senior | Architect |
|--------|--------|-----|--------|-----------|
| Cycle Time | Kendi task'larını hızlı bitirmek | Bağımlılıkları proaktif yönetmek | Takımın cycle time'ını düşürmek | Organizasyonel darboğazları kaldırmak |
| Change Failure Rate | Testleri yazmak, review feedback'i uygulamak | Edge case'leri öngörmek | Review'da kalite bariyeri olmak | Test stratejisi ve CI/CD iyileştirmeleri |
| Rework Rate | Spec'i doğru anlamak, soru sormak | Tasarımı ilk seferde doğru yapmak | Takıma tasarım rehberliği vermek | Mimari kararlarla rework'ü sistemik azaltmak |
| Delivery Impact | Task tamamlama | Doğru çözümü seçme | İş sonucu ile teknik kararı bağlama | Çoklu takım etkisi, strateji belirleme |

> **Terfi değerlendirmesi** bu tablodaki "bir üst seviyenin beklentilerini tutarlı
> olarak karşılama" ile ilişkilendirilir. Tek bir metrik terfi kararı vermez.
> Kariyer konuşması **sadece çeyreklik görüşmede** yapılır.

---

## Değerlendirme Takvimi

2 haftalık sprint döngüsüne göre yapılandırılmıştır:
Sprint 1 ✍️ check-in
Sprint 2 ✍️ check-in + 💬 1:1 (30dk)
Sprint 3 ✍️ check-in
Sprint 4 ✍️ check-in + 💬 1:1 (30dk)
Sprint 5 ✍️ check-in
Sprint 6 ✍️ check-in + 💬 1:1 (30dk) + 📊 Çeyreklik (60dk)


| Ritüel | Sıklık | Süre | Format | Odak |
|--------|--------|------|--------|------|
| **Sprint Check-in** | Her sprint sonu | 5 dk yazma | Confluence (yazılı) | Nabız, engel, akış |
| **1:1 Görüşme** | Her 2 sprint (~ayda 1) | 30 dk | Yüz yüze/online | L1 metrikler, engel kaldırma |
| **Çeyreklik Değerlendirme** | Her 6 sprint (~3 ayda 1) | 60 dk | Yüz yüze/online | Trend, kariyer, Delivery Impact |

---

### ✍️ Sprint Sonu Check-in

**Nedir:** Her sprint sonunda mühendisin Confluence'a yazdığı kısa not.
**Nerede:** Confluence → [Mühendis Adı] → Check-ins sayfası (sadece mühendis + yönetici erişir)
**Format:** Tabloya yeni satır eklenir.

**4 Soru:**
Bu sprintte tamamladığım en önemli iş:
Beni yavaşlatan veya engelleyen şey (varsa):
Gelecek sprint odağım:
Yöneticimden ihtiyacım olan şey (varsa):


**Kurallar:**
- Mühendis sprint son günü yazar (~5 dk)
- Yönetici okur (~2 dk), engel varsa **48 saat** içinde aksiyon alır
- Engel yoksa kısa yanıt veya emoji yeterli (✅, 👍)
- 2. ve 4. madde bazı sprintlerde boş olabilir — bu normaldir
- Check-in'ler **performans kanıtı olarak kullanılamaz**
- Yazmayan mühendise hatırlatma yapılır, zorlama yapılmaz

---

### 💬 Aylık 1:1 Görüşme (30 dakika)

**Sıklık:** Her 2 sprint sonu (~ayda 1)
**Süre:** 30 dakika — uzatılmaz
**İptal edilemez.** Çakışma varsa aynı hafta içinde yeniden planlanır.

#### Görüşme Öncesi Hazırlık

**Mühendis hazırlar (görüşmeden 1 gün önce):**
- [ ] Bu ay en çok gurur duyduğu iş ve **neden**
- [ ] En çok zorlandığı konu
- [ ] L1 metriklerindeki trendleri kendi yorumuyla
- [ ] Yöneticiden/organizasyondan talepleri

**Yönetici hazırlar:**
- [ ] L1 metrik trendleri (takım ortalaması ile karşılaştırmalı, anonim)
- [ ] Son 2 check-in'den biriken sinyal/pattern
- [ ] 2-3 cümlelik bağlam hipotezi

#### 30 Dakika Yapısı
[05 dk] Mühendis anlatır — "Bu ay nasıl geçti?"
(Yönetici dinler, not alır, sözünü kesmez)

[10 dk] Birlikte L1 veri okuma
Dashboard'a bakılır, trendler yorumlanır
Mühendis önce kendi yorumunu söyler
Yönetici hipotezini paylaşır

[10 dk] Engel kaldırma + takım katkısı geri bildirimi
"Seni yavaşlatan ne var? Benden ne lazım?"
"Bu ay takımdan sana gelen geri bildirim var mı?
Senin başkasına verdiğin katkı?"
Geçen ayın aksiyonlarının takibi

[05 dk] Karşılıklı aksiyon belirleme
Mühendis: 1 taahhüt
Yönetici: 1 taahhüt
İkisi de somut ve deadline'lı


#### 30 Dakikada Ne Konuşulmaz
- Kariyer planlaması (→ çeyreklik görüşmeye)
- Detaylı teknik tartışma (→ ayrı toplantı veya async)
- L2/L3 metriklere dalma (→ çeyreklik veya async)

#### Kritik Kurallar

1. **Sürpriz yasaktır.** Dashboard her zaman açık, mühendis verisini her zaman görür.
2. **Sayı ≠ yargı.** "Cycle time'ın 38 saat" gözlemdir. "Yavaşsın" yargıdır.
3. **Koçluk sorusu > tavsiye.** "Bence şunu yap" yerine "Ne denemek istersin?"
4. **Geçen ayın aksiyonu takip edilir.** Yapılamadıysa neden konuşulur, suçlama yapılmaz.

---

### 📊 Çeyreklik Değerlendirme Görüşmesi (60 dakika)

**Sıklık:** Her 6 sprint (~3 ayda 1)
**Süre:** 60 dakika

#### Zamanlama
Sprint 1-5: Normal akış (check-in + 1:1)
Sprint 6: Mühendis öz-değerlendirme yazar
Yönetici çeyreklik özet hazırlar
60 dk gelişim görüşmesi yapılır


#### Mühendis Öz-Değerlendirmesi
Bu çeyrekte en etkili 1-3 işim:
(Her biri için: Ne yaptım? Kime/neye etki etti? Bunu nasıl biliyorum?)
Bu çeyrekte en çok öğrendiğim şey:
L1 metrik trendlerimi nasıl yorumluyorum:
Cycle Time:
Change Failure Rate:
Rework Rate:
Takıma katkım (review, mentorluk, doküman, bilgi paylaşımı):
Kariyer hedefim ve bu çeyrek bu hedefe ne kadar yaklaştım:
Gelecek çeyrek odaklanmak istediğim 1-2 alan:
Yöneticimden/organizasyondan beklentilerim:

#### Yöneticinin Çeyreklik Özeti

Yönetici görüşme öncesi yazılı olarak hazırlar:
- Genel değerlendirme (3-5 cümle)
- Öne çıkan güçlü yanlar
- Gelişim fırsatları
- L1 trend yorumu (13 haftalık)
- Check-in'lerden ve 1:1'lerden biriken gözlemler
- Diğer mühendislerden gelen geri bildirimler (takım katkısı)

#### 60 Dakika Yapısı
[10 dk] Mühendis öz-değerlendirmesini özetler

[15 dk] Delivery Impact tartışması
"En etkili işin hangisiydi? Etkisini nasıl biliyorsun?"
Yönetici kendi gözlemlerini ve çeyreklik özetini paylaşır

[10 dk] L1 trend analizi (13 haftalık)
Birlikte bakılır
Gerekirse L2'den 2-3 metriğe seçici dalış

[10 dk] Takım katkısı geri bildirimi
Yöneticinin diğer ekip üyelerinden duyduğu geri bildirimler
Mühendis kendi katkı değerlendirmesini paylaşır

[10 dk] Kariyer konuşması
"Seviye beklentilerinin neresindeyiz?"
Maksimum 2 gelişim alanı belirlenir

[05 dk] Karşılıklı taahhütler
Karşılıklı Anlaşma Dokümanı imzalanır


#### L2 Metriklere Ne Zaman Bakılır

L2'nin tamamı taranmaz. Sadece L1 sinyallerinin yönlendirdiği 2-3 metriğe bakılır:

| L1 Sinyali | Bakılacak L2 |
|------------|-------------|
| Cycle Time yükseliyor | PR Size, Focus Time |
| Change Failure Rate arttı | Test Coverage Delta, Review kalitesi |
| Rework Rate yüksek | Planning Accuracy |
| Delivery Impact düşük | Deployment Frequency, Documentation |

---

## Özel Durumlar

### Onboarding
- İlk 4 hafta: Check-in başlar ama **metrik konuşulmaz** — odak: adaptasyon
- Hafta 5-12: 1:1 başlar, metrikler **kalibrasyon dönemi** olarak gösterilir
- Hafta 13+: Normal takvim

### Takım Değişikliği
- İlk 4-6 hafta kalibrasyon dönemi. Eski takımdaki metrikler taşınmaz.

### Performans Sorunu Sinyali
- L1 metriklerde **3 ay üst üste** kötüleşen trend + aksiyonlar sonuç vermiyorsa:
  - 1:1 geçici olarak **2 haftada 1** olarak sıklaştırılır
  - Amaç: Daha sık destek. Cezalandırma DEĞİL.
  - Mühendise neden açıkça söylenir
  - Maksimum 2 ay, sonra normale döner ya da formal süreç başlar

---

## Gaming / Anti-Pattern Farkındalığı

| Oynanabilir Metrik | Nasıl Oynanır | Nasıl Tespit Edilir |
|-------------------|---------------|-------------------|
| Cycle Time | Kolay task seçmek | Complexity vs cycle time korelasyonu |
| PR Size | Mantıksız parçalama | İlişkili PR sayısı ve merge sırası |
| Deployment Freq | Config change'i deploy saymak | Meaningful change oranı |
| Rework Rate | Farklı dosyalarda quick fix | Hotfix rate çapraz kontrol |
| Review Speed | Rubber-stamp review | Yorum sayısı, suggestion oranı |
| Test Coverage | Anlamsız test | Mutation testing skoru |

> **Bir metrik oynanmaya başladıysa, metrik yanlıştır, insan değil.**
> Sistemi düzeltin, insanı suçlamayın.

---

## AI Asistan Kullanımı Rehberi (⚠️ Beta)

### Beklentiler
- AI ile üretilen **her kod bloğu** insan tarafından anlaşılmış ve review
  edilmiş olmalıdır
- AI kullanımı bir beceridir, utanılacak bir şey değildir
- AI'ın önerdiği ama **reddedilen** kod da değerlidir — kritik düşünme göstergesi

### Beta Metrikler (L2 — henüz karar amaçlı kullanılmaz)

| Metrik | Açıklama | İzleme Amacı |
|--------|----------|-------------|
| AI Code Acceptance Rate | Kabul/red oranı | Etkili kullanım kalıbı |
| AI-Assisted Rework Rate | AI kodunun 21 gün içinde değiştirilmesi | Kalite etkisi |
| AI Context Quality | Prompt kalitesi (nitel, öz-değerlendirme) | Beceri gelişimi |

---

## Psikolojik Güvenlik Göstergeleri

### Sağlıklı Sinyaller ✅
- Mühendis kendi hatasını **proaktif** olarak paylaşıyor
- "Bilmiyorum" diyebiliyor
- Blameless postmortem'lere aktif katılım
- Metrik verisine itiraz edebiliyor
- Check-in'de engel yazmaktan çekinmiyor

### Uyarı Sinyalleri 🔴
- Sadece "güvenli" task seçme eğilimi
- 1:1'de tek kelimelik yanıtlar
- Check-in'lerde her sprint aynı/boş yanıt
- Review'larda sessiz kalma
- Metrik tartışmasında savunmacı tavır

> Yönetici bu sinyalleri **yargılamak** için değil, **ortamı iyileştirmek** için
> kullanır.

---

## Sık Sorulan Sorular

**S: Farklı disiplinler aynı metriklerle mi değerlendirilir?**
C: Evet. L1 metrikleri evrenseldir. Disipline özel derinlik, Delivery Impact
hikayesinde ve kariyer beklentilerinde ortaya çıkar.

**S: Bir metriğim kötüyse ne olur?**
C: Kötü bir metrik bir konuşma başlatıcıdır, yargı değil. Birlikte root cause
arar, aksiyon birlikte belirlenir.

**S: AI metriklerini kullanmıyorum, dezavantajlı mıyım?**
C: Hayır. AI metrikleri Beta'dadır ve karar amaçlı kullanılmaz.

**S: Takım değiştirdim, metriklerim sıfırlanır mı?**
C: Eski trendler bağlamsız taşınmaz. İlk 4-6 hafta kalibrasyon dönemidir.

**S: Metriğe itiraz edebilir miyim?**
C: Kesinlikle. "Bu sayı bağlamı yansıtmıyor" demek beklentimizdir.

**S: 1:1'im iptal edilirse?**
C: 1:1 iptal edilemez. Aynı hafta içinde yeniden planlanır. Tekrarlayan
iptaller governance ihlalidir.

**S: Çeyreklik görüşmede sürpriz kötü haber alır mıyım?**
C: Hayır. Çeyreklikteki her şey aylık 1:1'lerde zaten konuşulmuş olmalı.
Sürpriz, sistemin başarısızlığıdır.

**S: Check-in yazmayı unutursam?**
C: Hatırlatma yapılır, zorlama yapılmaz. Tekrarlarsa 1:1'de format
uygunluğu sorulur.
