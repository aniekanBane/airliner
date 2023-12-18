using Domain.Entities.ARS.DestinationAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasIndex(x => new { x.Name, x.Country }).IsUnique();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Country).IsRequired();

        builder.Property(x => x.Summary).HasMaxLength(4000);

        builder.PrimitiveCollection(x => x.Images).HasMaxLength(6);

        builder.HasMany(x => x.Airports)
            .WithOne()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.Property(a => a.IataCode)
            .HasField("_iataCode")
            .HasMaxLength(3)
            .IsFixedLength();

        builder.Property(a => a.IcaoCode)
            .HasField("_icaoCode")
            .HasMaxLength(4)
            .IsFixedLength();

        builder.Property(a => a.TimeZone).HasField("_timeZone");

        builder.HasIndex(x => x.IcaoCode).IsUnique();
        builder.HasAlternateKey(a => a.IataCode);

        builder.Property(a => a.Name).IsRequired();

        builder.Property(a => a.City).IsRequired();
        builder.Property(a => a.Country).IsRequired();

        builder.Property(a => a.TimeZone).IsRequired();

        builder.PrimitiveCollection(a => a.Terminals);
    }
}
