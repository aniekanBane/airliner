using Domain.Exceptions;
using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;
using Domain.Utilities;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed class City : AuditableEntity<string>, IAggregateRoot
{
    private readonly List<string> _images = [];
    private readonly List<Airport> _airports = [];
    private const string prefix = "DS";

    private City(string name, string country, string summary)
    {
        Name = name;
        Country = country;
        Summary = summary;

        SetId();
    }

    public string Name { get; private set; }

    public string Country { get; private set; }

    public string Summary { get; private set; }

    public bool IsHub { get => Airports.Any(x => x.IsHub); private set{} }

    public string Thumbnail 
    { 
        get => Images.FirstOrDefault() ?? string.Empty;  
        private set {}
    }

    public int NumberOfAirports { get => Airports.Count; private set {}}

    public IReadOnlyCollection<Airport> Airports => _airports.AsReadOnly();

    public IList<string> Images => _images.AsReadOnly();

    public static City Create(
        string name, 
        string country, 
        string summary)
    {
        return new City((CityName)name, (CountryName)country, summary);
    }

    public void UpdateDetails(string name, string country)
    {
        if (!name.Equals(Name, StringComparison.OrdinalIgnoreCase))
            Name = (CityName)name;
        
        if (!country.Equals(Country, StringComparison.OrdinalIgnoreCase))
            Country = (CountryName)country;
    }

    public void AddImage(string uri)
    {
        if (Images.Count == 6) return;

        if (!Images.Contains(uri))
            _images.Add(uri);
    }

    public void RemoveImage(string? uri)
    {
        _images.RemoveAll(x => x == uri);
    }

    public bool AddAirport(
        string iataCode, 
        string icaoCode, 
        string name, 
        string timeZone,
        ImmutableArray<string> terminals,
        bool isHub = false)
    {
        if (Airports.Any(x => x.IataCode == iataCode || x.IcaoCode == icaoCode))
            return false;

        var airport = Airport.Create(
            isHub,
            iataCode, icaoCode, 
            name, Name, Country, 
            timeZone, terminals
        );

        _airports.Add(airport);

        return true;
    }

    public void SetThumbnail(string image)
    {
        DomainException.ThrowIfNullOrWhiteSpace(image, nameof(image));

        Thumbnail = image;
    }

    public bool RemoveAirport(string iataCode)
    {
        return _airports.RemoveAll(x => x.IataCode == iataCode) == 1;
    }

    public void DeleteSummary() => Summary = string.Empty;

    public void UpdateSummary(string summary)
    {
        if (summary.Equals(Summary, StringComparison.OrdinalIgnoreCase))
            return;

        DomainException.ThrowIfNullOrWhiteSpace(summary, nameof(summary));

        Summary = summary;
    }

    private void SetId()
    {
        Id = prefix + RandomGenerator.GenerateRandomString(8);

        if (string.IsNullOrWhiteSpace(Id))
            throw new DomainException("null id!");
    }
}

public sealed record class CityName(string Value) : StringObject<CityName>(Value);

public sealed record class CountryName(string Value) : StringObject<CountryName>(Value);
