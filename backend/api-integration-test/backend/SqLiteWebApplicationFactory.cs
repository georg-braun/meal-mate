using infrastructure.database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace api_integration_test.backend;

/// <summary>
///     Creates a backend with authentication and a sqlite database.
/// </summary>
/// <typeparam name="TStartup"></typeparam>
public class SqLiteWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly SqliteConnection _connection;

    public SqLiteWebApplicationFactory(SqliteConnection connection)
    {
        _connection = connection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveRegisteredDataContext(services);
            AddSqliteDbContext(services);
            EnsureThatDatabaseIsCreated(services);
        });
    }

    private void RemoveRegisteredDataContext(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            serviceDescriptor => serviceDescriptor.ServiceType == typeof(DbContextOptions<MealMateContext>));

        if (descriptor is null) return;
        services.Remove(descriptor);
    }

    private void EnsureThatDatabaseIsCreated(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<MealMateContext>();

        db.Database.EnsureCreated();
    }

    private void AddSqliteDbContext(IServiceCollection services)
    {
        services.AddDbContext<MealMateContext>(options => { options.UseSqlite(_connection); });
    }
}