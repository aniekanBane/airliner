namespace Domain.Entities.ARS.FlightAggregate;

public enum FlightType
{
    Local = 21,

    Regional,

    International
}

public enum FlightStatus
{
    Pending = 31,

    Boarding,

    Departed,

    OnRoute,

    Arrived,

    Delayed,

    Rescheduled,

    Cancelled
}
