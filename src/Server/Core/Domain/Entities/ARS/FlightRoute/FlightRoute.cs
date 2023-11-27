using Domain.Entities.ARS.DestinationAggregate;
using Domain.Primitives.Common;

namespace Domain.Entities.ARS.FlightRoute;

public sealed class FlightRoute : AuditableEntity<Guid>, IAggregateRoot
{
    private FlightRoute(string departureAirportCode, string arrivalAirportCode)
    {
        DepartureAirportCode = departureAirportCode;
        ArrivalAirportCode = arrivalAirportCode;
        RouteStatus = RouteStatus.Active;
    }

    public string DepartureAirportCode { get; private set; }

    public string ArrivalAirportCode { get; private set; }

    public RouteStatus RouteStatus {get; private set; }

    public bool IsPopular { get; private set; }

    public bool IsClosed => RouteStatus == RouteStatus.Closed;

    public static FlightRoute Create(
        string? departureAirportCode, 
        string? arrivalAirportCode)
    {
        return new FlightRoute(
            (IATACode)departureAirportCode, 
            (IATACode)arrivalAirportCode
        );
    }

    public void CloseRoute()
    {
        if (IsClosed) 
            return;

        // TODO: closed event

        RouteStatus = RouteStatus.Closed;
    }
}

public enum RouteStatus
{
    Active = 41,

    InActive,

    Closed
}
