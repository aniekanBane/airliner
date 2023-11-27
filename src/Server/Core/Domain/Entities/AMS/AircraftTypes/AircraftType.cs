using Domain.Entities.AMS.Aircrafts;
using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.AMS.AircraftTypes;

public class AircraftType : AuditableEntity<int>, IAggregateRoot
{
    private readonly List<Aircraft> _aircrafts = [];

    private AircraftType(string model, string manufacturer)
    {
        Model = model;
        Manufacturer = manufacturer;
    }

    public string Model { get; private set; }

    public string Manufacturer { get; private set; }

    public int FleetSize { get => Aircrafts.Count; private set {} }

    public IReadOnlyCollection<Aircraft> Aircrafts => _aircrafts.AsReadOnly();

    public static AircraftType Create(string model, string manufacturer)
    {
        return new AircraftType(
            (AircrafModel)model, 
            (AircraftManufacturer)manufacturer
        );
    }
}

public sealed record class AircrafModel(string Value) 
    : StringObject<AircrafModel>(Value);

public sealed record class AircraftManufacturer(string Value) 
    : StringObject<AircraftManufacturer>(Value);
