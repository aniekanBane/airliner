using Domain.Primitives.Results;

namespace Domain.Entities.Errors;

public static partial class DomainErrors
{
    public static class Flight
    {
        public static readonly Error Exists = new(
            "Flight.Exists",
            "Flight already exists!"
        );

        public static readonly Error NotFound = new(
            "Flight.NotFound",
            "Flight does not exist!"
        );

        public static readonly Error NoRouteCombination = new(
            "Flight.NotFound",
            "No flights found on route combination."
        );

        public static readonly Error FlightTimeInPast = new(
            "FlightTime.Exception", 
            "Outbound flight cannot be in the past!"
        );

        public static readonly Error ReturnDateError = new(
            "FlightTime.Exception",
            "Inbound flight cannot be after outbound flight!"
        );
    }
}
