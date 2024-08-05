using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class EventRequest
{
    [JsonPropertyName("event")]
    public Event? Event { get; set; }
}