using System.Text.Json.Serialization;

namespace Events.Backend.Models;

public class EventErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}