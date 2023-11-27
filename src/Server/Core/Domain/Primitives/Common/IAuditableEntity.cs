namespace Domain.Primitives.Common;

public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity
{
    [Timestamp]
    public uint RowVersion { get; set; }

    public string Author { get; set; } = default!;

    public DateTime Created { get; set; }

    public string Editor { get; set; } = default!;

    public DateTime LastModified { get; set; }
}

public interface IAuditableEntity
{
    uint RowVersion { get; set; }

    string Author { get; set; }

    DateTime Created { get; set; }

    string Editor { get; set; }

    DateTime LastModified { get; set; }
}
