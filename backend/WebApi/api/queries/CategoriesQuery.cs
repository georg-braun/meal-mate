using domain;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public class CategoriesQuery
{
    public const string Route = nameof(CategoriesQuery);
    
    public static class Handler
    {
        public static async Task<List<Category>> Handle(MealMateContext context)
        {
            return await context.Categories.ToListAsync();
        }
    }
}