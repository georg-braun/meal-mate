using Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        AddDatabase(services, databaseConnectionString);

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<DomainEventInterceptor>();
        services.AddDbContext<MealMateContext>((provider, optionsBuilder) =>
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.AddInterceptors(provider.GetRequiredService<DomainEventInterceptor>());
        });

        return services;
    }
}