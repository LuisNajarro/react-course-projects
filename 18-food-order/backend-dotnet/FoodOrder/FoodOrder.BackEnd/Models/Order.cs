using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class Order
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    
    public Customer? Customer { get; set; }

    public List<OrderItem>? Items { get; set; }
}