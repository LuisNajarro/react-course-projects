using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class OrderResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; }
}