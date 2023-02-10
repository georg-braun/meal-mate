using api.database;
using domain;
using Microsoft.EntityFrameworkCore;

namespace api.commands;

public class GetItemsQuery
{
    public static string Route = nameof(GetItemsQuery);
}

public static class GetItemsQueryHandler
{
    public static async Task<List<Item>> Handle(MealMateContext context)
    {
        return await context.Items.Include(_ => _.Category).ToListAsync();
    }
}