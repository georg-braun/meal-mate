using domain;
using infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.api.queries;

public class CategoriesQuery
{
    public static string Route = nameof(CategoriesQuery);
    
    public static class Handler
    {
        public static async Task<List<Category>> Handle(MealMateContext context)
        {
            return await context.Categories.ToListAsync();
        }
    }
}