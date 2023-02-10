using api.database;
using domain;

namespace api.commands;

public record CreateShoppingListCommand
{
    public static string Route = nameof(CreateShoppingListCommand);
    public string Name { get; init; }
}

public static class CreateShoppingListCommandHandler
{
    public static async Task<IResult> Handle(CreateShoppingListCommand command, MealMateContext context)
    {
        var shoppingList = ShoppingList.Create(command.Name);
        context.ShoppingLists.Add(shoppingList);
        await context.SaveChangesAsync();
        return Results.Ok();
    }
}