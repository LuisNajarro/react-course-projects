using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Events.Backend.Util;

public static class Authentication
{
    private const string Key = "EpL2swPGsIyM6X8sv6IPgw2JFEZRHKwRlsnI8N3fgpcTr2sYGFCemNxHoi5a2sIAA3K0chX2Lo6ECgQbLfxuWw==";
    private const int ExpireMinutes = 60;

    public static string CreateJsonToken(string email)
    {
        var symmetricKey = Encoding.UTF8.GetBytes(Key);
        var tokenHandler = new JwtSecurityTokenHandler();
        var now = DateTime.UtcNow;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = now.AddMinutes(ExpireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var sToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(sToken);

        return token;
    }

    public static SecurityToken ValidateJsonToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Key);
        var validationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        SecurityToken securityToken;
        tokenHandler.ValidateToken(token, validationParameters, out securityToken);

        return securityToken;
    }

    public static bool IsValidPassword(string password, string storedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedPassword);
    }

    public static async ValueTask<object?> CheckAuthenticationMiddleware(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        
        if (context.HttpContext.Request.Method == HttpMethods.Options)
        {
            return await next(context);
        }
        if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            logger.LogInformation("NOT AUTH. AUTH HEADER MISSING.");
            throw new NotAuthenticatedException("Not authenticated.");
        }

        var authFragments = context.HttpContext.Request.Headers.Authorization.ToString().Split(" ");

        if (authFragments.Length != 2)
        {
            logger.LogInformation("NOT AUTH. AUTH HEADER INVALID.");
            throw new NotAuthenticatedException("Not authenticated.");
        }
        var authToken = authFragments[1];
        try
        {
            var validatedToken = ValidateJsonToken(authToken);
            context.HttpContext.Items["Token"] = validatedToken;
        }
        catch (Exception e)
        {
            logger.LogInformation(e, "NOT AUTH. TOKEN INVALID.");
            throw new NotAuthenticatedException("Not authenticated.");
        }

        return await next(context);
    }
}