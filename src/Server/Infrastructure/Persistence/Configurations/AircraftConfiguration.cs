using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.AMS.AircraftTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class AircraftConfiguration : IEntityTypeConfiguration<Aircraft>
{
    public void Configure(EntityTypeBuilder<Aircraft> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => x.RegistrationNumber).IsUnique();
        
        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(x => x.AircraftTypeModel).IsRequired();

        builder.OwnsMany(x => x.Cabins, n =>
        {
            n.HasIndex("AircraftId", "CabinClass").IsUnique();
        });
    }
}

internal sealed class AircraftTypeConfiguration : IEntityTypeConfiguration<AircraftType>
{
    public void Configure(EntityTypeBuilder<AircraftType> builder)
    {
        builder.HasIndex(x => x.Model).IsUnique();

        builder.Property(x => x.Model)
            .HasField("_model")
            .IsRequired();

        builder.Property(x => x.Manufacturer)
            .HasField("_manufacturer")
            .IsRequired();

        builder.HasMany(x => x.Aircrafts)
            .WithOne()
            .HasPrincipalKey(x => x.Model)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
