using Microsoft.EntityFrameworkCore;

namespace api.database;

public class MealMateContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=meal-mate.db");
        base.OnConfiguring(optionsBuilder);
    }
}