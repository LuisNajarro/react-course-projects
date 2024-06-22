using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class Meal
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("price")]
    public string? Price { get; set; }
    
    [JsonPropertyName("")]
    public string? Description { get; set; }
    
    [JsonPropertyName("")]
    public string? Image { get; set; }
}