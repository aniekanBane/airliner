using Domain.Entities.ARS.DestinationAggregate;
using Domain.Primitives.Common;
using Domain.Services;
using Persistence.Services.Logging;

namespace PublicApi.Extensions;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        //services.AddScoped<IDestinationService, DestinationService>();
        
        return services;
    }
}
