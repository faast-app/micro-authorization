using ApiAuth.Application.Abstractions.Data;
using ApiAuth.Domain.Repositories;
using ApiAuth.Extensions;
using ApiAuth.Persistence.Data;
using ApiAuth.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ApiAuth.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.ConfigureOptions<IntegracionDbOptionsSetup>();

        services.AddDbContext<IntegracionDbContext>(
            (serviceProvider, dbContenxtOptionsBuilder) =>
            {
                var databaseOptions = serviceProvider.GetService<IOptions<IntegracionDbOptions>>()!.Value;
                dbContenxtOptionsBuilder.UseMySql(databaseOptions.ConnectionString, ServerVersion.AutoDetect(databaseOptions.ConnectionString), mySqlOptionsAction =>
                {
                    mySqlOptionsAction.EnableRetryOnFailure(3);
                    mySqlOptionsAction.CommandTimeout(30);
                });

                dbContenxtOptionsBuilder.EnableDetailedErrors(true);
                dbContenxtOptionsBuilder.EnableSensitiveDataLogging(true);
            });

        services.AddScoped<IIntegracionDbContext>(sp =>
            sp.GetRequiredService<IntegracionDbContext>()
        );

        services.AddScoped<IUnitOfWorkIntegracion>(sp =>
            sp.GetRequiredService<IntegracionDbContext>()
        );

        services.AddServicesAsInterfaces(Assembly.Load("ApiAuth"), "Repository");

        return services;
    }
}