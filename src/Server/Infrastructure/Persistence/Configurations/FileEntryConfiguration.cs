using Domain.Entities.Shared.Storage;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class FileEntryConfiguration : IEntityTypeConfiguration<FileEntry>
{
    public void Configure(EntityTypeBuilder<FileEntry> builder)
    {
        builder.Property(x => x.AltName).HasMaxLength(64);

        builder.Property(x => x.Description).HasMaxLength(256);

        builder.Property(x => x.FileName).IsRequired();

        builder.Property(x => x.FileLocation).IsRequired();

        builder.Property(x => x.Uploaded).HasDefaultValueSql("now()");
    }
}
