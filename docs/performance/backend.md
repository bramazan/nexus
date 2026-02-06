# 🛠️ Backend Engineering: Performans ve Gelişim Rehberi (2026)

## 🎯 Amacımız
Bu rehber, Backend ekibinin başarısını ölçülebilir, adil ve şeffaf bir zemine oturtmak için hazırlanmıştır. Hedefimiz sizi yargılamak değil; **"Akış" (Flow)** durumunuzu korumak, teknik borcu yönetilebilir kılmak ve yazdığınız kodun müşteriye (veya diğer ekiplere) en hızlı ve güvenli şekilde ulaşmasını sağlamaktır.

Metrikleri 3 ana başlıkta takip edeceğiz: **Etki (Impact)**, **Kalite (Quality)** ve **Süreç (Process)**.

---

## 1️⃣ Stratejik "Kuzey Yıldızı" Metrikleri (North Star Metrics)
*Bu metrikler, global mühendislik standartlarında (DORA, SPACE) başarımızın ana göstergeleridir.*

### 🚀 Cycle Time (Döngü Süresi)
*   **Nedir?** Kodlamaya başladığınız andan (ilk commit), kodun canlı ortama (production) çıkmasına kadar geçen süre.
*   **Hedef:** < 48 Saat (İdeal: < 25 Saat).
*   **Neden?** Bekleme sürelerini (Code Review, Test, Deploy) minimize ederek işi bitirme hızımıza odaklanıyoruz.

### 🤝 Reviewer Response Time (Kod İnceleme Hızı)
*   **Nedir?** Size atanan bir PR'a (Pull Request) ilk yanıtı verme veya onaylama süreniz.
*   **Hedef:** **< 4 Saat** (Sizin Maddeniz).
*   **Neden?** Takım arkadaşlarını bloklamamak, bireysel hızdan daha değerlidir. "Benim işim bitti" değil, "Bizim işimiz bitti" kültürü esastır.

### 🎯 Planning Accuracy (Sprint Hedef Başarısı)
*   **Nedir?** Sprint başında taahhüt edilen işlerin (Sprint Goal), sprint sonunda tamamlanma oranı.
*   **Hedef:** **> %90** (Sizin Maddeniz).
*   **Neden?** İş birimine ve diğer ekiplere verdiğimiz sözü tutmak, öngörülebilirliğimizin kanıtıdır.

---

## 2️⃣ Kalite ve Güvenilirlik Metrikleri (Quality & Reliability)
*Hız yaparken sistemi kırmadığımızdan ve teknik borç yaratmadığımızdan emin olduğumuz alan.*

### 🛡️ Production Reliability & Change Failure Rate (CFR)
*   **Nedir?** Canlıya çıkan paketlerin hata yaratma oranı ve kritik hata sayısı.
*   **Hedef:** **< %5 Hata Oranı** (veya Çeyrekte < 2 Kritik Hata - Sizin Maddeniz).
*   **Neden?** Hızlı ama hatalı kod, uzun vadede bizi yavaşlatır.

### 📉 Rework Rate (Yeniden İşleme Oranı)
*   **Nedir?** Merge edilen kodun, takip eden 21 gün içinde ne kadarının değiştirildiği.
*   **Hedef:** < %3 - %5.
*   **Neden?** Özellikle AI araçları kullanırken, kodun "bir kerede doğru" (First Time Right) yazılıp yazılmadığını ölçer. Yüksek rework, analiz eksikliğine işarettir.

### ⚡ API Latency & Performance
*   **Nedir?** Sorumlu olduğunuz servislerin P95 yanıt süresi.
*   **Hedef:** **< 200ms** (Sizin Maddeniz).
*   **Neden?** Kodun sadece çalışması yetmez, performanslı çalışması gerekir.

---

## 3️⃣ Süreç ve Hijyen Metrikleri (Process & Hygiene)
*Sürdürülebilir bir çalışma ortamı için "yapılması gereken" rutinler.*

### 🧹 Technical Debt Allocation (Teknik Borç Kotası)
*   **Nedir?** Sprint eforunun refactoring, kütüphane güncelleme ve güvenlik sıkılaştırmalarına ayrılan kısmı.
*   **Hedef:** **Eforun %15'i** (Sizin Maddeniz).
*   **Aksiyon:** Bu kotayı kullanmak sizin sorumluluğunuzdadır. Kullanmazsanız teknik iflas (technical bankruptcy) riski artar.

### 🧪 Test Coverage (Test Kapsamı)
*   **Nedir?** Yeni yazılan kodların birim test (Unit Test) kapsamı.
*   **Hedef:** **Yeni Kod > %80** (Sizin Maddeniz).
*   **Not:** Sadece "coverage" yüzdesi değil, testlerin anlamlı senaryoları (business logic) kapsaması önemlidir.

### 📋 Sprint & Jira Hijyeni (Ortak Madde)
*   **Nedir?** Sprint ritüellerine katılım, Jira tasklarının güncelliği ve dokümantasyon (Confluence) katkısı.
*   **Hedef:** %90 Uyum.
*   **Neden?** Görünürlük yoksa, yönetim (management) size doğru desteği sağlayamaz.

---

## 🗓️ Performans Değerlendirmesi Nasıl Yapılacak?

Görüşmelerimizde bu 9 maddeyi şu çerçevede konuşacağız:

1.  **Veri Odaklı Teşhis:** "API Latency hedefini tutturamadın" demek yerine; *"API Latency artmış, bunun sebebi teknik borç (Madde 6) kotasını yeterince kullanamamamız mı?"* diye soracağız.
2.  **Sistem Sorunları:** Reviewer Response süresi (Madde 8) kötüyse, bunun sebebi senin yavaşlığın mı yoksa toplantı yükünün (Meeting Load) fazla olması mı? Bunu analiz edeceğiz.
3.  **Goodhart Yasası:** Bu metrikleri "tutturmak" için sistemi manipüle etmeyin (örn. coverage artsın diye içi boş test yazmak). Metrikler araçtır, amaç kaliteli üründür. 