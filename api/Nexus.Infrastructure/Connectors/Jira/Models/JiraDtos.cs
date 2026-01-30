using System.Text.Json.Serialization;

namespace Nexus.Infrastructure.Connectors.Jira.Models
{
    public class JiraProject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class JiraIssueSearchResponse
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
        
        [JsonPropertyName("issues")]
        public List<JiraIssue> Issues { get; set; } = new();
    }

    public class JiraIssue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
        
        [JsonPropertyName("fields")]
        public JiraIssueFields Fields { get; set; } = new();
    }

    public class JiraIssueFields
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;
        
        [JsonPropertyName("status")]
        public JiraStatus Status { get; set; } = new();
        
        [JsonPropertyName("priority")]
        public JiraPriority? Priority { get; set; }
        
        [JsonPropertyName("issuetype")]
        public JiraIssueType IssueType { get; set; } = new();

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        // Custom fields can be added here as needed
    }

    public class JiraStatus
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("statusCategory")]
        public JiraStatusCategory? StatusCategory { get; set; }
    }

    public class JiraStatusCategory
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty; // new, indeterminate, done
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class JiraPriority
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class JiraIssueType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("subtask")]
        public bool Subtask { get; set; }
    }

    // New DTOs for Extended Coverage
    
    public class JiraBoardResponse
    {
        [JsonPropertyName("values")]
        public List<JiraBoard> Values { get; set; } = new();
    }

    public class JiraBoard
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty; // scrum, kanban
    }

    public class JiraSprintResponse
    {
        [JsonPropertyName("values")]
        public List<JiraSprint> Values { get; set; } = new();
    }

    public class JiraSprint
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty; // active, closed, future
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }
        
        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonPropertyName("completeDate")]
        public DateTime? CompleteDate { get; set; }
    }
    
    public class JiraUser
    {
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; } = string.Empty;
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        
        [JsonPropertyName("emailAddress")]
        public string EmailAddress { get; set; } = string.Empty;
        
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }

    // Changelog DTOs
    public class JiraIssueWithChangelog : JiraIssue
    {
        [JsonPropertyName("changelog")]
        public JiraChangelog Changelog { get; set; } = new();
    }

    public class JiraChangelog
    {
        [JsonPropertyName("histories")]
        public List<JiraChangelogHistory> Histories { get; set; } = new();
    }

    public class JiraChangelogHistory
    {
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        
        [JsonPropertyName("author")]
        public JiraUser Author { get; set; } = new();
        
        [JsonPropertyName("items")]
        public List<JiraChangelogItem> Items { get; set; } = new();
    }

    public class JiraChangelogItem
    {
        [JsonPropertyName("field")]
        public string Field { get; set; } = string.Empty;
        
        [JsonPropertyName("fromString")]
        public string FromString { get; set; }
        
        [JsonPropertyName("toString")]
        public string ToString { get; set; }
    }
}
