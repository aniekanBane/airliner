namespace Domain.Primitives.Results;

public class Result<T> : Result, IResult<T>
{
    protected internal Result(
        T data, 
        HttpStatusCode statusCode, 
        string? message, 
        Error? error
    ) : base(statusCode, message, error) => Data = data;

    protected internal Result(T data, HttpStatusCode code, string? message) 
        : base(code, message, null) => Data = data;

    public T? Data { get; }

    public static IResult<T> Success(T data, HttpStatusCode code, string? message = default) 
        => new Result<T>(data, code, message);

    public static Task<IResult<T>> SuccessAsync(T data, HttpStatusCode code, string? message = default) 
        => Task.FromResult(Success(data, code, message));

    public new static IResult<T> Failure(HttpStatusCode code, Error error, string? message = default)
        => new Result<T>(default!, code, message, error);

    public new static Task<IResult<T>> FailureAsync(
        HttpStatusCode code, Error error, string? message = default
    ) => Task.FromResult(Failure(code, error, message));
}
