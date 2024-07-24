namespace Events.Backend.Util;

public static class Validation
{
    public static bool IsValidText(string? value, int minLength = 1)
    {
        return value is not null && value.Trim().Length >= minLength;
    }
    
    public static bool IsValidDate(string? value)
    {
        return value is not null && DateTime.TryParse(value, out _);
    }

    public static bool IsValidImageUrl(string? value)
    {
        return value is not null && value.StartsWith("http");
    }

    public static bool IsValidEmail(string? value)
    {
        return value is not null && value.Contains('@');
    }
}