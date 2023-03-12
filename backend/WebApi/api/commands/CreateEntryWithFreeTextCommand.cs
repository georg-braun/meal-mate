using MediatR;

namespace WebApi.api.commands;

public record CreateEntryWithFreeTextCommand
{
    public const string Route = nameof(CreateEntryWithFreeTextCommand);
    public Guid ShoppingListId { get; init; }
    public string FreeText { get; init; }

    public static class Handler
    {
        public static async Task<IResult> Handle(CreateEntryWithFreeTextCommand command, IMediator mediator)
        {
            if (string.IsNullOrEmpty(command.FreeText))
                return Results.BadRequest("No freetext provided.");

            await mediator.Send(new application.Commands.CreateEntryWithFreeTextCommand()
            {
                FreeText = command.FreeText,
                ShoppingListId = command.ShoppingListId
            });
            
            return Results.Ok();
        }
    }
}