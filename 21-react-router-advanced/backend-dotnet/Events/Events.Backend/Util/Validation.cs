namespace Events.Backend.Util;

public static class Validation
{
    public static bool IsValidText(string? value)
    {
        return value is not null && value.Trim().Length > 0;
    }
    
    public static bool IsValidDate(string? value)
    {
        return value is not null && DateTime.TryParse(value, out _);
    }

    public static bool IsValidImageUrl(string? value)
    {
        return value is not null && value.StartsWith("http");
    }
}