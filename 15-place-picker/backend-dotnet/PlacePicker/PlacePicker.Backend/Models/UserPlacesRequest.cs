using System.Text.Json.Serialization;

namespace PlacePicker.Backend.Models;

public class UserPlacesRequest
{
    [JsonPropertyName("places")]
    public List<Place> Places { get; set; }
}