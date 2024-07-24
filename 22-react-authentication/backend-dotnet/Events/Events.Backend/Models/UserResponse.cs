using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class UserResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    
    [JsonPropertyName("user")]
    public User? User { get; set; }
    
    [JsonPropertyName("token")]
    public string? Token { get; set; }
}