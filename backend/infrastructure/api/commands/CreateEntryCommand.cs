using infrastructure.database;

namespace infrastructure.api.commands;

public record CreateEntryCommand
{
    public static string Route = nameof(CreateEntryCommand);

    public Guid ItemId { get; init; }

    public Guid ShoppingListId { get; init; }
    public string Qualifier { get; init; }

    public static class Handler
    {
        public static async Task<IResult> Handle(CreateEntryCommand command, MealMateContext context)
        {
            await context.CreateEntryAsync(command.ItemId, command.ShoppingListId, command.Qualifier);
            return Results.Ok();
        }
    }
}