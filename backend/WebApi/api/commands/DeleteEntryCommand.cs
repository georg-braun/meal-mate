using MediatR;

namespace WebApi.api.commands;

public record DeleteEntryCommand
{
    public const string Route = nameof(DeleteEntryCommand);
    public Guid ShoppingListId { get; init; }
    public Guid EntryId { get; init; }

    public static class Handler
    {
        public static async Task<IResult> Handle(DeleteEntryCommand command, IMediator mediator)
        {
            await mediator.Send(new application.Commands.DeleteEntryCommand
                {EntryId = command.EntryId, ShoppingListId = command.ShoppingListId});
            return Results.Ok();
        }
    }
}