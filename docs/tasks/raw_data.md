Nexus 2026 projesi için ELT (Extract-Load-Transform) mimarisinin "Extract & Load" (Çıkarma ve Yükleme) aşamasını yönetecek detaylı ham veri (raw data) kontrol listesi aşağıdadır.
Bu liste, data.md ve diagram.md dosyalarındaki raw_events tablosunu beslemek ve Nexus 2026 vizyonundaki "Agentic AI" ve "Engineering Intelligence" yeteneklerini desteklemek için gereken tüm veri noktalarını kapsar.
📋 Bölüm 1: GitLab Entegrasyonu (Kaynak Kod & CI/CD)
Kodun yaşam döngüsü, AI katkısı ve DORA metrikleri için ana kaynaktır.
Sıra
Veri Nesnesi
Çekilecek API Detayı (Örnek)
raw_events Entity Type
Kritik Veri Alanları (Payload İçinde Olmalı)
Kaynak Referansı
1.1
Commits
/projects/:id/repository/commits
commit
author_email (kişi eşleşmesi), committed_date (kodlama süresi), message (AI imzası tespiti), stats (eklenen/silinen satır).
,,
1.2
Merge Requests (MRs)
/projects/:id/merge_requests
pull_request
created_at, merged_at, source_branch, target_branch, description (AI özetleri için).
,
1.3
MR Diffs / Changes
/merge_requests/:iid/changes
code_change
Dosya yolları (file_path) VE **diff/patch** içeriği. "Agentic AI" ile kod review ve refactor önerileri yapabilmek için kodun nasıl değiştiğini (diff payload) mutlaka saklamalıyız.
,
1.4
Discussions (Reviews)
/merge_requests/:iid/notes
review
body (yorum içeriği), created_at (ilk yorum zamanı - Pickup Time hesabı için), author.
,
1.5
Approvals
/merge_requests/:iid/approvals
review_approval
Kimin onayladığı ve onayın zaman damgası. Reviewer Load ve darboğaz analizi için.
,
1.6
Pipelines / Jobs
/projects/:id/pipelines
deployment
status (success/failed), duration, sha (hangi commit deploy oldu). Change Failure Rate (CFR) için temel kaynak.
,
1.7
Deployments
/projects/:id/deployments
deployment_event
environment, status, deployable (commit/tag), created_at. Pipeline'dan daha kesin sonuç verir. DORA Deployment Frequency için "production" ortamı filtrelemesi buradan yapılır.
,
1.8
Releases / Tags
/projects/:id/releases
release
tag_name, description, released_at, commit. Incident ile sürüm eşleştirmesi ve "Change Failure Rate" analizi için versiyon bilgisi şarttır.
,

--------------------------------------------------------------------------------
📋 Bölüm 2: Jira Entegrasyonu (Planlama & Akış)
İşin "State" değişimlerini ve planlama doğruluğunu ölçmek için kullanılır.
Sıra
Veri Nesnesi
Çekilecek API Detayı (Örnek)
raw_events Entity Type
Kritik Veri Alanları (Payload İçinde Olmalı)
Kaynak Referansı
2.1
Issues (Tasks/Bugs)
/search?jql=updated>last_sync
issue
issuetype (Bug/Story), priority, created, updated, assignee, status.
,
2.2
Issue Changelog
/issue/:id/changelog
issue_event
Çok Kritik: Statü değişiklik tarihçesi (fromString -> toString ve created). Flow Efficiency ve bekleme süreleri buradan hesaplanır.
,
2.3
Sprints
/board/:id/sprint
sprint
startDate, endDate, completeDate. Planlama doğruluğu (Planning Accuracy) için.
,
2.4
Sprint Issues
/sprint/:id/issue
sprint_link
İşin sprinte ne zaman eklendiği bilgisi. Sprint başladıktan sonra eklenen işler "Scope Creep" (Kapsam Sapması) olarak işaretlenir.
,
2.5
Links (Bağımlılıklar)
/issue/:id (Links field)
dependency
"Blocked by" veya "Relates to" ilişkileri. Takımlar arası bağımlılıkları (Dependency Wait) ölçmek için.

--------------------------------------------------------------------------------
📋 Bölüm 3: Instana & SRE (Operasyonel Sağlık)
DORA metriklerinden MTTR ve MTTD hesaplamaları ile sistem sağlığını izlemek için.
Sıra
Veri Nesnesi
Çekilecek Veri (Örnek)
raw_events Entity Type
Kritik Veri Alanları (Payload İçinde Olmalı)
Kaynak Referansı
3.1
Events / Incidents
Events API
incident
start_time (Olay başlangıcı), text (Hata mesajı), problem_id. MTTD (Tespit Süresi) hesabı için.
,
3.2
Application Vitals
Application Metrics
service_health
latency, error_rate, throughput. DORA metrikleri dışındaki operasyonel sağlık (Latency P95 vb.) için.
,
3.3
Trace Root Causes
Snapshot / Trace
root_cause
Hatanın kaynağını koda veya altyapıya bağlayan veriler (Commit Hash veya Service Name).
,

--------------------------------------------------------------------------------
📋 Bölüm 4: SonarQube & Güvenlik (Kalite & Risk)
Kodun sadece hızlı değil, güvenli ve temiz yazıldığını doğrulamak için.
Sıra
Veri Nesnesi
Çekilecek Veri (Örnek)
raw_events Entity Type
Kritik Veri Alanları (Payload İçinde Olmalı)
Kaynak Referansı
4.1
Project Measures
Measures API
code_quality
bugs, vulnerabilities, code_smells, coverage. Defect Density ve Tech Debt Ratio hesabı için.
,
4.2
Quality Gates
Quality Gate Status
compliance
status (Passed/Failed). Güvenlik politikalarına uyum (Policy Compliance) oranı için.
,

--------------------------------------------------------------------------------
📋 Bölüm 5: İK & Organizasyon (İnsan Faktörü)
Metrikleri normalize etmek ve "Developer Experience"ı korumak için.
Sıra
Veri Nesnesi
Çekilecek Veri (Örnek)
raw_events Entity Type
Kritik Veri Alanları (Payload İçinde Olmalı)
Kaynak Referansı
5.1
User Availability
HR System / Calendar
availability
İzin günleri, resmi tatiller. Cycle Time hesaplanırken bu günler süreden düşülmelidir (exclude).
5.2
Team Roster
HR System / LDAP
team_sync
Güncel takım üyelikleri. Kişi takımdan ayrıldıysa veya yeni geldiyse (Onboarding Speed) tespiti için.
,
🛠️ Uygulama İçin Teknik İpuçları
1. JSON Payload: Tüm veriyi olduğu gibi (ham haliyle) raw_events.payload (JSONB) alanına kaydedin. Veri modelinizde herhangi bir filtreleme veya dönüştürme yapmayın. Bu, gelecekte farklı metrikler hesaplamak isterseniz (örneğin: AI Agentic PR Rate) geçmiş veriyi yeniden işlemenizi sağlar.
2. Incremental Sync (Artımlı Senkronizasyon): Her çalıştırmada sadece last_sync_date'den sonraki verileri çekin. Ancak issue_changelog veya comments gibi alt verilerde güncelleme kaçırmamak için ana nesne güncellendiyse detaylarını da tekrar çekmeyi düşünün.
3. Rate Limiting: GitLab ve Jira API'leri için "Backoff" stratejisi (hata aldığında bekle ve tekrar dene) uygulayın.
4. Veri Eşleştirme Anahtarları: raw_events tablosunda entity_id alanına mutlaka kaynak sistemdeki orijinal ID'yi (örn. GitLab MR ID: 12345) yazın. Bu, tablosundaki external_id ile eşleşecektir.