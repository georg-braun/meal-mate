using Infrastructure.database;

namespace WebApi.api.commands;

public record CreateEntryWithFreeTextCommand
{
    public const string Route = nameof(CreateEntryWithFreeTextCommand);
    public Guid ShoppingListId { get; init; }

    public string FreeText { get; init; }

    public static class Handler
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
}