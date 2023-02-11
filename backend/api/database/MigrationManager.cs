using Microsoft.EntityFrameworkCore;

namespace api.database;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<MealMateContext>())
            {
                try
                {
                    // Ensure that the database is created and the migrations are applied
                    appContext.Database.Migrate();
                      
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        }
        return webApp;
    }
}