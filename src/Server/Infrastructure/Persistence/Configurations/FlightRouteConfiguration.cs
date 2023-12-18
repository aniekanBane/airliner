using Domain.Entities.ARS.FlightRoute;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class FlightRouteConfiguration : IEntityTypeConfiguration<FlightRoute>
{
    public void Configure(EntityTypeBuilder<FlightRoute> builder)
    {
        builder.Property(x => x.DepartureAirportCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder.Property(x => x.ArrivalAirportCode)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder.HasIndex(x => new { x.DepartureAirportCode, x.ArrivalAirportCode })
            .IsUnique();
    }
}
