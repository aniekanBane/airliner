using Domain.Entities.ARS.DestinationAggregate;
using Domain.Primitives.Common;

namespace Domain.Entities.ARS.FlightRoute;

public sealed class FlightRoute : AuditableEntity<Guid>, IAggregateRoot
{
    private decimal _basePrice;

    private FlightRoute(decimal basePrice, string departureAirportCode, string arrivalAirportCode)
    {
        _basePrice = basePrice;

        DepartureAirportCode = departureAirportCode;
        ArrivalAirportCode = arrivalAirportCode;
        RouteStatus = RouteStatus.Active;
    }

    public decimal BasePrice => _basePrice;

    public string DepartureAirportCode { get; private set; }

    public string ArrivalAirportCode { get; private set; }

    public RouteStatus RouteStatus {get; private set; }

    public bool IsPopular { get; private set; }

    public bool IsClosed => RouteStatus == RouteStatus.Closed;

    public static FlightRoute Create(
        decimal basePrice,
        string? departureAirportCode, 
        string? arrivalAirportCode)
    {
        return new FlightRoute(
            basePrice,
            (IATACode)departureAirportCode, 
            (IATACode)arrivalAirportCode
        );
    }

    internal void UpdatePrice(decimal newPrice)
    {
        _basePrice = newPrice;
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
