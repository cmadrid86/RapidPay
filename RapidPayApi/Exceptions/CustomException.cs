namespace RapidPayApi.Exceptions;

public abstract class CustomException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }

    protected CustomException(string message, int statusCode, string title, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Title = title;
    }

    protected CustomException(string message, int statusCode, string title) : base(message)
    {
        StatusCode = statusCode;
        Title = title;
    }
}