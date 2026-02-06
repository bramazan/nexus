# Nexus Implementation Checklist

## Phase 1: Infrastructure & Connectivity (Integration Layer)
Goal: Establish communication with external tools (GitLab, Jira, Instana).

- [x] **1. Define Workspace**
    - [x] Create default workspace ("Nexus Engineering") in `ApplicationDbContextInitialiser`.
    - [x] Verify `workspaces` table.

- [x] **2. Define Integrations**
    - [x] Implement `GitLabConnector` (Projects, MRs, Commits, Pipelines, Jobs).
    - [x] Implement `JiraConnector` (Issues, Boards, Sprints, Changelogs).
    - [x] Implement `InstanaConnector` (Services, Metrics, Events).
    - [x] Seed dummy integration data in `ApplicationDbContextInitialiser`.
    - [ ] **Action Required:** Update `integrations` table with REAL API Keys/Tokens.

## Phase 2: Organization & Identity Mapping (Human Layer)
Goal: Map "Who did what?" across different tools.

- [x] **3. Create Users**
    - [x] Seed default Admin user.
    - [ ] Create endpoint/script to import users from LDAP/AD or CSV.

- [ ] **4. Build Team Hierarchy**
    - [ ] Create `Team` entities (e.g., Backend, Frontend).
    - [ ] Define Parent/Child relationships between teams.

- [ ] **5. Assign Team Members**
    - [ ] Link `Users` to `Teams` with roles (Lead, Developer).

- [ ] **6. Map Tool Accounts (CRITICAL)**
    - [ ] Create `ToolAccount` mapping logic.
    - [ ] **Task:** Map `gitlab_username` -> `user_id`.
    - [ ] **Task:** Map `jira_account_id` -> `user_id`.
    - *Why?* Required for DORA metrics to attribute commits/issues to humans.

## Phase 3: Project & Asset Discovery (Asset Layer)
Goal: Define where the code and work actually lives.

- [ ] **7. Repository Discovery**
    - [x] Logic in `GitLabConnector.SyncProjectsAsync` (Basic implementation exists).
    - [ ] Extend logic to auto-link Repositories to Integrations during sync.

- [ ] **8. Service Catalog Definition**
    - [x] Create `Service` entities (e.g., "Payment API").
    - [ ] Assign `Service` ownership to `Teams`.
    - [ ] Link `Service` to `Repository` via `ServiceRepository` table.

- [ ] **9. Jira Project/Board Import**
    - [ ] Sync logic for `Boards` and `Sprints` from Jira to DB.

## Phase 4: Intelligence & Rules (Configuration Layer)
Goal: Teach the system what "Good" looks like.

- [ ] **10. Define Metric Thresholds**
    - [ ] Create `MetricThreshold` entity/table (Missing in current Domain?).
    - [ ] Seed default DORA thresholds (e.g., Elite < 1h Cycle Time).

- [ ] **11. User Availability**
    - [ ] Endpoint to manage `UserAvailability` (Vacations/Holidays).

## Phase 5: Data Engine (ETL & Logic) (CURRENT PHASE)
Goal: Process raw data into standard tables.

- [ ] **12. Sync Engine Implementation**
    - [ ] Create `SyncController` or `WorkerService`.
    - [x] **GitLab Sync Handler:** Fetch -> Map Users -> Save to `PullRequests`, `Commits`.
    - [ ] **Jira Sync Handler:** Fetch -> Map Users -> Save to `Issues`, `Sprints`.
    - [x] **Instana Sync Handler:** Fetch -> Save to `Incidents`, `Deployments`.

## Phase 6: Visualization
Goal: Show the insights.

- [ ] **13. API Endpoints for Dashboard**
    - [ ] `GET /metrics/dora`
    - [ ] `GET /metrics/velocity`
