namespace RapidPayApi.Exceptions;

public sealed class CardNotFoundException() : CustomException(DefaultErrorMessage, DefaultStatusCode, DefaultTitle)
{
    private const string DefaultErrorMessage = "Card was not found";

    private const string DefaultTitle = "NotFound";

    private const int DefaultStatusCode = 404;
}