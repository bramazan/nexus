# Örnek Raporlar — Ali Yılmaz

---

## 1. Sprint Sonu Check-in Örneği

**Konum:** Confluence → Ali Yılmaz → Check-ins sayfası
**Format:** Tabloya yeni satır eklenir

| Sprint | En Önemli İş | Engel | Gelecek Sprint Odak | Yöneticimden İhtiyaç | YM Notu |
|--------|-------------|-------|---------------------|---------------------|---------|
| Sprint 16 | Auth modülü tamamlandı, sandbox çalışıyor | Cuma 3.5h toplantı, kod yazamıyorum | Payment flow core | Toplantı yükü çözümü | 1:1'de bakarız 📅 |
| Sprint 15 | Reconciliation prod deploy 🎉 0 hata | — | Ödeme sağlayıcı başlangıç | Yok | Tebrikler! 🎉 |
| Sprint 14 | Phase 2 error handling + dead letter queue | API kontratı belirsiz, scope kayıyor | Prod deploy planı | Product ile üçlü toplantı | Salı 14:00 ayarladım 👍 |
| Sprint 13 | Phase 1 refactoring tamamlandı | Kafka topic 3 gündür bekliyor | Staging test + Phase 2 | DevOps escalation | DevOps ile konuştum ✅ |

> Mühendis her sprint sonunda en üste yeni satır ekler (~5dk).
> Yönetici "YM Notu" sütununa kısa yanıt yazar (~2dk).
> Engel varsa 48 saat içinde aksiyon alınır.
> Engel yoksa emoji yeterli.

---

## 2. Aylık 1:1 Görüşme Tutanağı

**Konum:** Confluence → Ali Yılmaz → 1:1 Şubat 2026

| | |
|---|---|
| **Mühendis** | Ali Yılmaz — Senior Software Engineer |
| **Yönetici** | Burak Ramazan — Engineering Manager |
| **Tarih** | 2026-02-07 |
| **Dönem** | Sprint 15–16 |

### Mühendisin Hazırlığı

> **Gurur:** Reconciliation prod'a çıktı. Ops günlük 12-15 manual
> intervention'dan 0'a düştü.
>
> **Zorluk:** İki projeyi paralel yürütmek yordu. Focus time düştü.
>
> **Metrik yorumum:** Cycle Time 38h — büyük PR'lar nedeniyle, bilinçli
> tercih. Rework %8 ama 2/3'ü API kontrat değişikliğinden.
>
> **Taleplerim:** Cuma toplantı yükü çözülsün.

### Yönetici Notları (Görüşme Öncesi)

> Cycle Time ve Rework trendinin yukarı gitmesi muhtemelen reconciliation
> refactoring'in PR boyutu + paralel ödeme sağlayıcı işinden kaynaklanıyor.
> Change Failure Rate sıfır — kaliteden ödün vermemiş, hızdan ödün vermiş.
> Focus Time ciddi düşük, toplantı yükünü birlikte çözmemiz lazım.

### L1 Metrikler

| Metrik | Ali | Takım Ort. | Not |
|--------|:---:|:----------:|-----|
| Cycle Time | 38h ↗️ | 26h | Reconciliation PR boyutu. Bağlamla açıklanıyor. |
| Change Failure Rate | %0 | %4 | Mükemmel. |
| Rework Rate | %8 ↗️ | %6 | 2/3 kontrol dışı. Ali'nin kontrolünde ~%3. |

### Takım Katkısı Geri Bildirimi

Elif (Frontend): "Reconciliation API dokümanı çok temiz, entegrasyon
3 gün yerine 1 günde bitti."

Can (Junior): Review'lardan çok öğrendiğini söyledi.

### Görüşme Özeti

**Veri:** Cycle Time ve Rework yükselişi beklenen ve açıklanan bir durum.
Temmuz'da normalleşmezse tekrar bakarız.

**Engel:** Cuma takvimi incelendi. Ad-hoc product sync kaldırıldı,
Knowledge Share Perşembe'ye alınacak. ~1.5 saat kazanım.

**Geçen dönem aksiyonları:**
- ✅ Burak: DevOps escalation
- ✅ Burak: Product toplantısı
- ✅ Ali: API kontrat taslağı

### Aksiyonlar

| Kimin | Aksiyon | Deadline |
|-------|---------|----------|
| Ali | Auth modülünü staging'e deploy | 2026-02-14 |
| Burak | Knowledge Share saatini ekiple tartış | Sprint 17 Retro |
| Burak | Reconciliation etkisini yönetim sunumuna dahil et | 2026-02-15 |

| | |
|---|---|
| **Sonraki 1:1** | 2026-03-07 |
| **Mühendis onayı** | ☐ |

---

## 3. Çeyreklik Değerlendirme Tutanağı

**Konum:** Confluence → Ali Yılmaz → Q2 2026

| | |
|---|---|
| **Mühendis** | Ali Yılmaz — Senior Software Engineer |
| **Yönetici** | Burak Ramazan — Engineering Manager |
| **Tarih** | 2026-02-07 |
| **Dönem** | Q2 2026 (Nisan – Haziran) |

### Ali'nin Öz-Değerlendirmesi

**En etkili işlerim:**
1. **Reconciliation Refactoring** — 3 yıllık teknik borç temizlendi.
   Ops manual intervention: 12-15/gün → 0.
2. **Ödeme Sağlayıcı Entegrasyonu** — Auth modülü tamamlandı. Devam ediyor.
3. **Junior Mentorluk** — Can'a 6 detaylı PR review + 2 pair session.

**Öğrendiğim:** Event-driven architecture'da dead letter queue yönetimi.

**Metrik yorumum:**
- Cycle Time: Son sprinte yükseldi, reconciliation PR boyutundan. Düşecek.
- Change Failure Rate: %0-2 arası, tutarlı.
- Rework: Yükseldi ama çoğu API kontrat kaynaklı.

**Takıma katkım:** 42 PR review (takımda en yüksek), ort. 2.1h response,
2 teknik doküman.

**Kariyer hedefim:** Staff Engineer. Eksiklerim: teknik strateji dokümanı
yazmadım, Architecture Review'da pasif kaldım.

**Gelecek çeyrek odağım:**
1. ADR/RFC yazma deneyimi
2. Architecture Review'da aktif katılım

**Yöneticimden beklentim:** Staff yolculuğu için mentor/sponsor desteği.

### Yöneticinin Çeyreklik Özeti

**Genel değerlendirme:** Ali bu çeyrekte teknik derinlik ve cross-team etki
açısından Senior seviyesinin üzerinde performans gösterdi. Reconciliation
projesi end-to-end sahiplenme örneği. Cycle Time ve Rework yükselişi
bağlamla açıklanıyor ve Q3'te normalleşmesini bekliyorum.

**Öne çıkan güçlü yanlar:** Proaktif problem tespiti, iş etkisini
somutlaştırma becerisi, review hızı ile takım çarpan etkisi.

**Gelişim fırsatları:** Teknik strateji dokümantasyonu ve Architecture
Review'da sesini duyurması Staff yolculuğu için kritik.

### Takım Katkısı Geri Bildirimi

- Elif (FE): "API dokümanı sayesinde entegrasyon 3 gün→1 güne düştü."
- Can (Jr): "Review'larda alternatif yaklaşımları açıklıyor, çok öğretici."
- Burak (Ops): "Reconciliation refactoring hayat kurtardı."
- Ali'nin review istatistikleri: 42 PR review, ort. 2.1h response (takım ort: 6.4h)

### L1 Çeyreklik Trend

| Metrik | Q2 Trendi | Takım Ort. | Değerlendirme |
|--------|-----------|:----------:|---------------|
| Cycle Time | 24→28→38h ↗️ | 26h | Reconciliation etkisi. Q3'te normalleşecek. |
| Change Failure Rate | %0-2 ─ | %4 | Tutarlı, mükemmel. |
| Rework Rate | %4→6→8 ↗️ | %6 | API kontrat kaynaklı (çözüldü). Q3'te düşecek. |

### Delivery Impact

Reconciliation projesi Senior beklentisinin **üzerinde**:
- Problemi kendi buldu, çözümü tasarladı
- Cross-team etki yarattı (Ops, Finans, Frontend)
- İş etkisini somut veriyle ifade edebildi
- Bu davranış kalıbı Staff Engineer beklentileriyle örtüşüyor

### Görüşme Notları
- Delivery Impact: Ali reconciliation'ı Senior üstü bir iş olarak sundu, yönetici onayladı.
- Kariyer: Staff yolculuğuna başlama kararı verildi, ADR + Arch Review odak olacak.
- Ali'nin talebi: Mentor desteği — Deniz ile eşleştirilecek.

### Kariyer Değerlendirmesi

**Staff ile örtüşen:**
- ✅ End-to-end sahiplenme + cross-team etki
- ✅ Takım çarpan etkisi (review hızı + mentorluk)
- ✅ Teknik borcu proaktif sahiplenme

**Staff için geliştirilecek:**
- ❌ Teknik strateji dokümanı (ADR/RFC)
- ❌ Architecture Review'da aktif katılım
- ❌ Sistematik mentorluk (informal → yapılandırılmış)

### Aksiyonlar

| Kimin | Aksiyon | Deadline |
|-------|---------|----------|
| Ali | Ödeme sağlayıcı için ADR yazacak | Taslak: Temmuz sonu |
| Ali | Q3'te en az 2 Architecture Review'da aktif katılım | İlki: Temmuz |
| Burak | Deniz'in ADR örneğini paylaşacak | 2026-02-15 |
| Burak | Ali–Deniz mentorluk bağlantısı kuracak | 2026-02-15 |
| Burak | Reconciliation etkisini yönetim sunumuna dahil edecek | 2026-02-15 |

### Karşılıklı Anlaşma

| | İmza | Tarih |
|---|---|---|
| Ali Yılmaz | ☐ | |
| Burak Ramazan | ☐ | |

| | |
|---|---|
| **Sonraki 1:1** | 2026-03-07 |
| **Sonraki çeyreklik** | Q3 2026 sonu |
| **Gizlilik** | Sadece Ali ve Burak |