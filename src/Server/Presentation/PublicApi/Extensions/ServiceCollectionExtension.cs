// using Domain.Entities.AMS.Aircrafts;
// using Domain.Entities.ARS.DestinationAggregate;
// using Domain.Entities.Shared.Storage;
using Domain.Primitives.Common;
// using Domain.Services;
using Persistence.Services.Logging;
// using PublicApi.Configurations;

namespace PublicApi.Extensions;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        // services.AddScoped<IDestinationService, DestinationService>()
        //         .AddScoped<IAircraftService, AircraftService>()
        //         .AddScoped<IFileEntryService, FileEntryService>();
        
        return services;
    }

    // internal static AppSettings AppSettings(
    //     this IServiceCollection services, 
    //     IConfiguration configuration)
    // {
    //     var appSetting = new AppSettings();
    //     configuration.Bind(appSetting);
    //     services.Configure<AppSettings>(configuration);

    //     return appSetting;
    // }
}
