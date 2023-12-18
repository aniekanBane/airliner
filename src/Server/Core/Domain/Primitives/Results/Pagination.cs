namespace Domain.Primitives.Results;

public readonly record struct Pagination(
    int PageCount,
    int CurrentPage = 1, 
    int PageSize = 10,
    int TotalCount = 0
)
{
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < PageCount;
}
