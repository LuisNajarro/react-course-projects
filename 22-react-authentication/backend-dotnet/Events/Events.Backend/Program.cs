using Events.Backend.Routes;
using Events.Backend.Util;
using Microsoft.AspNetCore.Diagnostics;
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
            .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Patch, HttpMethods.Delete)
            .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapAuthRoutes();

app.MapEventsRoutes();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var error = exceptionHandlerPathFeature?.Error;
        
        context.Response.StatusCode = error switch
        {
            NotFoundException e => e.Status,
            NotAuthenticatedException e => e.Status,
            _ => StatusCodes.Status500InternalServerError
        };
        context.Response.ContentType = "application/json";

        var result = new { message = error?.Message ?? "Something went wrong." };
        await context.Response.WriteAsJsonAsync(result);
    });
});

await app.RunAsync();
