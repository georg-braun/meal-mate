using domain;
using MediatR;

namespace WebApi.api.commands;

public record CreateShoppingListCommand
{
    public const string Route = nameof(CreateShoppingListCommand);
    public string Name { get; init; }

    public static class Handler
    {
        public static async Task<IResult> Handle(CreateShoppingListCommand command, IMediator mediator)
        {
            var shoppingList = await mediator.Send(new application.Commands.CreateShoppingListCommand {Name = command.Name});

            return shoppingList is null
                ? Results.UnprocessableEntity()
                : Results.Created($"GetShoppingList/?id={shoppingList.Id}", ToDto(shoppingList));
        }

        private static Response ToDto(ShoppingList shoppingList)
        {
            return new Response
            {
                Name = shoppingList.Name,
                Id = shoppingList.Id
            };
        }

        public record Response
        {
            public string Name { get; init; }
            public Guid Id { get; init; }
        }
    }
}