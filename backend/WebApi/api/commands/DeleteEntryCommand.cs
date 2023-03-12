using Infrastructure.database;

namespace WebApi.api.commands;

public record DeleteEntryCommand
{
    public const string Route = nameof(DeleteEntryCommand);
    public Guid ShoppingListId { get; init; }
    public Guid EntryId { get; init; }
    
    public static class Handler
    {
        public static async Task Handle(DeleteEntryCommand command, MealMateContext context)
        {
            await context.DeleteEntryAsync(command.ShoppingListId, command.EntryId);
        }
    }
}

