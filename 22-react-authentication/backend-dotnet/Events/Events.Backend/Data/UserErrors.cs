using System.Text.Json.Serialization;

namespace Events.Backend.Data;

public class UserErrors
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("password")]
    public string? Password { get; set; }
    
    [JsonPropertyName("credentials")]
    public string? Credentials { get; set; }
}