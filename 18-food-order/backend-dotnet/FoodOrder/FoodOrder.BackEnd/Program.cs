using System.Text.Json;
using FoodOrder.BackEnd.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "wwwroot/images"
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .WithMethods(HttpMethods.Get, HttpMethods.Put)
            .WithHeaders(HeaderNames.ContentType);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = StatusCodes.Status200OK;
        return;
    }

    await next.Invoke();
});

app.MapGet("/meals", async () =>
    {
        var mealsJson = await File.ReadAllTextAsync("./data/available-meals.json");
        var meals = JsonSerializer.Deserialize<List<Meal>>(mealsJson);
        return TypedResults.Ok(meals);
    })
    .WithName("get-meals")
    .WithOpenApi();

app.MapPost("/orders", async Task<Results<Created<OrderResponse>, BadRequest<OrderResponse>>> ([FromBody] OrderRequest? request) =>
    {
        var order = request?.Order;
        
        if (order?.Items is null || order.Items.Count == 0)
        {
            return TypedResults.BadRequest(new OrderResponse { Message = "Missing data." });
        }

        if (order.Customer?.Email is null ||
            !order.Customer.Email.Contains('@') ||
            string.IsNullOrWhiteSpace(order.Customer.Email) ||
            string.IsNullOrWhiteSpace(order.Customer.Name) ||
            string.IsNullOrWhiteSpace(order.Customer.Street) ||
            string.IsNullOrWhiteSpace(order.Customer.PostalCode) ||
            string.IsNullOrWhiteSpace(order.Customer.City))
        {
            return TypedResults.BadRequest(new OrderResponse
                { Message = "Missing data: Email, name, street, postal code or city is missing." });
        }

        var newOrder = new Order
        {
            Id = new Random().Next(0, 1000).ToString(),
            Customer = order.Customer,
            Items = order.Items
        };
        var allOrdersJson = await File.ReadAllTextAsync("./data/orders.json");
        var allOrders = JsonSerializer.Deserialize<List<Order>>(allOrdersJson) ?? [];
        allOrders.Add(newOrder);
        await File.WriteAllTextAsync("./data/orders.json", JsonSerializer.Serialize(allOrders));

        return TypedResults.Created((string?)null, new OrderResponse { Message = "Order created!" });
    })
    .WithName("post-orders")
    .WithOpenApi();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { Message = "Not found" });
    }
});

await app.RunAsync();