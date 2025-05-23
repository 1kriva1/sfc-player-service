namespace SFC.Player.Application.Common.Exceptions;
public class CommunicationException : Exception
{
    public CommunicationException() { }

    public CommunicationException(string message) : base(message) { }

    public CommunicationException(string message, Exception exception) : base(message, exception) { }
}
