# Metrik Yönetişim Politikası
**Versiyon:** 1.0
**Son Güncelleme:** 2026-02
**Sonraki Gözden Geçirme:** 2026-05

---

## Amaç

Bu doküman, mühendislik metriklerinin **nasıl toplandığını, kimin görebildiğini,
nasıl kullanıldığını ve nasıl kullanılamayacağını** tanımlar. Amacı, veri odaklı
kültürü korurken **güven ve psikolojik güvenliği** garanti altına almaktır.

---

## Temel İlkeler

### İlke 1: Metrikler Silah Değildir
Metrikler engelleri görünür kılmak, süreçleri iyileştirmek ve büyümeyi desteklemek
içindir. Hiçbir metrik, tek başına veya bağlamsız olarak:
- Disiplin sürecine kanıt olamaz
- Terfi/terfisizlik gerekçesi olamaz

### İlke 2: Bağlam Her Zaman Sayıdan Önce Gelir
Bir Cycle Time grafiği yükselen trend gösteriyorsa, bu "mühendis yavaşladı"
demek DEĞİLDİR. Önce sorulması gereken:
- Sprint kapsamı değişti mi?
- Dış bağımlılık var mı?
- Kişisel durum (izin, sağlık, onboarding) etkili mi?
- Teknik borç mu üstlenildi?

### İlke 3: Şeffaflık Çift Yönlüdür
Yönetici mühendisinin verisini görebiliyorsa, mühendis de **kendi verisini her
zaman** görebilmelidir. Sürpriz değerlendirme yapılmaz.

### İlke 4: Ölçmek Davranışı Değiştirmemeli
Bir metrik, insanların davranışını **doğrudan** etkilediği noktada amacını
kaybeder (Goodhart Yasası). Bu nedenle:
- Metrikler **hedef** olarak sunulmaz, **gösterge** olarak sunulur
- "Cycle Time'ını 20 saatin altına düşür" ❌
- "Cycle Time trendine birlikte bakalım, engelleri konuşalım" ✅

### İlke 5: Değerlendirme Takvimi
Sprint check-in, aylık 1:1 ve çeyreklik gelişim görüşmesi birbirini tamamlar.
Hiçbiri diğerinin yerini almaz, hiçbiri atlanmaz.

---

## Değerlendirme Takvimi Yönetişimi

### Sprint Check-in Kuralları

| Kural | Detay |
|-------|-------|
| Kim yazar | Mühendis |
| Ne zaman | Her sprint son günü |
| Nereye | Confluence → [Mühendis Adı] → Check-ins sayfası |
| Kim okur | Doğrudan yönetici |
| Yanıt süresi | Engel varsa 48 saat içinde aksiyon |
| Engel yoksa | Emoji reaction yeterli (✅ veya 👍) |
| Saklanma süresi | 6 ay, sonra otomatik arşiv |
| Performans kanıtı olarak kullanılabilir mi | **HAYIR** |
| Üst yönetime raporlanır mı | **HAYIR** — sadece yönetici-mühendis arası |
| Yazmayana ne olur | Hatırlatma yapılır, zorlama yapılmaz. Tekrarlarsa 1:1'de format uygunluğu sorulur |

### Aylık 1:1 Kuralları

| Kural | Detay |
|-------|-------|
| Sıklık | Her 2 sprint sonu (~ayda 1), takvimde sabit gün/saat |
| Süre | 30 dakika (uzatılmaz) |
| İptal edilebilir mi | **HAYIR** — çakışma varsa aynı hafta yeniden planlanır |
| Kim hazırlanır | Her iki taraf (görüşmeden en az 1 gün önce) |
| Tutanak yazılır mı | Evet — aksiyon maddeleri zorunlu, detaylı not opsiyonel |
| Tutanağı kim görür | Sadece mühendis ve yönetici |
| Mühendis onayı gerekli mi | Evet — aksiyonlar üzerinde mutabakat |

### Çeyreklik Gelişim Görüşmesi Kuralları

| Kural | Detay |
|-------|-------|
| Sıklık | Her 6 sprint (~3 ayda 1), çeyreğin son 2 haftası |
| Süre | 60 dakika |
| Öz-değerlendirme zorunlu mu | Evet — Sprint 6 başında yazılır |
| Yönetici özeti zorunlu mu | Evet — Sprint 6 başında hazırlanır |
| Kariyer konuşması yapılır mı | Evet — sadece bu görüşmede |
| Karşılıklı Anlaşma Dokümanı | Zorunlu — her iki taraf imzalar |
| EM'ye raporlama | Anonim trend özeti (bireysel isim yok) |

---

## Envanter ve Performans Metrikleri Ayrımı

72 metriklik envanter **organizasyonel sağlığı** izlemek içindir.
Bireysel performans değerlendirmesinde ise engineer.md'de tanımlanan
**3 katmanlı yapı** kullanılır:

| Katman | Amaç | Kullanım | Metrik Sayısı |
|--------|------|----------|:-------------:|
| **L1 — North Star** | Her 1:1'de konuşulur | Bireysel değerlendirme | 4 |
| **L2 — Gelişim** | L1'deki trendin nedenini anlamak | Çeyreklikte seçici dalış | 8 |
| **L3 — Teşhis** | Root cause analizi | Sadece sorun araştırmasında | 60 |

Envanterdeki hedefler (ör. Rework Rate < %3) **organizasyonel hedeflerdir**
(ekip ortalaması). Engineer.md'deki sağlıklı aralıklar (ör. Rework Rate < %8)
**bireysel göstergelerdir**. Bu iki hedef farklı amaçlara hizmet eder ve
karıştırılmamalıdır.

---

## Erişim ve Görünürlük Matrisi

| Veri Türü | Mühendis (Kendi) | Mühendis (Başkası) | Engineering Manager |
|-----------|:---:|:---:|:---:|
| Kendi L1 metrikleri (dashboard) | ✅ Her zaman | ❌ | ✅ |
| Kendi L2 metrikleri | ✅ Her zaman | ❌ | ✅ |
| L3 teşhis metrikleri | ✅ | ❌ | İstek üzerine |
| Takım ortalamaları (anonim) | ✅ | ✅ | ✅ |
| Bireysel trend (isimli) | Sadece kendi | ❌ | Kendi direkt raporları |
| Sprint check-in'ler | ✅ | ❌ | Kendi direkt raporları |
| Aylık 1:1 tutanakları | ✅ | ❌ | N/A (kendisi yazar) |
| Çeyreklik değerlendirme | ✅ | ❌ | ✅ |
| Öz-değerlendirme | ✅ | ❌ | ✅ |
| Psikolojik güvenlik anketi | Anonim | Anonim | Takım sonucu |


## Yasaklanmış Kullanımlar

| # | Yasaklanmış Kullanım | Neden | Derece |
|---|---------------------|-------|--------|
| 1 | Metrikleri Teams/e-mail'de bireysel paylaşmak | Psikolojik güvenliği yıkar | Orta |
| 2 | Mühendisleri sıralamak | Gaming ve toksik rekabet | Ağır |
| 3 | Metrikleri maaş/bonus'a doğrudan bağlamak | Goodhart — metrik amacını kaybeder | Ağır |
| 4 | Tek dönem verisini terfi kararına dayanak göstermek | Trend > anlık | Orta |
| 5 | L3 metrikleri performans görüşmesinde kullanmak | Teşhis amaçlı, değerlendirme değil | Hafif |
| 6 | Beta metrikleri (AI) üzerinden yargılama | Endüstri standardı yok | Orta |
| 7 | Başka takımın metrikleriyle kıyaslama | Bağlam farklılığı | Orta |
| 8 | Sprint check-in'i performans kanıtı olarak kullanmak | Güven kırar, yazmayı durdurur | Ağır |
| 9 | 1:1'i iptal edip bir sonraki aya atmak | Değerlendirme takvimi kutsaldır | Orta |
| 10 | Çeyreklik görüşmede ilk kez olumsuz geri bildirim vermek | Sürpriz yasağı | Ağır |
| 11 | Mühendis öz-değerlendirmesini görmezden gelmek | Çalışan sesini yok sayar | Orta |
| 12 | Dashboard erişimini kısıtlamak veya geciktirmek | Şeffaflık ilkesi ihlali | Ağır |
| 13 | Envanter hedeflerini bireysel performans hedefi olarak sunmak | Org hedefi ≠ bireysel hedef | Orta |

---

## İhlal Mekanizması

### Raporlama

Her mühendis, metrik kötüye kullanımını veya takvim ihlalini **anonim olarak**
raporlayabilir.

**Kanallar:*
- Doğrudan EM'e e-posta
- HR üzerinden anonim bildirim

**Garanti:** Raporlayan kişinin kimliği **hiçbir durumda** ifşa edilmez.

### İnceleme Süreci

1. EM veya görevlendirdiği kişi **7 iş günü** içinde inceler
2. İnceleme kapsamı:
   - İlgili 1:1 tutanakları
   - Dashboard erişim logları
   - Sprint check-in geçmişi (varsa ilgili)
   - İlgili taraflarla gizli görüşme
3. İhlal teyit edilirse yaptırım uygulanır
4. Raporlayan kişiye sonuç **anonim olarak** bildirilir

### Yaptırım Tablosu

| Derece | Tanım | Yaptırım | Örnek |
|--------|-------|---------|-------|
| **Hafif** | İlk kez, kasıtsız, düşük etki | Eğitim + sözlü uyarı | L3 metriği 1:1'de kullanmak |
| **Orta** | Tekrarlayan veya birden fazla kişiyi etkileyen | Yazılı uyarı + mentorluk + 1 çeyrek denetim | 1:1'i 2 ay üst üste iptal, tek dönem verisiyle terfi engelleme |
| **Ağır** | Kasıtlı, sistemik, güven yıkıcı | Rol değişikliği, HR disiplin süreci | Metrikleri bilerek yanlış yorumlamak, manipüle etmek, anonim kimliği ifşa etmek |

### Yönetici Koruması

Yöneticiler de bu sistemin "kullanıcısıdır" ve hata yapabilir. Hafif ihlallerde
amaç **eğitmek ve düzeltmek**, cezalandırmak değildir. Ağır ihlaller ise kasıt
ve güven yıkımı içerir — bunlar tolere edilmez.

---

## Metrik Yaşam Döngüsü

### Yeni Metrik Ekleme

1. Öneri sahibi EM'e RFC ile iletir
2. RFC şablonu:
Metrik Adı:
Hangi katman: L1 / L2 / L3 / Org Envanter
Ne ölçüyor:
Neden gerekli (hangi soruyu cevaplıyor):
Veri kaynağı:
Gaming riski ve önlemi:
Mevcut hangi metrikle örtüşüyor (varsa):
Önerilen sağlıklı aralık:



3. Minimum **2 hafta** tartışma süresi
4. Engineering Leadership Team (ELT) onayı
5. İlk 1 çeyrek **Beta** etiketi — karar amaçlı kullanılamaz
6. Beta döneminde çeyreklik review'da değerlendirilir

### Metrik Sunset (Kaldırma)

Bir metrik sunset adayı olur eğer:
- **3 çeyrek** boyunca hiçbir 1:1 veya retro'da referans verilmemiş
- Veri kalitesi güvenilir değil (>%20 eksik/hatalı)
- Gaming tespit edilmiş ve düzeltilemiyor
- Başka bir metrik tarafından daha iyi temsil ediliyor
- Ölçüm maliyeti, sağladığı değerin üzerinde

Sunset kararı ELT tarafından çeyreklik gözden geçirmede verilir.
Kaldırılan metrikler bu dokümanda **"Sunset Arşivi"** bölümüne taşınır.

### Katman Değişikliği

Bir metrik katman değiştirebilir:
- L2 → L1 terfi: Tutarlı olarak en çok aksiyon üreten L2 metriği, bir L1'in
  yerini alabilir
- L1 → L2 düşürme: Aksiyon üretmeyen veya gaming'e uğrayan L1 metriği
- Beta → L2 mezuniyet: 2 çeyrek başarılı izleme sonrası

Her değişiklik ELT onayı gerektirir ve tüm ekibe duyurulur.

---

## AI Kodunun Yönetişimi

### Kurallar
1. AI kaynaklı kod, insan kodla **aynı kalite standartlarına** tabidir
2. PR'da AI kaynaklı kod varsa belirtmek **teşvik edilir** (zorunlu değil)
3. AI kodunun review'ı için ek checklist:
   - [ ] Kodu anladım ve açıklayabilirim
   - [ ] Edge case'leri düşündüm
   - [ ] Testleri yazdım/doğruladım
   - [ ] Güvenlik taraması geçti
4. `Unverified Code Sources = 0%` CI/CD'de otomatik kontrol edilir [2]

### AI Metrik Yönetişimi
- Tüm AI metrikleri **Beta** statüsündedir [1]
- Performans değerlendirmesinde kullanılamaz
- Takım düzeyinde anonim olarak izlenir
- Bireysel AI kullanım oranı **sadece mühendis kendisi** görebilir
- Çeyreklik metrics review'da Beta statüsü değerlendirilir

---

## Psikolojik Güvenlik Denetimi

### Çeyreklik Anonim Anket (5 soru, 1-5 ölçek)

1. "Aylık 1:1 görüşmelerinde metrik verilerim adil yorumlanıyor"
2. "Hata yaptığımda cezalandırılmayacağımı biliyorum"
3. "Metriklerime itiraz edersem ciddiye alınacağını düşünüyorum"
4. "Yöneticim sayılara değil, bağlama odaklanıyor"
5. "Bu ekipte metrikler bizi geliştirmek için kullanılıyor, yargılamak için değil"

### Eşikler ve Aksiyon

| Ortalama | Durum | Aksiyon |
|----------|-------|--------|
| **≥ 4.0** | Sağlıklı | Sonuçlar ekiple paylaşılır, kutlanır |
| **3.0 – 3.9** | İyileştirme gerekli | ELT gündemine alınır, yönetici ile aksiyon planı |
| **< 3.0** | Acil müdahale | EM doğrudan sahiplenir, 30 gün aksiyon planı |

- Sonuçlar **takım düzeyinde** paylaşılır
- Bireysel yanıtlar **hiçbir zaman** kimseyle paylaşılmaz
- Anket sonuçları trendi çeyreklik olarak izlenir

### Ek Sinyal: Takvim Sağlığı

Psikolojik güvenliğin proxy göstergeleri olarak şunlar da izlenir:

| Sinyal | Sağlıklı | Uyarı |
|--------|----------|-------|
| Sprint check-in yazma oranı | >%80 ekip geneli | <%60 |
| Check-in'de engel raporlama oranı | Doğal dağılım | Hiç engel rapor edilmiyorsa |
| 1:1'de mühendis konuşma oranı | >%50 süre mühendiste | <%30 (yönetici monolog yapıyor) |
| Öz-değerlendirme derinliği | Detaylı, düşünülmüş | Tek cümlelik, formül yanıtlar |
| Metrik itiraz sıklığı | Zaman zaman itiraz var | Hiç itiraz yok |

---

## Rastgele Denetim

### Amaç
Sistemin tasarlandığı gibi çalıştığını doğrulamak. Yöneticileri cezalandırmak
değil, **sistemi kalibre etmek**.

### Süreç
Her çeyrekte:
1. **3 rastgele takım** seçilir (kura ile)
2. Her takımdan **2 rastgele mühendis** seçilir (kura ile)
3. Denetimi yapan kişi: EM veya **o takımlarla doğrudan ilişkisi
   olmayan** bir Engineering Manager
4. İncelenen materyaller:
   - Son 3 ayın 1:1 tutanakları
   - Çeyreklik değerlendirme dokümanı
   - Karşılıklı Anlaşma Dokümanı
   - Sprint check-in response pattern'i (yönetici yanıt veriyor mu?)

### Denetim Checklist'i

- [ ] Aylık 1:1'ler gerçekleşmiş mi? (iptal/erteleme var mı?)
- [ ] 1:1 formatı engineer.md'ye [1] uygun mu?
- [ ] Mühendis sesi tutanakta var mı? (sadece yönetici mi konuşmuş?)
- [ ] Yasaklanmış kullanımlardan herhangi biri var mı?
- [ ] Karşılıklı aksiyon taahhüdü var mı?
- [ ] Geçen dönemin aksiyonları takip edilmiş mi?
- [ ] Çeyreklik görüşmede öz-değerlendirme kullanılmış mı?
- [ ] Yöneticinin çeyreklik özeti yazılmış mı?
- [ ] Sürpriz (ilk kez duyulan olumsuz) geri bildirim var mı?
- [ ] Sprint check-in'lerdeki engellere yönetici yanıt vermiş mi?
- [ ] Dashboard erişimi kısıtlanmış mı?

### Sonuçlar
- Anonim olarak ELT'ye raporlanır (takım/yönetici ismi belirtilmez)
- Pattern/trend olarak paylaşılır: "6 denetimden 4'ünde aksiyon takibi eksik"
- Sorun bulunan yöneticiye **özel ve yapıcı** geri bildirim verilir
- Geri bildirim tone'u: "Şunu fark ettik, nasıl destek olabiliriz?"
  — "Şunu yanlış yapıyorsun" DEĞİL

---

## Özel Durumlar

### Yeni Yönetici Onboarding
- İlk çeyrekte yeni yönetici, deneyimli bir Engineering Manager'dan
  **gölge mentorluk** alır
- İlk 3 aylık 1:1 tutanakları mentor tarafından gözden geçirilir
- İlk çeyreklik değerlendirmede mentor eşlik eder

### Organizasyonel Değişiklik Dönemleri
Büyük reorganizasyon, toplu işe alım veya kriz dönemlerinde:
- Çeyreklik değerlendirme **1 ay** ertelenebilir (ELT kararıyla)
- Aylık 1:1'ler **erteleneMEZ** — kriz döneminde daha da önemli
- Sprint check-in'e geçici olarak 5. soru eklenebilir:
  "Bu dönemde seni etkileyen organizasyonel değişiklik var mı?"

### Performans Süreci Geçişi
L1 metriklerde **3 ay üst üste** kötüleşen trend varsa ve aylık 1:1'lerde
belirlenen aksiyonlar sonuç vermiyorsa [1]:
1. Yönetici bunu mühendisinle **açıkça** konuşur
2. 1:1 geçici olarak **2 haftada 1** olarak sıklaştırılır
3. Bu değişikliğin amacı "daha sık destek" olarak çerçevelenir
4. Maksimum 2 ay bu sıklıkta kalınır
5. İyileşme olursa → normal takvime dönüş
6. İyileşme olmazsa → HR ile formal süreç başlatılır
7. Formal süreç başlatılmadan önce EM bilgilendirilir

> **Hiçbir zaman** metrik verisi tek başına PIP/disiplin kanıtı olamaz.
> Formal süreçte metrikler **bağlam hikayesiyle birlikte** sunulur.

---

## Doküman Yönetimi

### Erişim
- Bu doküman tüm engineering ekibine **açıktır**
- Değişiklikler `#engineering` kanalında duyurulur
- Her mühendis yorum/öneri hakkına sahiptir

### Değişiklik Süreci
1. Değişiklik önerisi `#engineering` kanalında tartışılır
2. Minimum 1 hafta yorum süresi
3. ELT onayı
4. Versiyon numarası güncellenir, değişiklik loguna eklenir

### Gözden Geçirme
- **Çeyreklik:** ELT tarafından metrics review ile birlikte
- **Yıllık:** Kapsamlı revision — tüm ekipten input alınır

---

## Doküman Geçmişi

| Tarih | Versiyon | Değişiklik |
|-------|---------|-----------|
| 2025-01 | 1.0 | İlk yayın |