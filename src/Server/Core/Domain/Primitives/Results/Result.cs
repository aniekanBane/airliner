namespace Domain.Primitives.Results;

public class Result : IResult
{
    protected Result(HttpStatusCode statusCode, string? message, Error? error)
    {        
        Error = error;
        Code = statusCode;
        Message = message;
        OccuredAt = DateTime.UtcNow;
    }

    protected Result(HttpStatusCode code, string? message) : this(code, message, null) {}

    public HttpStatusCode Code { get; }
    public bool IsSuccess => Error is null;
    public string? Message { get; }
    public Error? Error { get; }
    public DateTime OccuredAt { get; }

    public static IResult Failure(HttpStatusCode code, Error error, string? message = default)
     => new Result(code, message, error);

    public static IResult Success(HttpStatusCode code, string? message = default)
        => new Result(code, message);

    public static Task<IResult> FailureAsync(HttpStatusCode code, Error error, string? message = default)
        => Task.FromResult(Failure(code, error, message));

    public static Task<IResult> SuccessAsync(HttpStatusCode code, string? message = default) 
        => Task.FromResult(Success(code, message));
}
