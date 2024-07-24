namespace Events.Backend.Util;

public class NotFoundException : Exception
{
    public int Status { get; set; }
    
    public NotFoundException(string message, int status = 404) : base(message)
    {
        Status = status;
    }
}