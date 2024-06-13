using System.Text.Json.Serialization;

namespace PlacePicker.Backend.Models;

public class Place
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("image")]
    public Image? Image { get; set; }

    [JsonPropertyName("lat")]
    public decimal? Lat { get; set; }

    [JsonPropertyName("lon")]
    public decimal? Lon { get; set; }
}