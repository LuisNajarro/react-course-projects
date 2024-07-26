using Events.Backend.Data;
using Events.Backend.Models;
using Events.Backend.Util;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Events.Backend.Routes;

public static class AuthRoutes
{
    public static void MapAuthRoutes(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/signup", async Task<Results<Created<UserResponse>, UnprocessableEntity<UserErrorResponse>>> (User user) =>
        {
            var errors = new UserErrors();

            if (!Validation.IsValidEmail(user.Email))
            {
                errors.Email = "Invalid email.";
            }
            else
            {
                try
                {
                    var existingUser = await UsersRepository.Get(user.Email!);
                    if (existingUser is not null)
                    {
                        errors.Email = "Email exists already.";
                    }
                }
                catch (Exception e) { }
            }

            if (!Validation.IsValidText(user.Password, 6))
            {
                errors.Password = "Invalid password. Must be at least 6 characters long.";
            }

            if (errors.Email is not null || errors.Password is not null)
            {
                return TypedResults.UnprocessableEntity(new UserErrorResponse
                {
                    Message = "User signup failed due to validation errors.",
                    Errors = errors
                });
            }

            var createdUser = await UsersRepository.Add(user);
            var authToken = Authentication.CreateJsonToken(createdUser.Email);

            return TypedResults.Created("",
                new UserResponse { Message = "User created.", User = createdUser, Token = authToken });
        });
        
        routes.MapPost("/login", async Task<Results<Ok<UserResponse>, UnauthorizedHttpResult, UnprocessableEntity<UserErrorResponse>>> (User data) =>
        {
            var email = data.Email;
            var password = data.Password;
            
            User user;
            try
            {
                user = await UsersRepository.Get(email);
            }
            catch (Exception e)
            {
                return TypedResults.Unauthorized(); // TODO: Add error message 'Authentication failed.'
            }

            var passwordIsValid = Authentication.IsValidPassword(password, user.Password);
            if (!passwordIsValid)
            {
                return TypedResults.UnprocessableEntity(new UserErrorResponse
                {
                    Message = "Invalid credentials.",
                    Errors = new UserErrors { Credentials = "Invalid email or password entered." }
                });
            }

            var token = Authentication.CreateJsonToken(email);
            return TypedResults.Ok(new UserResponse { Token = token });
        });
    }
}