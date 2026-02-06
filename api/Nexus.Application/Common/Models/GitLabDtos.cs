using System.Text.Json.Serialization;

namespace Nexus.Application.Common.Models
{
    public class GitLabDeployment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("ref")]
        public string Ref { get; set; } = string.Empty;
        
        [JsonPropertyName("sha")]
        public string Sha { get; set; } = string.Empty;

        [JsonPropertyName("environment")]
        public GitLabEnvironment Environment { get; set; } = new();

        [JsonPropertyName("user")]
        public GitLabUser? User { get; set; }
    }

    public class GitLabEnvironment
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class GitLabJob
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        
        [JsonPropertyName("stage")]
        public string Stage { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("started_at")]
        public DateTime? StartedAt { get; set; }
        
        [JsonPropertyName("finished_at")]
        public DateTime? FinishedAt { get; set; }
        
        [JsonPropertyName("duration")]
        public double? Duration { get; set; }
    }

    public class GitLabMergeRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("iid")]
        public int Iid { get; set; }
        
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonPropertyName("target_branch")]
        public string TargetBranch { get; set; } = string.Empty;
        
        [JsonPropertyName("source_branch")]
        public string SourceBranch { get; set; } = string.Empty;
        
        [JsonPropertyName("author")]
        public GitLabUser Author { get; set; } = new();
        
        [JsonPropertyName("assignee")]
        public GitLabUser? Assignee { get; set; }
        
        [JsonPropertyName("merged_by")]
        public GitLabUser? MergedBy { get; set; }
        
        [JsonPropertyName("merged_at")]
        public DateTime? MergedAt { get; set; }
        
        [JsonPropertyName("closed_by")]
        public GitLabUser? ClosedBy { get; set; }
        
        [JsonPropertyName("closed_at")]
        public DateTime? ClosedAt { get; set; }
    }

    public class GitLabCommit
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("short_id")]
        public string ShortId { get; set; } = string.Empty;
        
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; } = string.Empty;
        
        [JsonPropertyName("author_email")]
        public string AuthorEmail { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; } = string.Empty;
    }

    public class GitLabBranch
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("commit")]
        public GitLabCommit? Commit { get; set; }

        [JsonPropertyName("merged")]
        public bool Merged { get; set; }
        
        [JsonPropertyName("protected")]
        public bool Protected { get; set; }
        
        [JsonPropertyName("default")]
        public bool Default { get; set; }
    }

    public class GitLabMember : GitLabUser
    {
        [JsonPropertyName("access_level")]
        public int AccessLevel { get; set; }
        
        [JsonPropertyName("expires_at")]
        public DateTime? ExpiresAt { get; set; }
    }

    public class GitLabUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;
        
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; } = string.Empty;
    }

    public class GitLabPipeline
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("iid")]
        public int Iid { get; set; }

        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;
        
        [JsonPropertyName("ref")]
        public string Ref { get; set; } = string.Empty;
        
        [JsonPropertyName("sha")]
        public string Sha { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; } = string.Empty;
    }

    public class GitLabIssue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("iid")]
        public int Iid { get; set; }
        
        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonPropertyName("closed_at")]
        public DateTime? ClosedAt { get; set; }
        
        [JsonPropertyName("closed_by")]
        public GitLabUser? ClosedBy { get; set; }
        
        [JsonPropertyName("author")]
        public GitLabUser Author { get; set; } = new();
        
        [JsonPropertyName("assignee")]
        public GitLabUser? Assignee { get; set; }

        [JsonPropertyName("assignees")]
        public List<GitLabUser> Assignees { get; set; } = new();
        
        [JsonPropertyName("due_date")]
        public string? DueDate { get; set; } // YYYY-MM-DD
        
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; } = string.Empty;
    }

    public class GitLabProject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        
        [JsonPropertyName("default_branch")]
        public string DefaultBranch { get; set; } = string.Empty;
        
        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; } = string.Empty;
        
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; } = string.Empty;
        
        [JsonPropertyName("last_activity_at")]
        public DateTime LastActivityAt { get; set; }
    }
    
    public class GitLabRelease
    {
         [JsonPropertyName("name")]
         public string Name { get; set; } = string.Empty;
         
         [JsonPropertyName("tag_name")]
         public string TagName { get; set; } = string.Empty;
         
         [JsonPropertyName("description")]
         public string Description { get; set; } = string.Empty;
         
         [JsonPropertyName("created_at")]
         public DateTime CreatedAt { get; set; }
         
         [JsonPropertyName("released_at")]
         public DateTime ReleasedAt { get; set; }
         
         [JsonPropertyName("author")]
         public GitLabUser Author { get; set; } = new();

         [JsonPropertyName("commit")]
         public GitLabCommit? Commit { get; set; }
    }
}
