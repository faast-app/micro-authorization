using System.Reflection;

namespace ApiAuth.Extensions;

public static class ServiceExtensions
{
    public static void AddServicesAsInterfaces(this IServiceCollection services, Assembly assembly, string serviceSuffix)
    {
        services.AddServicesAsInterfaces(new Assembly[] { assembly }, serviceSuffix);
    }
    public static void AddServicesAsInterfaces(this IServiceCollection services, Assembly[] assemblies, string serviceSuffix)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(x => x.Where(c => c.Name.EndsWith(serviceSuffix)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }
}