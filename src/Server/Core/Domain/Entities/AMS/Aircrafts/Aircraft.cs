using Domain.Entities.AMS.AircraftTypes;
using Domain.Exceptions;
using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;
using Domain.Utilities;

namespace Domain.Entities.AMS.Aircrafts;

public sealed class Aircraft : AuditableEntity<string>, IAggregateRoot
{
    private readonly List<Cabin> _cabins = [];
    private readonly string _registrationNumber;
    private const string prefix = "A3R";

    private Aircraft(
        string name, 
        int capacity,
        string aircraftTypeModel,
        string registrationNumber, 
        AircraftStatus aircraftStatus)
    {
        _registrationNumber = registrationNumber;
        
        Name = name;
        Capacity = capacity;
        AircraftStatus = aircraftStatus;
        AircraftTypeModel = aircraftTypeModel;

        SetId();
    }

    public int Capacity { get; private set; }

    public string Name { get; private set; }

    public string AircraftTypeModel { get; private set; }

    public string RegistrationNumber => _registrationNumber;
    
    public AircraftStatus AircraftStatus { get; private set; }

    public IReadOnlyCollection<Cabin> Cabins => _cabins.AsReadOnly();

    public bool IsPending => AircraftStatus is AircraftStatus.Pending;

    public static Aircraft Create(
        int capacity, string name, 
        string aircraftModel, 
        string registrationNumber, 
        AircraftStatus aircraftStatus)
    {   
        return new Aircraft(
            (AircraftName)name,
            (AircraftCapacity)capacity, 
            (AircrafModel)aircraftModel, 
            (RegistrationNumber)registrationNumber,
            aircraftStatus
        );
    }

    public void ApplyMalfunction()
    {
        if (!CanGround()) return;
        
        AircraftStatus = AircraftStatus.InMaintenance;
    }

    public void GroundAircraft()
    {
        if (!CanGround()) return;

        AircraftStatus = AircraftStatus.Grounded;
    }

    public void Fly()
    {
        if (!IsPending) return;

        AircraftStatus = AircraftStatus.InFlight;
    }

    public void AddCabin(int capacity, CabinClass cabinClass)
    {
        if (!CanAddCabin(capacity))
            throw new DomainException("Value ({0}) is invalid.", nameof(capacity));

        if (Cabins.Any(x => x.CabinClass == cabinClass))
            return;

        _cabins.Add(Cabin.Create(capacity, cabinClass));
    }

    public void UpdateName(string name)
    {
        Name = (AircraftName)name;
    }

    public bool RemoveCabin(CabinClass cabinClass)
    {
        return _cabins.RemoveAll(x => x.CabinClass == cabinClass) == 1;
    }

    private void SetId()
    {
        Id = prefix + RandomGenerator.GenerateRandomString(8);

        if (string.IsNullOrWhiteSpace(Id))
            throw new DomainException("null id!");
    }

    private bool CanGround()
    {
        return AircraftStatus is AircraftStatus.Pending or AircraftStatus.InMaintenance;
    }

    private bool CanAddCabin(int capacity)
    {
        return (Capacity - Cabins.Sum(x => x.Capacity)) > capacity;
    }
}

public sealed record class AircraftCapacity : NumberObject<AircraftCapacity>
{
    public AircraftCapacity(int value) : base(value) {}
}
