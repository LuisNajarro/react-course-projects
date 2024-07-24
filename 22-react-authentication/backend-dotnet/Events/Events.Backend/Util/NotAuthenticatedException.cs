namespace Events.Backend.Util;

public class NotAuthenticatedException : Exception
{
    public int Status { get; set; }
    
    public NotAuthenticatedException(string message, int status = 401) : base(message)
    {
        Status = status;
    }
}