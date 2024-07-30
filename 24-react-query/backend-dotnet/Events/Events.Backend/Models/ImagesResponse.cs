using System.Text.Json.Serialization;
using Events.Backend.Data;

namespace Events.Backend.Models;

public class ImagesResponse
{
    [JsonPropertyName("images")]
    public List<Image>? Images { get; set; }
}