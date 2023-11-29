namespace Application.Exceptions;

public class ConflictException(Error error) 
    : ApplicationException(HttpStatusCode.Conflict, error)
{
}
