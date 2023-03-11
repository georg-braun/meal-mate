using infrastructure.database;

namespace infrastructure.api.commands;

public record DeleteItemCommand
{
    public const string Route = nameof(DeleteItemCommand);
    public Guid ItemId { get; init; }
    
    public static class Handler
    {
        public static async Task Handle(DeleteItemCommand command, MealMateContext context)
        {
            await context.DeleteItemAsync(command.ItemId);
        }
    }
}