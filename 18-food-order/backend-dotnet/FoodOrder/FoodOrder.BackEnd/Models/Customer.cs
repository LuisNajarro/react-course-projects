using System.Text.Json.Serialization;

namespace FoodOrder.BackEnd.Models;

public class Customer
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    
    [JsonPropertyName("postal-code")]
    public string? PostalCode { get; set; }
    
    [JsonPropertyName("city")]
    public string? City { get; set; }
}