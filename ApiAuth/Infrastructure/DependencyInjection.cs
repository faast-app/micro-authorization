using ApiAuth.Extensions;
using System.Reflection;

namespace ApiAuth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddServicesAsInterfaces(Assembly.Load("ApiAuth"), "Service");

        return services;
    }
}