using System.Text.Json.Serialization;

namespace PlacePicker.Backend.Models;

public class Image
{
    [JsonPropertyName("src")]
    public string? Src { get; set; }

    [JsonPropertyName("alt")]
    public string? Alt { get; set; }
}