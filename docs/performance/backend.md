(Backend Mühendisleri için Performans ve Gelişim El Kitabı)
# 🛠️ Software Engineering: Performans ve Gelişim

## 🎯 Vizyonumuz:
Backend ekibi olarak başarımız sadece "yazdığımız kod satırı" ile ölçülmez. Bizim için başarı; **sistemin kararlılığı**, **iş akışının kesintisizliği** ve **teknik borç yaratmadan değer üretme** yeteneğimizdir.

Bu doküman, takip ettiğimiz metrikleri ve bunların performans değerlendirmesinde nasıl kullanılacağını şeffaflaştırır.

---

## 1️⃣ Hedeflerimiz
*Bu 4 metrik, başarımızın ana göstergeleridir ve doğrudan iş sonuçlarına etki eder.*

### 🚀 1. Cycle Time
*   **Tanım:** Bir işe başladığınız (ilk commit) andan, o işin canlı ortamda (production) çalışır hale gelmesine kadar geçen süre.
*   **Hedef (Elit):** **< 25 Saat**.
*   **Neden Önemli?** Düşük Cycle Time, müşteriye hızlı değer ürettiğimizi ve kodun bekleme kuyruklarında (Code Review, Deploy) çürümediğini gösterir.

### 📉 2. Rework Rate
*   **Tanım:** Merge edilen (tamamlanan) bir kodun, takip eden 21 gün içinde ne kadarının değiştirildiğini ölçer.
*   **Hedef:** **<%3 - %5**.
*   **Kritik Uyarı:** Özellikle AI (Copilot vb.) kullanırken "hızlı ama hatalı/eksik" kod üretip üretmediğimizin en net sinyalidir. Yüksek Rework, analiz eksikliğine veya "Blind AI Trust" (Kör YZ Güveni) sorununa işarettir.

### 🛡️ 3. Change Failure Rate
*   **Tanım:** Canlıya çıkan paketlerin (deployments) yüzde kaçının bir "incident" (olay) veya "rollback" (geri alma) ile sonuçlandığı.
*   **Hedef:** **<%1 - %5**.
*   **Mantık:** Hızlanırken sistemi kırmadığımızdan emin olmalıyız. İstikrar yoksa, hızın bir değeri yoktur

### 🎯 4. Planning Accuracy
*   **Tanım:** Sprint başında taahhüt edilen işlerin (Story Points), sprint sonunda tamamlanma oranı.
*   **Hedef:** **>%85**.
*   **Mantık:** İş birimine verdiğimiz sözü tutmak, mühendislik ekibine olan güveni artırır ve tahmin edilebilirliği (predictability) sağlar.

---

## 2️⃣ Teşhis ve Gelişim Metrikleri 
*Bu metrikler birer "hedef" değildir. Ana hedefleri tutturamadığımızda "Neden?" sorusunu cevaplamak için kullandığımız sinyallerdir.*

### 🤝 İşbirliği ve İnceleme 
*   **Reviewer Response Time:** Bir PR size atandığında ilk yanıtı verme süreniz. Hedef **< 4 Saat**. Takım arkadaşlarını bloklamamak, bireysel hızdan daha değerlidir [5].
*   **PR Size:** PR başına kod değişikliği. Hedef **< 200 satır**. Küçük parçalar daha hızlı incelenir ve daha az hata içerir

### 🧠 İnsan Faktörü ve SPACE 
*   **Focus Time (Odaklanma):** Gün içinde toplantısız, kesintisiz çalışabildiğiniz blok süreler. Hedef: Günde **> 2 Saat**. Eğer toplantı yükünüz artarsa, yöneticinizle takviminizi sadeleştirmeyi konuşuruz.
*   **Context Switching:** Gün içinde çok fazla farklı işe (ticket) dokunup dağılmanız. Bunu minimize etmeye çalışırız.

### 💰 Maliyet Bilinci
*   **Cloud Cost Efficiency:** Yazdığınız servislerin veya sorguların bulut maliyetine etkisi. Verimsiz kaynak kullanımını (Waste) kod aşamasında fark etmenizi bekleriz

---

## 🗓️ Performans Görüşmeleri Nasıl Yapılacak?
Yöneticinizle yapacağınız 1:1 görüşmeler "hesap sorma" seansı değildir. Veriyi şöyle kullanacağız:

1.  **Engelleri Kaldırmak:** *"Cycle Time artmış görünüyor. Verilere baktığımda 'Pickup Time' (İnceleme Bekleme) süresinin çok yüksek olduğunu görüyorum. Takım içinde kod incelemelerini hızlandırmak için ne yapabiliriz?"*
2.  **Yük Dengesi:** *"Bu ay çok fazla PR incelemişsin (High Reviewer Load), bu yüzden kendi işlerin gecikmiş olabilir. Teşekkürler, gelecek sprint yükü dengeleyelim."*
3.  **AI Kullanımı:** *"AI kullanım oranın yüksek ama Rework oranın da artmış. AI çıktılarını merge etmeden önce daha detaylı test etmelisin."*

**⚠️ Kırmızı Çizgi:** Bu metrikleri "oyunlaştırmaya" (gaming) çalışmak (örn. commit sayısını artırmak için gereksiz boşluk eklemek) kesinlikle yasaktır ve profesyonelliğe aykırıdır.

