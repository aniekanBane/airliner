namespace Application.Abstractions.Database;

public interface IDatabaseSeeder
{
    void Initialize();
    
    Task InitializeAsync(CancellationToken cancellationToken = default);
}
