using System.Text.Json.Serialization;

namespace Events.Backend.Data;

public class Errors
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("date")]
    public string? Date { get; set; }
    
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}