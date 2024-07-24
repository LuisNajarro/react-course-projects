using System.Text.Json.Serialization;

namespace Events.Backend.Data;

public class EventsList
{
    [JsonPropertyName("events")]
    public List<Event>? Events { get; set; }
}