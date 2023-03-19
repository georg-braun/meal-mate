using domain;
using domain.shopping_list;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public static class ShoppingListQuery
{
    public const string Route = nameof(ShoppingListQuery);
}

public static class ShoppingListQueryHandler
{
    public static async Task<IResult> Handle(Guid id, MealMateContext context)
    {
        var shoppingList = await context.ShoppingLists.Include(_ => _.Entries).ThenInclude(_ => _.Item)
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        if (shoppingList is null) return Results.NoContent();

        return Results.Ok(ToDto(shoppingList));
    }

    private static ShoppingListQueryResponse ToDto(ShoppingList shoppingList)
    {
        return new ShoppingListQueryResponse
        {
            Id = shoppingList.Id,
            Name = shoppingList.Name,
            Entries = shoppingList.Entries.Select(ToDto).ToList()
        };
    }

    public static ShoppingListQueryEntryDto ToDto(Entry entry)
    {
        return new ShoppingListQueryEntryDto
        {
            Id = entry.Id,
            ItemName = entry.Item.Name,
            ItemId = entry.Item.Id,
            Qualifier = entry.Qualifier
        };
    }
}

public record ShoppingListQueryEntryDto
{
    public Guid Id { get; init; }
    public Guid ItemId { get; init; }
    public string ItemName { get; init; } = null!;
    public string Qualifier { get; init; } = null!;
}

public record ShoppingListQueryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public List<ShoppingListQueryEntryDto> Entries { get; init; } = new();
}