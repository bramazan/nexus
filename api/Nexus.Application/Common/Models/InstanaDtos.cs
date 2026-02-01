using System.Text.Json.Serialization;

namespace Nexus.Application.Common.Models
{
    public class InstanaApplication
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
    }

    public class InstanaService
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        
        [JsonPropertyName("types")]
        public List<string> Types { get; set; } = new();
    }

    public class InstanaMetricRequest
    {
        [JsonPropertyName("metrics")]
        public List<string> Metrics { get; set; } = new();
        
        [JsonPropertyName("rollup")]
        public int? Rollup { get; set; } 
    }

    public class InstanaMetricResponse
    {
        [JsonPropertyName("items")]
        public List<InstanaMetricItem> Items { get; set; } = new();
    }

    public class InstanaMetricItem
    {
         [JsonPropertyName("metrics")]
         public Dictionary<string, List<List<double>>> Metrics { get; set; } = new();
    }

    public class InstanaEvent
    {
        [JsonPropertyName("eventId")]
        public string EventId { get; set; } = string.Empty;
        
        [JsonPropertyName("start")]
        public long Start { get; set; }
        
        [JsonPropertyName("end")]
        public long? End { get; set; }
        
        [JsonPropertyName("severity")]
        public int Severity { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty; // issue, incident, change
        
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        
        [JsonPropertyName("entityId")]
        public string EntityId { get; set; } = string.Empty;
    }
}
