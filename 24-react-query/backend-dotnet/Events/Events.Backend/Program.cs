using System.Globalization;
using System.Text.Json;
using Events.Backend.Data;
using Events.Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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
            .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete, HttpMethods.Options)
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

app.UseStaticFiles();

app.UseCors();

app.MapGet("/events", async (int? max, string? search) =>
{
    var eventsFileContent = await File.ReadAllTextAsync("./Data/events.json");
    var events = JsonSerializer.Deserialize<List<Event>>(eventsFileContent);

    if (search is not null)
    {
        events = events?.Where(e =>
        {
            var searchableText = $"{e.Title} {e.Description} {e.Location}";
            return searchableText.ToLower().Contains(search.ToLower());
        }).ToList();
    }

    if (max is not null)
    {
        events = events?.Take(max.Value).ToList();
    }

    return TypedResults.Ok(new EventsResponse
    {
        Events = events?.Select(e => new Event
        {
            Id = e.Id,
            Title = e.Title,
            Image = e.Image,
            Date = e.Date,
            Location = e.Location
        }).ToList()
    });
});

app.MapGet("/events/images", async () =>
{
    var imagesFileContent = await File.ReadAllTextAsync("./Data/images.json");
    var images = JsonSerializer.Deserialize<List<Image>>(imagesFileContent);

    return TypedResults.Ok(new ImagesResponse
    {
        Images = images
    });
});

app.MapGet("/events/{id}", async Task<Results<Ok<EventResponse>, NotFound<EventErrorResponse>>> (string id) =>
{
    var eventsFileContent = await File.ReadAllTextAsync("./Data/events.json");
    var events = JsonSerializer.Deserialize<List<Event>>(eventsFileContent);

    var @event = events?.Find(e => e.Id == id);

    if (@event is null)
    {
        return TypedResults.NotFound(new EventErrorResponse { Message = $"For the id {id}, no event could be found." });
    }

    await Task.Delay(TimeSpan.FromSeconds(1));
    return TypedResults.Ok(new EventResponse { Event = @event });
});

app.MapPost("/events",
    async Task<Results<Ok<EventResponse>, BadRequest<EventErrorResponse>>> ([FromBody] EventRequest? request) =>
    {
        var @event = request?.Event;
        if (@event is null)
        {
            return TypedResults.BadRequest(new EventErrorResponse { Message = "Event is required" });
        }

        if (string.IsNullOrEmpty(@event.Title) ||
            string.IsNullOrEmpty(@event.Description) ||
            string.IsNullOrEmpty(@event.Date) ||
            string.IsNullOrEmpty(@event.Time) ||
            string.IsNullOrEmpty(@event.Image) ||
            string.IsNullOrEmpty(@event.Location))
        {
            return TypedResults.BadRequest(new EventErrorResponse { Message = "Invalid data provided." });
        }

        var eventsFileContent = await File.ReadAllTextAsync("./Data/events.json");
        var events = JsonSerializer.Deserialize<List<Event>>(eventsFileContent);

        var newEvent = new Event
        {
            Id = Math.Round(new Random().NextDouble() * 10000).ToString(CultureInfo.InvariantCulture),
            Title = @event.Title,
            Description = @event.Description,
            Date = @event.Date,
            Time = @event.Time,
            Image = @event.Image,
            Location = @event.Location
        };

        events?.Add(newEvent);

        await File.WriteAllTextAsync("./Data/events.json", JsonSerializer.Serialize(events));

        return TypedResults.Ok(new EventResponse { Event = newEvent });
    });

app.MapPut("/events/{id}",
    async Task<Results<Ok<EventResponse>, BadRequest<EventErrorResponse>, NotFound<EventErrorResponse>>> (string id,
        [FromBody] EventRequest? request) =>
    {
        var @event = request?.Event;
        if (@event is null)
        {
            return TypedResults.BadRequest(new EventErrorResponse { Message = "Event is required" });
        }

        if (string.IsNullOrEmpty(@event.Title) ||
            string.IsNullOrEmpty(@event.Description) ||
            string.IsNullOrEmpty(@event.Date) ||
            string.IsNullOrEmpty(@event.Time) ||
            string.IsNullOrEmpty(@event.Image) ||
            string.IsNullOrEmpty(@event.Location))
        {
            return TypedResults.BadRequest(new EventErrorResponse { Message = "Invalid data provided." });
        }

        var eventsFileContent = await File.ReadAllTextAsync("./Data/events.json");
        var events = JsonSerializer.Deserialize<List<Event>>(eventsFileContent);

        var eventIndex = events?.FindIndex(e => e.Id == id);

        if (eventIndex is null or -1)
        {
            return TypedResults.NotFound(new EventErrorResponse { Message = "Event not found" });
        }

        events![eventIndex.Value] = new Event
        {
            Id = id,
            Title = @event.Title,
            Description = @event.Description,
            Date = @event.Date,
            Time = @event.Time,
            Image = @event.Image,
            Location = @event.Location
        };

        await File.WriteAllTextAsync("./Data/events.json", JsonSerializer.Serialize(events));

        await Task.Delay(TimeSpan.FromSeconds(1));
        return TypedResults.Ok(new EventResponse { Event = events[eventIndex.Value] });
    });

app.MapDelete("/events/{id}", async  Task<Results<Ok<EventResponse>, NotFound<EventErrorResponse>>> (string id) =>
{
    var eventsFileContent = await File.ReadAllTextAsync("./Data/events.json");
    var events = JsonSerializer.Deserialize<List<Event>>(eventsFileContent);

    var eventIndex = events?.FindIndex(e => e.Id == id);
    
    if (eventIndex is null or -1)
    {
        return TypedResults.NotFound(new EventErrorResponse { Message = "Event not found" });
    }
    
    events!.RemoveAt(eventIndex.Value);
    
    await File.WriteAllTextAsync("./Data/events.json", JsonSerializer.Serialize(events));
    
    await Task.Delay(TimeSpan.FromSeconds(1));
    return TypedResults.Ok(new EventResponse { Message = "Event deleted" });
});

await app.RunAsync();
