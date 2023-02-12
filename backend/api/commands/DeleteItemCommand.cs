using api.database;
using domain;

namespace api.commands;

public record DeleteItemCommand
{
    public static string Route = nameof(DeleteItemCommand);
    public Guid ItemId { get; init; }
}

public static class DeleteItemCommandHandler
{
    public static async Task Handle(DeleteItemCommand command, MealMateContext context)
    {
        await context.DeleteItemAsync(command.ItemId);
    }
}