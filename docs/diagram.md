// Engineering Intelligence Platform (Nexus) - Database Schema

Table workspaces {
  id uuid [pk]
  name varchar [not null]
  created_at timestamp
}

// --------------------------------------------------------
// 1. Core Identity & Organization
// --------------------------------------------------------

Table users {
  id uuid [pk]
  email varchar [unique]
  full_name varchar
  created_at timestamp
}

Table teams {
  id uuid [pk]
  workspace_id uuid [ref: > workspaces.id]
  name varchar
  parent_team_id uuid [ref: > teams.id]
  created_at timestamp
}

Table team_members {
  team_id uuid [ref: > teams.id]
  user_id uuid [ref: > users.id]
  role varchar
  joined_at timestamp
  indexes {
    (team_id, user_id) [unique]
  }
}

Table tool_accounts {
    id uuid [pk]
    user_id uuid [ref: > users.id]
    integration_id uuid [ref: > integrations.id]
    external_id varchar
    username varchar
    external_email varchar
    display_name varchar
    external_metadata json
    is_active boolean
}

// --------------------------------------------------------
// 2. Integration & Raw Data Layer
// --------------------------------------------------------

Table integrations {
  id uuid [pk]
  workspace_id uuid [ref: > workspaces.id]
  type varchar [note: 'github, jira, linear, etc.']
  name varchar
  config json
  created_at timestamp
}

Table raw_events {
  id uuid [pk]
  integration_id uuid [ref: > integrations.id]
  source varchar [note: 'github, gitlab, jira, instana, sonarqube']
  
  entity_type varchar [note: 'pull_request, issue, deployment, incident, code_quality']
  entity_id varchar [note: 'Source system ID']
  
  payload json [note: 'Original JSON from API']
  
  occurred_at timestamp [note: 'Event time in source']
  ingested_at timestamp [note: 'When we pulled it']
  
  indexes {
    (integration_id, entity_type, entity_id)
  }
}

// --------------------------------------------------------
// 3. Service Catalog & Architecture
// --------------------------------------------------------

Table services {
  id uuid [pk]
  workspace_id uuid [ref: > workspaces.id]
  name varchar
  description text
  owner_team_id uuid [ref: > teams.id]
  tier varchar [note: 'tier-1, tier-2, internal']
  lifecycle varchar [note: 'production, experimental, deprecated']
  created_at timestamp
}

Table repositories {
  id uuid [pk]
  integration_id uuid [ref: > integrations.id]
  name varchar
  external_id varchar
  url varchar
  default_branch varchar
}

Table service_repositories {
  service_id uuid [ref: > services.id]
  repository_id uuid [ref: > repositories.id]
  path_filter varchar [note: 'Glob pattern for monorepo support (e.g. "packages/api/**")']
  
  indexes {
    (service_id, repository_id) [unique]
  }
}

// --------------------------------------------------------
// 4. Source Control & Code Quality
// --------------------------------------------------------

Table branches {
  id uuid [pk]
  repository_id uuid [ref: > repositories.id]
  name varchar
  created_at timestamp
  updated_at timestamp
}

Table commits {
  id uuid [pk]
  repository_id uuid [ref: > repositories.id]
  sha varchar
  author_email varchar
  message text
  committed_at timestamp
  created_at timestamp
}

Table code_changes {
  id uuid [pk]
  commit_id uuid [ref: > commits.id]
  file_path varchar
  lines_added int
  lines_deleted int
  is_refactor boolean [note: 'Heuristic detection or strict formatting update']
  is_rework boolean [note: 'Changes to recently modified code']
}

Table pull_requests {
  id uuid [pk]
  integration_id uuid [ref: > integrations.id]
  repository_id uuid [ref: > repositories.id]
  
  external_id varchar
  number int
  title varchar
  
  author_tool_account_id uuid [ref: > tool_accounts.id]
  
  state varchar [note: 'open, closed, merged']
  created_at timestamp
  merged_at timestamp
  closed_at timestamp
  
  size_lines int [note: 'additions + deletions']
  is_ai_generated boolean
  
  review_count int
  cycle_time_minutes int [note: 'Calculated value']
}

Table pull_request_reviews {
  id uuid [pk]
  pull_request_id uuid [ref: > pull_requests.id]
  author_tool_account_id uuid [ref: > tool_accounts.id]
  state varchar [note: 'approved, changes_requested, commented, dismissed']
  submitted_at timestamp
  
  indexes {
    (pull_request_id, author_tool_account_id)
  }
}

Table ai_signals {
  id uuid [pk]
  pull_request_id uuid [ref: > pull_requests.id]
  ai_type varchar [note: 'agentic, assisted, none']
  tool_name varchar [note: 'copilot, devin, cursor']
  detected_at timestamp
}

// --------------------------------------------------------
// 5. Project Management (Issues & Sprints)
// --------------------------------------------------------

Table sprints {
  id uuid [pk]
  integration_id uuid [ref: > integrations.id]
  name varchar
  status varchar [note: 'active, future, closed']
  start_date date
  end_date date
}

Table issues {
  id uuid [pk]
  integration_id uuid [ref: > integrations.id]
  external_id varchar
  title varchar
  description text
  
  status varchar [note: 'todo, in_progress, done']
  type varchar [note: 'bug, story, task']
  priority varchar [note: 'p0, p1, p2']
  
  parent_issue_id uuid [ref: > issues.id]
  
  created_at timestamp
  started_at timestamp
  completed_at timestamp
}

Table issue_events {
  id uuid [pk]
  issue_id uuid [ref: > issues.id]
  actor_tool_account_id uuid [ref: > tool_accounts.id]
  event_type varchar [note: 'status_change, assignee_change, priority_change']
  
  previous_value varchar
  new_value varchar
  
  occurred_at timestamp
}

Table issue_estimates {
  id uuid [pk]
  issue_id uuid [ref: > issues.id]
  estimate_value float
  estimate_unit varchar [note: 'points, hours']
  created_at timestamp
}

Table issue_assignees {
  issue_id uuid [ref: > issues.id]
  user_id uuid [ref: > users.id]
  assigned_at timestamp
  indexes {
    (issue_id, user_id) [unique]
  }
}

Table issue_sprint_links {
  issue_id uuid [ref: > issues.id]
  sprint_id uuid [ref: > sprints.id]
  added_at timestamp [note: 'To calculate scope creep']
  indexes {
    (issue_id, sprint_id) [unique]
  }
}

Table branch_issue_links {
  branch_id uuid [ref: > branches.id]
  issue_id uuid [ref: > issues.id]
  indexes {
    (branch_id, issue_id) [unique]
  }
}

// --------------------------------------------------------
// 6. DevOps & Reliability (DORA Metrics)
// --------------------------------------------------------

Table deployments {
  id uuid [pk]
  service_id uuid [ref: > services.id]
  environment varchar [note: 'prod, staging']
  
  started_at timestamp [note: 'Pipeline start time']
  deployed_at timestamp [note: 'Successful deployment time']
  
  status varchar [note: 'success, failed, cancelled']
  commit_id uuid [ref: > commits.id]
  trigger_actor_id uuid [ref: > users.id]
}

Table releases {
  id uuid [pk]
  service_id uuid [ref: > services.id]
  version varchar
  released_at timestamp
  description text
}

Table incidents {
  id uuid [pk]
  service_id uuid [ref: > services.id]
  title varchar
  severity varchar [note: 'sev1, sev2, sev3']
  
  start_time timestamp
  detected_at timestamp [note: 'For MTTD']
  acknowledged_at timestamp [note: 'For MTTA']
  end_time timestamp
  
  root_cause_commit_id uuid [ref: > commits.id]
  status varchar [note: 'open, investigating, resolved']
}

Table user_availability {
  id uuid [pk]
  user_id uuid [ref: > users.id]
  start_date date
  end_date date
  type varchar [note: 'vacation, sick, parental, sabbatical']
  reason text
}

Table metric_thresholds {
    id uuid [pk]
    metric_name varchar
    segment varchar [note: 'startup, enterprise, default']
    min_value double
    max_value double
    level varchar [note: 'Elite, High, Medium, Low']
    created_at timestamp
}