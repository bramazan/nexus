# Nexus 2026: Raw Data & ELT Checklist

Bu liste, Nexus 2026 projesi için ELT (Extract-Load-Transform) sürecinin "Extract & Load" aşamalarını takip etmek için hazırlanmıştır. `data.md` ve `diagram.md` dosyalarındaki veri gereksinimlerini karşılamak ve "Agentic AI" yeteneklerini desteklemek için bu kontrol listesi kullanılacaktır.

> **Durum İşaretleri:**
> - [x] **Tamamlandı:** Altyapısı kuruldu, veri çekildi ve doğrulandı.
> - [ ] **Bekliyor:** Henüz entegrasyonu yapılmadı.

---

## 📋 Bölüm 1: GitLab Entegrasyonu (Kaynak Kod & CI/CD)
Kod yaşam döngüsü, AI katkısı ve DORA metrikleri için ana kaynak.

| Durum | Sıra | Veri Nesnesi | Çekilecek API Detayı (Örnek) | Entity Type | Kritik Veri Alanları |
| :---: | :--- | :--- | :--- | :--- | :--- |
| [x] | 1.1 | **Commits** | `/projects/:id/repository/commits` | `commits` | author_email, committed_date, message, stats. |
| [x] | 1.2 | **Merge Requests (MRs)** | `/projects/:id/merge_requests` | `pull_request` | created_at, merged_at, source_branch, target_branch, description. |
| [ ] | 1.3 | **MR Diffs / Changes** | `/merge_requests/:iid/changes` | `code_change` | file_path, **diff/patch** içeriği. |
| [ ] | 1.4 | **Discussions (Reviews)** | `/merge_requests/:iid/notes` | `review` | body, created_at, author. |
| [ ] | 1.5 | **Approvals** | `/merge_requests/:iid/approvals` | `review_approval` | Onaylayan kişi, zaman damgası. |
| [ ] | 1.6 | **Pipelines / Jobs** | `/projects/:id/pipelines` | `deployment` | status, duration, sha. |
| [ ] | 1.7 | **Deployments** | `/projects/:id/deployments` | `deployment_event` | environment, status, deployable, created_at. |
| [ ] | 1.8 | **Releases / Tags** | `/projects/:id/releases` | `release` | tag_name, description, released_at, commit. |

---

## 📋 Bölüm 2: Jira Entegrasyonu (Planlama & Akış)
İşin "State" değişimlerini ve planlama doğruluğunu ölçmek için.

| Durum | Sıra | Veri Nesnesi | Çekilecek API Detayı (Örnek) | Entity Type | Kritik Veri Alanları |
| :---: | :--- | :--- | :--- | :--- | :--- |
| [ ] | 2.1 | **Issues (Tasks/Bugs)** | `/search?jql=updated>last_sync` | `issue` | issuetype, priority, created, updated, assignee, status. |
| [ ] | 2.2 | **Issue Changelog** | `/issue/:id/changelog` | `issue_event` | fromString -> toString, created (Statü değişim tarihçesi). |
| [ ] | 2.3 | **Sprints** | `/board/:id/sprint` | `sprint` | startDate, endDate, completeDate. |
| [ ] | 2.4 | **Sprint Issues** | `/sprint/:id/issue` | `sprint_link` | İşin sprinte eklenme tarihi (Scope Creep tespiti). |
| [ ] | 2.5 | **Links (Bağımlılıklar)** | `/issue/:id` (Links field) | `dependency` | "Blocked by", "Relates to". |

---

## 📋 Bölüm 3: Instana & SRE (Operasyonel Sağlık)
Sistem sağlığı ve DORA (MTTR, MTTD) metrikleri için.

| Durum | Sıra | Veri Nesnesi | Çekilecek Veri (Örnek) | Entity Type | Kritik Veri Alanları |
| :---: | :--- | :--- | :--- | :--- | :--- |
| [ ] | 3.1 | **Events / Incidents** | Events API | `incident` | start_time, text, problem_id. |
| [ ] | 3.2 | **Application Vitals** | Application Metrics | `service_health` | latency, error_rate, throughput. |
| [ ] | 3.3 | **Trace Root Causes** | Snapshot / Trace | `root_cause` | Hata kaynağı (Commit Hash / Service Name). |

---

## 📋 Bölüm 4: SonarQube & Güvenlik (Kalite & Risk)
Kod kalitesi ve güvenlik açıklarını izlemek için.

| Durum | Sıra | Veri Nesnesi | Çekilecek Veri (Örnek) | Entity Type | Kritik Veri Alanları |
| :---: | :--- | :--- | :--- | :--- | :--- |
| [ ] | 4.1 | **Project Measures** | Measures API | `code_quality` | bugs, vulnerabilities, code_smells, coverage. |
| [ ] | 4.2 | **Quality Gates** | Quality Gate Status | `compliance` | status (Passed/Failed). |

---

## 📋 Bölüm 5: İK & Organizasyon (İnsan Faktörü)
Metrik normalizasyonu ve Developer Experience için.

| Durum | Sıra | Veri Nesnesi | Çekilecek Veri (Örnek) | Entity Type | Kritik Veri Alanları |
| :---: | :--- | :--- | :--- | :--- | :--- |
| [ ] | 5.1 | **User Availability** | HR System / Calendar | `availability` | İzinler, resmi tatiller. |
| [ ] | 5.2 | **Team Roster** | HR System / LDAP | `team_sync` | Takım üyelikleri, katılış/ayrılış tarihleri. |

---

### 🛠️ Uygulama İlkeleri
1.  **Ham Veri Saklama:** `raw_events.payload` (JSONB) alanına veriyi olduğu gibi kaydedin. Filtreleme yapmayın.
2.  **Incremental Sync:** Sadece son senkronizasyondan sonraki verileri çekmeye çalışın (`last_sync_date`).
3.  **Hata Yönetimi:** Backoff stratejisi uygulayın.
4.  **Eşleştirme:** `entity_id` alanına her zaman kaynak sistemdeki ID'yi (GitLab MR ID: 12345 vb.) kaydedin.