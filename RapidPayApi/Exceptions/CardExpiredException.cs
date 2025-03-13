namespace RapidPayApi.Exceptions;

public sealed class CardExpiredException() : CustomException(DefaultErrorMessage, DefaultStatusCode, DefaultTitle)
{
    private const string DefaultErrorMessage = "Card is expired";

    private const string DefaultTitle = "BadRequest";

    private const int DefaultStatusCode = 400;
}