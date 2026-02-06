# 📊 Kapsamlı Mantık ve Metrik Envanteri (70+ Metrik)

> [!IMPORTANT]
> **Gizlilik ve Etik Bildirgesi**
> Nexus, **sistemleri, akışları ve darboğazları** ölçmek için tasarlanmıştır, bireyleri gözetlemek için değil.
> - **Trendler > Mutlak Değerler:** Metrikler, günlük çıktıyı mikro-yönetmek için değil, eğilimleri belirlemek için kullanılmalıdır.
> - **Önce Gizlilik:** "Odaklanma Süresi" (Focus Time) gibi veriler, psikolojik güvenliği korumak amacıyla anonimleştirilir veya toplulaştırılır.
> - **Oyunlaştırma Karşıtlığı:** Goodhart Yasası geçerlidir ("Bir ölçüt hedef haline geldiğinde, iyi bir ölçüt olmaktan çıkar"). Bu metrikleri teşhis için kullanın, bireyler için KPI hedefi olarak değil.

## 1. 🏛️ Gelişmiş DORA & Akış Metrikleri (Post-DORA)
**Amaç:** Hız ve istikrarı sadece sonuç (lagging) göstergeleriyle değil, süreç (leading) göstergeleriyle ölçmek.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | ELIT Hedef (2026) |
| :--- | :--- | :--- | :--- | :--- |
| **1** | **Cycle Time (Global)** | İlk commit'ten canlıya çıkışa kadar geçen toplam süre. Nihai hız metriğidir. | Git + Jira | < 25 Saat |
| **2** | **Coding Time** | İlk commit ile PR açılışı arasındaki süre. Bireysel "akış" durumunu ölçer (yerel geliştirme gürültüsüne dikkat). | Git | < 54 Dk (Trend) |
| **3** | **Pickup Time** | PR açıldıktan sonra incelemenin başlaması için geçen bekleme süresi. Önemli bir darboğaz göstergesi. | Git | < 60 Dk |
| **4** | **Review Time** | İlk yorumdan birleştirmeye (merge) kadar geçen tartışma süresi. Yüksek değerler belirsiz spekleri veya karmaşıklığı gösterir. | Git | < 3 Saat |
| **5** | **Deploy Time** | CI/CD süreç süresi. Yavaş pipeline'lar geliştirici momentumunu öldürür. | CI/CD | < 16 Dk |
| **6** | **Deployment Frequency** | Servis başına günlük başarılı dağıtım sayısı. Yüksek frekans, paket büyüklüğü riskini azaltır. | CI/CD | > 1.2 / Gün |
| **7** | **Failed Recovery Time** | (MTTR) Başarısız bir dağıtım sonrası servisi geri yükleme süresi. | Gözlemlenebilirlik | < 1 Saat |
| **8** | **Change Failure Rate** | Canlı ortamda hataya neden olan dağıtımların yüzdesi. | Jira + CI/CD | < %1 |
| **9** | **Throughput** | Zaman içinde tamamlanan iş birimi (PR/Bilet) sayısı. | Jira / Git | Kararlı / Artan |
| **10** | **Merge Frequency** | Geliştirici başına haftalık merge edilen PR sayısı. Küçük parçalarla ve sık entegrasyonu teşvik eder. | Git | > 2.0 / Hafta |
| **11** | **Flow Efficiency** | `Aktif Süre / (Aktif + Bekleme Süresi)`. İşin ne kadar süre boşta beklediğini gösterir. | Jira Statüleri | > %40 |
| **12** | **WIP (Work In Progress)** | Kişi başı aktif paralel görev sayısı. Yüksek WIP odağı yok eder. | Jira + Git | < 2 Madde |

---

## 2. 🤖 YZ ve Ajan Tabanlı Zeka (2026 Yeni)
**Amaç:** YZ ajanlarının otonomisini ölçmek ve değer akışını şişirmeden iyileştirdiklerinden emin olmak.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef / Kritik |
| :--- | :--- | :--- | :--- | :--- |
| **13** | **AI Acceptance Rate** | Geliştiriciler tarafından kabul edilen YZ önerilerinin (Copilot/Cursor) oranı. Araç faydasını ölçer. | IDE Telemetrisi | > %30 |
| **14** | **Agentic PR Rate** | Tamamen otonom ajanlar (örn. Devin) tarafından oluşturulan PR oranı. | Git Etiketleri | İzleme |
| **15** | **Review Depth (AI)** | YZ koduna yapılan yorumlar vs. İnsan koduna yapılan yorumlar. YZ koduna "Kör Onay" verilmesini engeller. | Git | > İnsan Ort. |
| **16** | **AI ROI Time** | YZ destekli görevlerin döngü süresi vs. tamamen manuel görevler. | Git + Jira | YZ < Manuel |
| **17** | **Unverified Code Sources** | Bilinmeyen/onaysız LLM'lerden gelen kod parçalarının tespiti (eski adıyla "Gölge YZ"). | Provenance | %0 |
| **18** | **Prompt Risk Score** | Ajan girdilerinin "prompt injection" saldırılarına karşı savunmasızlığı. | Güvenlik Taraması | Düşük |
| **19** | **Agent Success Rate** | Bir Ajanın insan müdahalesi olmadan tamamladığı görevlerin yüzdesi. | Ajan Logları | > %80 |
| **20** | **Inter-Agent Handoff** | Çıktıların bir Ajandan diğerine hatasız geçiş oranı (Düşünce Zinciri). | Orkestratör | Yüksek |
| **21** | **Token Efficiency** | Kabul edilen PR başına maliyet (Token). YZ işgücünün finansal verimliliği. | LLM Gateway | Optimize Et |

---

## 3. 🛡️ Kod Kalitesi ve Risk
**Amaç:** Hızın, özellikle YZ kaynaklı hacimle birlikte, uzun vadeli sürdürülebilirliği tehlikeye atmamasını sağlamak.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | ELIT Hedef (2026) |
| :--- | :--- | :--- | :--- | :--- |
| **22** | **Rework Rate** | Merge edildikten sonraki 21 gün içinde yeniden değiştirilen kod. Yüksek oran = yanlış anlaşılmış gereksinimler. | Git | < %3 |
| **23** | **Refactor Rate** | Eski koda (>21 gün) yapılan güncellemeler. Sağlıklı teknik borç ödemesini gösterir. | Git | %10-15 |
| **24** | **PR Complexity** | PR başına değiştirilen kod satırı. Küçük değişiklikler daha güvenlidir. | Git | < 200 Satır |
| **25** | **Code Churn** | Eklenen/silinen/değiştirilen kod hacmi. Yüksek ani artışlar istikrarsızlığı gösterir. | Git | Düşük |
| **26** | **Defect Density** | 1.000 satır kod başına düşen hata sayısı. | SonarQube | < 0.2 |
| **27** | **Defect Escape Rate** | Canlı ortamda bulunan hatalar vs. Alt Ortamlarda bulunanlar. | Jira | < %5 |
| **28** | **Code Coverage** | Otomatik testler tarafından kapsanan kod tabanı yüzdesi. | Birim Testler | > %80 |
| **29** | **PR Maturity** | PR açıldığında kodun ne kadar "bitmiş" olduğu (sonraki churn'e dayanarak). | Git | > %90 |
| **30** | **Tech Debt Ratio** | `Onarım Eforu / Yeni Özellik Eforu`. | Jira Tipleri | < %20 |

---

## 4. 🧠 İnsan ve Geliştirici Deneyimi (DevEx)
**Amaç:** Tükenmişliği önlemek ve "Derin Çalışma" (Deep Work) süresini korumak.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef |
| :--- | :--- | :--- | :--- | :--- |
| **31** | **Focus Time** | Günlük ≥2 saatlik kesintisiz çalışma bloklarının toplamı. | Takvim/IDE | > 4.2 Saat |
| **32** | **Meeting Load** | Toplantılarda geçirilen sürenin yüzdesi. | Takvim | < %20 |
| **33** | **Context Switching** | Gün içinde farklı biletler/görevler arasında geçiş sıklığı. | IDE/Jira | Düşük |
| **34** | **Dev eNPS** | Net Tavsiye Skoru: "Bu ekibi bir arkadaşına önerir misin?" | Anket | > 40 |
| **35** | **Burnout Risk** | Bileşik gösterge: Yüksek Fazla Mesai + İzin Kullanmama + Yüksek WIP. | İK/Git | Düşük |
| **36** | **Knowledge Silos** | PR incelemelerinin dağılımı. Her şeyi sadece 1-2 kişi mi inceliyor? | Git | Dengeli |
| **37** | **Onboarding Speed** | Yeni işe başlayanların ilk anlamlı commit'i atma süresi. | İK/Git | < 14 Gün |
| **38** | **Perceived Productivity** | Geliştiriciler kendilerini üretken *hissediyor mu*? (Genelde verilere göre elde tutma ile daha iyi korelasyon gösterir). | Anket | Yüksek |
| **39** | **Peak Productivity Hours** | Ekibin veya bireylerin en çok commit attığı saat dilimleri (Sabahçı vs. Akşamcı). Toplantı planlaması için kritiktir. | Git + Jira Time Stamps | İzleme |
| **40** | **After-Hours Activity** | Mesai saatleri dışında (örn. 19:00 sonrası) veya hafta sonu yapılan işlerin oranı. Tükenmişlik (Burnout) için erken uyarıdır. | Git + Jira Time Stamps | < %5 |

---

## 5. 🗺️ Planlama ve Ürün Uyumu
**Amaç:** Mühendislik çıktısını iş stratejisiyle hizalamak.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef |
| :--- | :--- | :--- | :--- | :--- |
| **41** | **Planning Accuracy** | Söylenen/Yapılan Oranı: Sprint hedeflerinin % kaçını gerçekleştirdik? | Jira | > %80 |
| **42** | **Capacity Agility** | Ekibin değişimi absorbe etme yeteneği vs. katı kapasite. | Jira | %85-115 |
| **43** | **Scope Creep** | Sprint başladıktan sonra eklenen plansız işler. | Jira | < %10 |
| **44** | **Unplanned Work** | Yangın söndürme / acil hatalar için harcanan zaman yüzdesi. | Jira | < %15 |
| **45** | **Investment Profile** | Kaynak dağılımı: İnovasyon vs. İşletme (KTLO) vs. Borç. | Jira Etiketleri | %60 Yeni |
| **46** | **Ticket Hygiene** | Jira girdilerinin kalitesi (açıklama, kabul kriterleri). | Jira | > %90 |
| **47** | **Orphan Items** | Stratejik bir Girişime/Epic'e bağlı olmayan görevler. | Jira | < %5 |
| **48** | **Lead Time to Start** | Backlog oluşturulmasından "Devam Ediyor"a geçiş süresi. | Jira | Minimal |
| **49** | **Feature Adoption** | Yayınlanan özelliklerin kullanım oranı. Nihai "Sonuç" metriği. | Analitik | Yüksek |

---

## 6. 💰 FinOps ve İş Değeri
**Amaç:** Mühendislik çerçevesini "Maliyet Merkezi"nden "Kar Sürücüsü"ne dönüştürmek.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef |
| :--- | :--- | :--- | :--- | :--- |
| **50** | **Cost per Feature** | Özellik başına toplam tahsis edilen karmaşıklık maliyeti (Bulut + İnsan). | FinOps | İzleme |
| **51** | **Rev per Engineer** | Gelir / Mühendis Sayısı. Verimlilik vekili. | Finans | Yüksek |
| **52** | **Capitalization Rate** | CapEx vergi teşviki için uygun mühendislik işi yüzdesi. | Jira/Finans | Otomatik |
| **53** | **Cloud Waste** | Atıl/kullanılmayan kaynaklara yapılan harcama (örn. zombi geliştirme ortamları). | Bulut Maliyeti | < %5 |
| **54** | **Revenue Impact** | Belirli sürümlere doğrudan gelir atfı (mümkün olduğunda). | Analitik | Yüksek |
| **55** | **Data Sovereignty** | Coğrafi veri barındırma yasalarına uyumluluk. | Uyumluluk | %100 |

---

## 7. 🚑 Operasyonel Sağlık (SRE)
**Amaç:** Güvenilirlik yoluyla güveni sürdürmek.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef |
| :--- | :--- | :--- | :--- | :--- |
| **56** | **MTTR (Incident)** | Yüksek önem dereceli olayları Ortalama Çözme Süresi. | PagerDuty | < 1 Saat |
| **57** | **MTTD (Detect)** | Kırılmadan alarmın çalmasına kadar geçen süre. | Gözlemlenebilirlik | < 5 Dk |
| **58** | **MTTA (Ack)** | Bir insanın alarmı onaylaması (acknowledge) için geçen süre. | PagerDuty | < 15 Dk |
| **59** | **Availability** | Sistem çalışma süresi % (planlı bakımlar hariç). | Pingdom | > %99.9 |
| **60** | **Error Rate** | 5xx hatası döndüren isteklerin yüzdesi. | APM | < %0.1 |
| **61** | **Latency (P95)** | 95. yüzdelik dilim yanıt süresi. | APM | < 200ms |
| **62** | **Vuln. Resolution** | Kritik güvenlik açıklarını yamama süresi. | Snyk | < 24 Saat |
| **63** | **Policy Compliance** | Statik analiz güvenlik geçitlerini geçen kod oranı. | SonarQube | %100 |
| **64** | **AI BOM Control** | Kullanılan YZ Modeli sürümlerinin ve lisanslarının takibi. | Envanter | %100 |

---

## 8. 🏢 Organizasyonel Hijyen
**Amaç:** Ekibin yapısal sağlığını izlemek.

| # | Metrik | Açıklama ve Bağlam | Veri Kaynağı | Hedef |
| :--- | :--- | :--- | :--- | :--- |
| **65** | **Reviewer Load** | Kişi başı PR incelemesi. Darboğazları ve inceleyen tükenmişliğini önleyin. | Git | Dengeli |
| **66** | **Bus Factor** | Bir projenin durması için ayrılması gereken minimum kişi sayısı. | Git | > 2 |
| **67** | **Doc Freshness** | Dokümantasyonun ortalama yaşı. | Wiki | < 90 Gün |
| **68** | **Dependency Wait** | Diğer ekipler tarafından engellenerek geçirilen süre. | Jira Bağları | Düşük |
| **69** | **Hiring Load** | Mülakat için harcanan müh. saati vs. kodlama. | Takvim | İzleme |
| **70** | **Attrition Risk** | Ayrılma için erken uyarı işaretleri (azalan aktivite). | İK | Düşük |
| **71** | **Maker Ratio** | Yapıcı Süresi (Kodlama) vs. Yönetici Süresi (İdari). | Takvim | > %70 |
| **72** | **Sprint Success** | Sprint HEDEFİNİN tamamlanması (sadece görevlerin değil). | Jira | Yüksek |