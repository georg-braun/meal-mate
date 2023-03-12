using WebApi.api.commands;
using WebApi.api.queries;

namespace WebApi.api;

public static class ApiExtensions
{
    public static void MapCommands(this WebApplication app)
    {
        app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommand.Handler.Handle).WithTags("Category");
        app.MapPost($"/{DeleteItemCommand.Route}", DeleteItemCommand.Handler.Handle).WithTags("Category");

        app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommand.Handler.Handle)
            .WithTags("Category");
        app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommand.Handler.Handle).WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryWithFreeTextCommand.Route}", CreateEntryWithFreeTextCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{DeleteEntryCommand.Route}", DeleteEntryCommand.Handler.Handle).WithTags("ShoppingList");
    }

    public static void MapQueries(this WebApplication app)
    {
        app.MapGet($"/{ItemsQuery.Route}", ItemsQuery.Handler.Handle).WithTags("Category");
        app.MapGet($"/{CategoriesQuery.Route}", CategoriesQuery.Handler.Handle).WithTags("Category");
        app.MapGet($"/{CategoriesDetailsQuery.Route}", CategoriesDetailsQuery.Handler.Handle).WithTags("Category");
        app.MapGet($"/{ShoppingListQuery.Route}", ShoppingListQueryHandler.Handle).WithTags("ShoppingList");
    }
}