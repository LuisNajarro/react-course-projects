using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class EventsResponse
{
    [JsonPropertyName("events")]
    public List<Event>? Events { get; set; }
}