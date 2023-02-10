using api.database;
using domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.commands;

public class GetCategoriesQuery
{
    public static string Route = nameof(GetCategoriesQuery);
}

public static class GetCategoriesQueryHandler
{
    public static async Task<List<Category>> Handle(MealMateContext context)
    {
        return await context.Categories.ToListAsync();
        
    }
}