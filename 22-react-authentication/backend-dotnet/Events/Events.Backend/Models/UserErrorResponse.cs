using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class UserErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public UserErrors? Errors { get; set; }
}