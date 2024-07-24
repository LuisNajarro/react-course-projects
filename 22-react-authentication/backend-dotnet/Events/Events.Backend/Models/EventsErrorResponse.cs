using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class EventsErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public EventErrors? Errors { get; set; }
}