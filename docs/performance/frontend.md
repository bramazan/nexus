(Frontend Mühendisleri için Performans ve Gelişim El Kitabı)
# 🎨 Frontend Engineering: Performans ve Gelişim Rehberi (2026)

## 🎯 Vizyonumuz: "Kullanıcı Deneyimi ve Hızlı Akış"
Frontend ekibi olarak amacımız sadece arayüz çizmek değil; kullanıcıya **hızlı**, **hatasız** ve **öngörülebilir** bir deneyim sunmaktır. Platformumuzdaki verileri, **tasarım ile kod arasındaki sürtünmeyi azaltmak** ve **dağıtım (deploy) sıklığını artırmak** için kullanacağız.

---

## 1️⃣ Hedeflerimiz

### 📦 1. PR Size & Cycle Time
*   **Hedef:** PR Boyutu **< 200 Satır**, Cycle Time **< 2 Gün**.
*   **Neden?** Frontend geliştirmede büyük PR'lar ("Big Bang"), görsel hataların (UI Bugs) gözden kaçmasına neden olur. Küçük ve sık değişiklikler, inceleme kalitesini artırır

### 🎨 2. Visual Rework Rate
*   **Tanım:** "Done" statüsüne gelen bir işin, tasarım uyumsuzluğu veya UI hatası nedeniyle tekrar açılması.
*   **Hedef:** **<%5**.
*   **Mantık:** Tasarım (Figma) ile kod arasındaki uyumsuzluk en büyük zaman kaybıdır. Bu metrik yüksekse, geliştirme öncesi analiz/tasarım sürecini iyileştiririz

### 🚀 3. Deployment Frequency
*   **Tanım:** Production ortamına ne sıklıkla çıkıldığı.
*   **Hedef:** Günde en az **1 kez** (Takım ortalaması).
*   **Mantık:** Sık dağıtım, hataların etkisini azaltır ve kullanıcı geri bildirim döngüsünü kısaltır [1].

### 🚦 4. Core Web Vitals & Lighthouse
*   **Tanım:** Geliştirilen sayfaların performans (LCP, INP) ve erişilebilirlik skorları.
*   **Hedef:** Lighthouse Skoru **> 90**.
*   **Not:** Performans bir "özellik" değil, temel bir gereksinimdir.

---

## 2️⃣ Teşhis ve Gelişim Metrikleri

### 🤖 AI Etkisi ve Verimliliği
*   **AI Code Acceptance:** Copilot/AI önerilerini kabul etme oranınız.
*   **Nasıl Kullanılır?** Bu bir hedef değildir. Ancak hızınız düşükse ve AI kullanımınız da düşükse, *"Acaba araçları verimli kullanabiliyor musun? Eğitim ister misin?"* diye sorarız. Tam tersi, kullanım yüksek ama hata (Rework) da yüksekse, *"AI kodunu yeterince denetlemiyor musun?"* diye bakarız

### 🧘 SPACE: Geliştirici Deneyimi (DevEx)
*   **Local Build Time:** Projenin lokal ortamda derlenme süresi. Eğer bu süre artarsa, altyapı ekibinden iyileştirme talep ederiz.
*   **Toplantı Yükü:** Kod yazma zamanınızı korumak için takviminizdeki "Focus Time" bloklarını takip ederiz.

---

## 🗓️ Performans Görüşmeleri: Beklentiler
Performans görüşmelerinde (1:1), sayılardan çok **trendlere** ve **engellere** odaklanacağız:

*   **Beklemeler:** *"Cycle Time verisine göre, Backend API'lerini beklerken çok zaman kaybediyorsun. Bu bağımlılığı yönetmek için 'Contract Testing' veya 'Mocking' süreçlerini iyileştirelim mi?"*
*   **Kalite:** *"Son sprintte Visual Rework (tasarım düzeltmeleri) artmış. Tasarımcılarla olan iletişimde veya speklerin netliğinde bir sorun mu var?"*

**Özet:** Bu sistem sizi yargılamak için değil; **"Flow" (Akış)** durumunuzu bozan taşları yoldan temizlemek için kuruldu.
