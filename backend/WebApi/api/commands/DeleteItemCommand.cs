using MediatR;

namespace WebApi.api.commands;

public record DeleteItemCommand
{
    public const string Route = nameof(DeleteItemCommand);
    public Guid ItemId { get; init; }

    public static class Handler
    {
        public static async Task<IResult> Handle(DeleteItemCommand command, IMediator mediator)
        {
            await mediator.Send(new application.Commands.DeleteItemCommand {ItemId = command.ItemId});
            return Results.Ok();
        }
    }
}