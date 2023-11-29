namespace Application.Exceptions;

public class ServerException(Error error) 
    : ApplicationException(HttpStatusCode.InternalServerError, error)
{
    public ServerException() : this(DomainErrors.Generic.ServerError) {}
}
