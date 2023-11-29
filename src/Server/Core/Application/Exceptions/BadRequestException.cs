namespace Application.Exceptions;

public class BadRequestException(Error error) 
    : ApplicationException(HttpStatusCode.BadRequest, error)
{
    public BadRequestException() : this(DomainErrors.Generic.BadRequest) {}
}
