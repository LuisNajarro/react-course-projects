using System.Text.Json.Serialization;

namespace Events.Backend.Data;

public class Image
{
    [JsonPropertyName("path")]
    public string? Path { get; set; }
    
    [JsonPropertyName("caption")]
    public string? Caption { get; set; }
}