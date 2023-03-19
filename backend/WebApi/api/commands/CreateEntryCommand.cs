using Infrastructure.database;
using MediatR;

namespace WebApi.api.commands;

public record CreateEntryCommand
{
    public const string Route = nameof(CreateEntryCommand);

    public Guid ItemId { get; init; }

    public Guid ShoppingListId { get; init; }
    
    public string Qualifier { get; init; } = null!;

    public static class Handler
    {
        public static async Task<IResult> Handle(CreateEntryCommand command, IMediator mediator)
        {
            await mediator.Send(new application.Commands.CreateEntryCommand()
            {
                ItemId = command.ItemId,
                ShoppingListId = command.ShoppingListId,
                Qualifier = command.Qualifier
            });
            
            return Results.Empty;
        }
    }
}