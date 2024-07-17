using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class ErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public Errors? Errors { get; set; }
}