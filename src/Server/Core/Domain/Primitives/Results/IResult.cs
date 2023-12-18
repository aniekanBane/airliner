namespace Domain.Primitives.Results;

public interface IPaginatedResult<T> : IResult<ImmutableList<T>>
{
    Pagination Pagination { get; }
}

public interface IResult<T> : IResult
{
    T? Data { get; }
}

public interface IResult
{
    HttpStatusCode Code { get; }

    bool IsSuccess { get; }

    string? Message { get; }

    Error? Error { get; }

    DateTime OccuredAt { get; }
}
