using Domain.Primitives.Results;

namespace Domain.Entities.Errors;

public static partial class DomainErrors
{
    public static class Generic
    {
        public static readonly Error ServerError = new("ServerError", "Something went wrong.");
        public static readonly Error BadRequest = new("BadRequest", "Request format is invalid.");
    }
}
