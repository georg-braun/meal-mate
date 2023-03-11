using Microsoft.EntityFrameworkCore;

namespace infrastructure.database;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<MealMateContext>())
            {
                // Ensure that the database is created and the migrations are applied
                appContext.Database.Migrate();
            }
        }

        return webApp;
    }
}