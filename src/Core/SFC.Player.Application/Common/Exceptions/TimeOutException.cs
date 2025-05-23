namespace SFC.Player.Application.Common.Exceptions;
public class TimeOutException : Exception
{
    public TimeOutException() { }

    public TimeOutException(string message) : base(message) { }

    public TimeOutException(string message, Exception exception) : base(message, exception) { }
}
