using domain;
using infrastructure.database;

namespace infrastructure.api.commands;

public record CreateShoppingListCommand
{
    public const string Route = nameof(CreateShoppingListCommand);
    public string Name { get; init; }
    
    public static class Handler
    {
        public static async Task<IResult> Handle(CreateShoppingListCommand command, MealMateContext context)
        {
            var shoppingList = ShoppingList.Create(command.Name);
            var createdList = await context.ShoppingLists.AddAsync(shoppingList);
            await context.SaveChangesAsync();

            return createdList is null
                ? Results.UnprocessableEntity()
                : Results.Created($"GetShoppingList/?id={createdList.Entity.Id}", ToDto(createdList.Entity));
        }

        private static Response ToDto(ShoppingList shoppingList) =>
            new()
            {
                Name = shoppingList.Name,
                Id = shoppingList.Id
            };

        public record Response
        {
            public string Name { get; init; }
            public Guid Id { get; init; }
        }
    }
}

