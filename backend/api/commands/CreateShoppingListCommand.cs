using api.database;
using domain;

namespace api.commands;

public record CreateShoppingListCommand
{
    public static string Route = nameof(CreateShoppingListCommand);
    public string Name { get; init; }
}

public static class CreateShoppingListCommandHandler
{
    public static async Task<IResult> Handle(CreateShoppingListCommand command, MealMateContext context)
    {
        var shoppingList = ShoppingList.Create(command.Name);
        var createdList = await context.ShoppingLists.AddAsync(shoppingList);
        await context.SaveChangesAsync();

        return createdList is null ? Results.UnprocessableEntity() : Results.Created($"GetShoppingList/?id={createdList.Entity.Id}", ToDto(createdList.Entity));
    }

    private static CreateShoppingListCommandResponse ToDto(ShoppingList shoppingList)
    {
        return new CreateShoppingListCommandResponse()
        {
            Name = shoppingList.Name,
            Id = shoppingList.Id
        };
    }
    
    public record CreateShoppingListCommandResponse
    {
        public string Name { get; init; }
        public Guid Id { get; init; }
    }
}