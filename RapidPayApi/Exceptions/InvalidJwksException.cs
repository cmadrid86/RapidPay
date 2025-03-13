namespace RapidPayApi.Exceptions;

public class InvalidJwksException : CustomException
{
    private const string DefaultMessage = "Invalid JWKS";
    private const int DefaultStatusCode = 500;
    private const string DefaultTitle = "Invalid JWKS";

    public InvalidJwksException() : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }

    public InvalidJwksException(string errorMessage) : base(errorMessage, DefaultStatusCode, DefaultTitle)
    {
    }
}