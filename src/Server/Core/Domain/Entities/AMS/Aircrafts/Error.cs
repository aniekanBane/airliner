using Domain.Primitives.Results;

namespace Domain.Entities.Errors;

public static partial class DomainErrors
{
    public static class Aircraft
    {
        public static readonly Error Exists = new(
            "Aircraft.Exists", 
            "Aircraft already exists!"
        );

        public static readonly Error NotFound = new(
            "Aircraft.NotFound", 
            "Aircraft does not exist!"
        );

        public static readonly Error DuplicateName = new(
            "Aircraft.NameConflict", 
            "Aircraft with name already exist!"
        );
    }
}
