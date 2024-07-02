using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class OrderRequest
{
    [JsonPropertyName("order")]
    public Order? Order { get; set; }
}