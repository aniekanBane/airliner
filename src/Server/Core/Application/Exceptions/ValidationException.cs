using FluentValidation.Results;

namespace Application.Exceptions;

public class ValidationException(IEnumerable<ValidationFailure> failures) 
    : ApplicationException(
        HttpStatusCode.BadRequest, 
        new(
            "One or more validation failures has occurred",
            [.. failures.Select(x => $"{x.PropertyName}: {x.ErrorMessage}")]
        )
    )
{
}
