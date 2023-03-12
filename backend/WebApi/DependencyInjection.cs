using application;
using Infrastructure;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddSolutionDependencies(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure();
        
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
     
        return services;
    }
}