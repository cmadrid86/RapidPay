namespace RapidPayApi.Exceptions;

public sealed class InsufficientBalanceException() : CustomException(DefaultErrorMessage, DefaultStatusCode, DefaultTitle)
{
    private const string DefaultErrorMessage = "Insufficient balance";

    private const string DefaultTitle = "BadRequest";

    private const int DefaultStatusCode = 400;
}