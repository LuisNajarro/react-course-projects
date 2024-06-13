using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PlacePicker.Backend.Models;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.MapGet("/places", async () =>
    {
        var fileContent = await File.ReadAllTextAsync("./data/places.json");

        var placesData = JsonSerializer.Deserialize<List<Place>>(fileContent);

        return Results.Ok(new { places = placesData });
    })
    .WithName("get-places")
    .WithOpenApi();

app.MapGet("/user-places", async () =>
    {
        var fileContent = await File.ReadAllTextAsync("./data/user-places.json");

        var places = JsonSerializer.Deserialize<List<Place>>(fileContent);

        return Results.Ok(new { places });
    })
    .WithName("get-user-places")
    .WithOpenApi();

app.MapPut("/user-places", async ([FromBody] List<Place> places) =>
    {
        var placesContent = JsonSerializer.Serialize(places);
        
        await File.WriteAllTextAsync("./data/user-places.json", placesContent);

        return Results.Ok(new { message = "User places updated!" });
    })
    .WithName("put-user-places")
    .WithOpenApi();

await app.RunAsync();