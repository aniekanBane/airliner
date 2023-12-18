using Domain.Entities.ARS.FlightAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasIndex(x => x.FlightNumber);
        builder.Property(x => x.FlightNumber).IsRequired();

        builder.HasIndex(x => new { x.FlightNumber, x.DepartureTime }).IsUnique();

        builder.Property(x => x.Capacity).HasField("_capacity");

        builder.Property(x => x.DepartureAirport)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder.Property(x => x.ArrivalAirport)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder.Property(x => x.AircraftId).IsRequired();

        builder.OwnsMany(x => x.Fares, n =>
        {
            n.HasIndex("Name", "CabinClass", "FlightId").IsUnique();
            
            n.Property(f => f.Name).IsRequired();

            n.Property(f => f.Price).HasField("_price");

            n.Property(f => f.AvailableSeats).HasField("_availableSeats");

            n.PrimitiveCollection(f => f.FareRules);
        });
    }
}
