using System.Text;
using api.database;
using domain;

namespace api.commands;

public record CreateEntryCommand
{
    public static string Route = nameof(CreateEntryCommand);
    
    public Guid ItemId { get; init; }
    
    public Guid ShoppingListId { get; init; }
    public string Qualifier { get; init; }
}

public record CreateEntryWithFreeTextCommand
{
    public static string Route = nameof(CreateEntryWithFreeTextCommand);
    public Guid ShoppingListId { get; init; }
    
    public string FreeText { get; init; }
}


public static class CreateEntryCommandHandler
{
    public static async Task<IResult> Handle(CreateEntryCommand command, MealMateContext context)
    {
        await context.CreateEntryAsync(command.ItemId, command.ShoppingListId, command.Qualifier);
        return Results.Ok();
    }
}

public static class CreateEntryWithFreeTextCommandHandler
{
    public static async Task<IResult> Handle(CreateEntryWithFreeTextCommand command, MealMateContext context)
    {
        if (string.IsNullOrEmpty(command.FreeText))
            return Results.BadRequest("No freetext provided.");
        
        var itemName = string.Empty;
        var qualifier = string.Empty;
        var parts = command.FreeText.Split(" ");
        
        if (parts.Length == 1)
        {
            itemName = parts.First();
        }
        else
        {
            itemName = string.Join(" ", parts[..^1]);
            qualifier = parts[^1];
        }

        var item = await context.CreateItemIfDoesntExistAsync(itemName);
        
        await context.CreateEntryAsync(item.Id, command.ShoppingListId, qualifier);
        return Results.Ok();
    }
}