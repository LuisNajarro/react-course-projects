using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class OrderItem
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }
}