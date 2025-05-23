namespace SFC.Player.Application.Common.Exceptions;
public class TokenExchangeException : Exception
{
    public TokenExchangeException() { }

    public TokenExchangeException(string message) : base(message) { }

    public TokenExchangeException(string message, Exception innerException) : base(message, innerException) { }
}
