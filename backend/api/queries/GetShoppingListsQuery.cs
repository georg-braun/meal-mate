using api.database;
using domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.commands;

public class GetShoppingListsQuery
{
    public static string Route = nameof(GetShoppingListsQuery);
}

public static class GetShoppingListsQueryHandler
{
    public static async Task<List<ShoppingList>> Handle(MealMateContext context)
    {
        return await context.ShoppingLists.ToListAsync();
        
    }
}