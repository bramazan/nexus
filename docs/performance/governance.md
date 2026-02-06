# ⚖️ Performans Yönetişimi ve Süreç Rehberi (Governance Handbook)

Bu doküman, Engineering Intelligence platformumuzun nasıl yönetileceğini, verilerin nasıl kullanılacağını ve karar alma mekanizmalarını tanımlar.

## 1. Temel Prensipler (Constitution)

1.  **Bireysel Takip Yok:** Veriler, bireyleri yargılamak için değil; takımların önündeki engelleri (yavaş CI pipeline, belirsiz spekler) kaldırmak için kullanılır [4].
2.  **Goodhart Yasası:** Bir metrik hedef haline gelir ve manipüle edilirse (örn. commit sayısını artırmak için gereksiz commit atmak), o metrik değerlendirmeden çıkarılır [8, 9].
3.  **Trend > Anlık Değer:** Bir haftalık düşüş sorun değildir; 3 aylık düşüş trendi müdahale gerektirir.

---

## 2. Yönetişim Yapısı (Governance Structure)

Platformdaki verilerden ve kararlardan kim sorumludur?

### 🛠️ Platform Ekibi (Enablers)
*   **Sorumluluk:** Metriklerin doğru toplanması, dashboard'ların doğruluğu ve "Golden Path" (Standartlaştırılmış, güvenli dağıtım yolları) oluşturulması [10, 11].
*   **Aksiyon:** Eğer CI/CD süreleri (Deploy Time) tüm takımlarda artıyorsa, altyapıyı iyileştirmek onların görevidir.

### 🧠 Engineering Manager / Takım Lideri
*   **Sorumluluk:** Takımın "Akışını" (Flow) korumak.
*   **Aksiyon:** Cycle Time arttığında, "Geliştirici yavaş" demek yerine; "Analiz dokümanları eksik mi geliyor? Review için çok mu bekliyoruz?" analizini yapmak [12].

### 🤖 AI Yönetişimi (AI Governance)
Yapay zeka (Copilot, Ajanlar) kullanımı arttıkça şu kurallar geçerlidir:
*   **Human-in-the-Loop:** Tamamen AI tarafından yazılan kodlar (Agentic PR), mutlaka bir insan tarafından incelenmeli ve onaylanmalıdır [13].
*   **Rework Rate Takibi:** AI kullanım oranı artarken "Rework Rate" (Hata düzeltme oranı) de artıyorsa, AI kullanımı durdurulup eğitim verilecektir [12, 14].

---

## 3. Süreç ve Ritüeller (The Process)

Veriyi ne sıklıkla ve nasıl konuşacağız?

### A. Haftalık "Check-in" (Opsiyonel - Sadece Sinyaller)
*   **Süre:** 15 Dakika (Async veya Stand-up sonrası).
*   **Odak:** **"Signals"**. Platformdan gelen otomatik uyarılar.
    *   *Örnek:* "Bu hafta Review bekleme süreleri %50 arttı." -> *Aksiyon:* "Arkadaşlar bu hafta PR'lara öncelik verelim." [15].

### B. Sprint Retrospektifi (Operasyonel)
*   **Süre:** Retrospektifin içinde 10-15 dakika.
*   **Veri:** **DORA Metrikleri (Cycle Time, Deployment Freq) ve Planning Accuracy.**
*   **Konuşma:**
    *   "Sprint hedefini (%90 Planning Accuracy) tutturduk mu?"
    *   "Bizi en çok yavaşlatan 'Blocker' neydi? (Veriye bakılır: Bekleme süreleri)." [16].

### C. Çeyrek Dönem Değerlendirmesi (Stratejik & Kariyer)
*   **Süre:** 1 Saat (1:1 Görüşme).
*   **Veri:** **SPACE (Memnuniyet, Gelişim) + Yatırım Profili (Investment Profile).**
*   **Konuşma:**
    *   "Teknik borç temizliğine yeterince vakit ayırabildin mi? (%15 kotası)" [17].
    *   "Odaklanma süren (Focus Time) yeterli mi? Toplantı yükünü azaltmalı mıyız?" [18].
    *   "Kariyer hedefin için hangi yetkinliklere (Review kalitesi, Mimari katkı) odaklanalım?"

---

## 4. Kırmızı Çizgiler (Anti-Patterns)

Aşağıdaki davranışlar bu sürecin ruhuna aykırıdır ve yasaktır:
*   ❌ **Stack Ranking:** Geliştiricileri "En çok satır yazanlar" diye sıralamak [19].
*   ❌ **Hata Sayısı ile Yargılama:** Hataları (Bugs) kişisel başarısızlık saymak. (Bunun yerine sistemin hatayı yakalama yeteneğini sorgularız).
*   ❌ **AI Kodunu Sorgusuz Kabul Etmek:** AI metriklerini (Hız) artırmak için kali