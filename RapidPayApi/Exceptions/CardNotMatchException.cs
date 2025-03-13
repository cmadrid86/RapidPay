namespace RapidPayApi.Exceptions;

public sealed class CardNotMatchException() : CustomException(DefaultErrorMessage, DefaultStatusCode, DefaultTitle)
{
    private const string DefaultErrorMessage = "CVV or expiration does not match";

    private const string DefaultTitle = "BadRequest";

    private const int DefaultStatusCode = 400;
}