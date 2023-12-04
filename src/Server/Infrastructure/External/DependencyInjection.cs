using Application.Abstractions.Common;
using External.TimeProvider;
using Microsoft.Extensions.DependencyInjection;

namespace External;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ITimeProvider, SystemTimeProvider>();

        return services;
    }
}
