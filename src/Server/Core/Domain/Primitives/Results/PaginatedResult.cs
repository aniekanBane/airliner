namespace Domain.Primitives.Results;

public sealed class PaginatedResult<T> : Result, IPaginatedResult<T>
{
    private PaginatedResult(
        ImmutableList<T> data,
        Pagination pagination,
        HttpStatusCode statusCode,
        string? message,
        Error? error 
    ) : base(statusCode, message, error)
    {
        Data = data;
        Pagination = pagination;
    }

    public PaginatedResult(
        ImmutableList<T> data, 
        Pagination pagination, 
        HttpStatusCode statusCode, 
        string? message
    ) : this(data, pagination, statusCode, message, null){}

    public PaginatedResult(HttpStatusCode statusCode, Error error, string? message) 
        : this([], default, statusCode, message, error) {}

    public Pagination Pagination { get; }

    public ImmutableList<T> Data { get; }

    public static IPaginatedResult<T> Success(
        ImmutableList<T> data, 
        HttpStatusCode code,  
        int count = 0, int page = 1, int size = 10, string? message = default)
    {
        var totalPages = (int)Math.Ceiling(count / (double)size);
        return new PaginatedResult<T>(data, new(totalPages, page, size, count), code, message);
    }

    public static Task<IPaginatedResult<T>> SuccessAsync(
        ImmutableList<T> data, 
        HttpStatusCode code,  
        int count = 0, int page = 1, int size = 10, string? message = default)
    {
        return Task.FromResult(Success(data, code, count, page, size, message));
    }

    public static new IPaginatedResult<T> Failure(
        HttpStatusCode code, 
        Error error, 
        string? message = default)
    {
        return new PaginatedResult<T>(code, error, message);
    }

    public static new Task<IPaginatedResult<T>> FailureAsync(
        HttpStatusCode code, 
        Error error, 
        string? message = default)
    {
        return Task.FromResult(Failure(code, error, message));
    }
}
