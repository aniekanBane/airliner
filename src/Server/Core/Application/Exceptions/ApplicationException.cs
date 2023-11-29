namespace Application.Exceptions;

public abstract class ApplicationException : Exception
{
    protected ApplicationException(HttpStatusCode statusCode, Error error) 
        : base((string)error)
    {
        Error = error;
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
    public Error Error { get; }
}
