using Domain.Primitives.Results;

namespace Domain.Entities.Errors;

public static partial class DomainErrors
{
    public static class AircraftType
    {
        public static readonly Error Exists = new(
            "AircraftType.Exists", 
            "Aircraft type already exists!"
        );

        public static readonly Error NotFound = new(
            "AircraftType.NotFound", 
            "Aircraft type does not exist!"
        );
    }
}
