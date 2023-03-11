using domain;
using infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.api.queries;

public class ShoppingListsQuery
{
    public const string Route = nameof(ShoppingListsQuery);
}

public static class GetShoppingListsQueryHandler
{
    public static async Task<List<ShoppingList>> Handle(MealMateContext context)
    {
        return await context.ShoppingLists.ToListAsync();
    }
}