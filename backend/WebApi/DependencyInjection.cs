using application;
using Infrastructure;

namespace WebApi;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddSolutionDependencies(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PostgresDatabase");
        if (string.IsNullOrEmpty(connectionString))
        {
            // Todo: Handle empty string. FluentValidator. 
        }
        
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(connectionString!);
        
        var assembly = typeof(DependencyInjection).Assembly;
        builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
     
        return builder;
    }
    

}