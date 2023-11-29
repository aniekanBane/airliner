namespace Application.Exceptions;

public class NotFoundException(Error error) 
    : ApplicationException(HttpStatusCode.NotFound, error)
{
}
