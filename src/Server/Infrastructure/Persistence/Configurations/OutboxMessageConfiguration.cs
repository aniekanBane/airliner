using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models.Messaging;

namespace Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.Property(x => x.EventType).IsRequired();

        builder.Property(x => x.Content).IsRequired();

        builder.Property(x => x.Content).HasColumnType("jsonb");

        builder.HasIndex(x => x.OccurredAtUtc);
        builder.HasIndex(x => x.IsPublished);
    }
}
