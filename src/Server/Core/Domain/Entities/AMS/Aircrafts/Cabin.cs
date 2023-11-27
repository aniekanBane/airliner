namespace Domain.Entities.AMS.Aircrafts;

public sealed record class Cabin
{
    private Cabin(int capacity, CabinClass cabinClass)
    {
        Capacity = capacity;
        CabinClass = cabinClass;
    }

    public int Capacity { get; init; }

    public CabinClass CabinClass { get; init; }

    public static implicit operator CabinClass(Cabin cabin) => cabin.CabinClass;

    public static implicit operator string(Cabin cabin) => cabin.CabinClass.ToString();

    public static implicit operator int(Cabin cabin) => cabin.Capacity;

    public static Cabin Create(int capacity, CabinClass cabinClass)
    {
        return new Cabin((AircraftCapacity)capacity, cabinClass);
    }
}

public enum CabinClass
{
    First = 1,

    Business,

    Premium,

    Economy
}
