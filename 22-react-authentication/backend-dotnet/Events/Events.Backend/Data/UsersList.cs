using System.Text.Json.Serialization;

namespace Events.Backend.Data;

public class UsersList
{
    [JsonPropertyName("users")]
    public List<User>? Users { get; set; }
}