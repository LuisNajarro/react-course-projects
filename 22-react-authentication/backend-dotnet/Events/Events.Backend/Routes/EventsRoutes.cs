using Events.Backend.Data;
using Events.Backend.Models;
using Events.Backend.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Events.Backend.Routes;

public static class EventsRoutes
{
    public static void MapEventsRoutes(this IEndpointRouteBuilder routes)
    {
        var eventsRoutes = routes.MapGroup("/events");
        
        eventsRoutes.MapGet("/", async () =>
        {
            var events = await EventsRepository.GetAll();
            await Task.Delay(TimeSpan.FromSeconds(2));
            return TypedResults.Ok(new EventsResponse { Events = events });
        })
        .WithName("GetAllEvents")
        .WithOpenApi();
        
        eventsRoutes.MapGet("/{id}", async (string id) =>
        {
            var @event = await EventsRepository.Get(id);
            return TypedResults.Ok(new EventResponse { Event = @event });
        })
        .WithName("GetEvent")
        .WithOpenApi();
        
        eventsRoutes.MapPost("/", async Task<Results<Created<EventResponse>, UnprocessableEntity<EventsErrorResponse>>> ([FromBody] Event data) =>
        {
            var errors = new EventErrors();

            if (!Validation.IsValidText(data.Title))
            {
                errors.Title = "Invalid title.";
            }
            
            if (!Validation.IsValidText(data.Description))
            {
                errors.Description = "Invalid description.";
            }
            
            if (!Validation.IsValidDate(data.Date))
            {
                errors.Date = "Invalid date.";
            }

            if (!Validation.IsValidImageUrl(data.Image))
            {
                errors.Image = "Invalid image.";
            }
            
            if (errors.Title is not null || errors.Description is not null || errors.Date is not null || errors.Image is not null)
            {
                return TypedResults.UnprocessableEntity(new EventsErrorResponse
                    { Message = "Adding the event failed due to validation errors.", Errors = errors });
            }

            await EventsRepository.Add(data);
            return TypedResults.Created($"/{data.Id}", new EventResponse { Message = "Event saved.", Event = data });
        })
        .WithName("AddEvent")
        .WithOpenApi()
        .AddEndpointFilter(async (context, next) => await Authentication.CheckAuthenticationMiddleware(context, next));
        
        eventsRoutes.MapPatch("/{id}", async Task<Results<Ok<EventResponse>, UnprocessableEntity<EventsErrorResponse>>> (string id, [FromBody] Event data) =>
        {
            var errors = new EventErrors();

            if (!Validation.IsValidText(data.Title))
            {
                errors.Title = "Invalid title.";
            }
            
            if (!Validation.IsValidText(data.Description))
            {
                errors.Description = "Invalid description.";
            }
            
            if (!Validation.IsValidDate(data.Date))
            {
                errors.Date = "Invalid date.";
            }

            if (!Validation.IsValidImageUrl(data.Image))
            {
                errors.Image = "Invalid image";
            }
            
            if (errors.Title is not null || errors.Description is not null || errors.Date is not null || errors.Image is not null)
            {
                return TypedResults.UnprocessableEntity(new EventsErrorResponse
                    { Message = "Updating the event failed due to validation errors.", Errors = errors });
            }

            await EventsRepository.Replace(id, data);
            return TypedResults.Ok(new EventResponse { Message = "Event updated.", Event = data });
        })
        .WithName("UpdateEvent")
        .WithOpenApi()
        .AddEndpointFilter(async (context, next) => await Authentication.CheckAuthenticationMiddleware(context, next));
        
        eventsRoutes.MapDelete("/{id}", async (string id) =>
        {
            await EventsRepository.Remove(id);
            return TypedResults.Ok(new EventResponse { Message = "Event deleted." });
        })
        .WithName("DeleteEvent")
        .WithOpenApi()
        .AddEndpointFilter(async (context, next) => await Authentication.CheckAuthenticationMiddleware(context, next));
    }
}