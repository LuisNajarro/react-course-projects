using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class EventResponse
{
    [JsonPropertyName("event")]
    public Event? Event { get; set; }
    
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}