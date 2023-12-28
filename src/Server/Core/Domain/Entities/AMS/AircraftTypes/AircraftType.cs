using Domain.Entities.AMS.Aircrafts;
using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.AMS.AircraftTypes;

public class AircraftType : AuditableEntity<int>, IAggregateRoot
{
    private readonly List<Aircraft> _aircrafts = [];
    private readonly string _manufacturer;
    private readonly string _model;

    private AircraftType(string model, string manufacturer)
    {
        _model = model;
        _manufacturer = manufacturer;
    }

    public string Model => _model;

    public string Manufacturer => _manufacturer;

    public int FleetSize { get => Aircrafts.Count; private set {} }
    
    public IReadOnlyCollection<Aircraft> Aircrafts => _aircrafts.AsReadOnly();

    public static AircraftType Create(string model, string manufacturer)
    {
        return new AircraftType(
            (AircraftModel)model, 
            (AircraftManufacturer)manufacturer
        );
    }
}

public sealed record class AircraftModel(string Value) 
    : StringObject<AircraftModel>(Value);

public sealed record class AircraftManufacturer(string Value) 
    : StringObject<AircraftManufacturer>(Value);
