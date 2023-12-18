using Application.Abstractions.Database;

namespace PublicApi.Extensions;

internal static class WebApplicationExtension
{
    internal static void InitializeDatabases(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<IDatabaseSeeder>();
            
            foreach(var seeder in seeders)
            {
                // seeder.Initialize();
            }
        }
    }
}
