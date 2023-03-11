using domain;
using infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.api.queries;

public static class ShoppingListQuery
{
    public const string Route = nameof(ShoppingListQuery);
}

public static class ShoppingListQueryHandler
{
    public static async Task<ShoppingListQueryResponse> Handle(Guid id, MealMateContext context)
    {
        var shoppingList = await context.ShoppingLists.Include(_ => _.Entries).ThenInclude(_ => _.Item)
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        if (shoppingList is null) return null;

        return ToDto(shoppingList);
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
    public string ItemName { get; init; }
    public string Qualifier { get; init; }
}

public record ShoppingListQueryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<ShoppingListQueryEntryDto> Entries { get; init; }
}