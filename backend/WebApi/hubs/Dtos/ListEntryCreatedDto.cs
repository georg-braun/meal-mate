namespace WebApi.hubs.Dtos;


public record ListEntryCreatedDto 
{
    public required Guid Id { get; init; }
    public required Guid ShoppingListId { get; init; }
    public required string Qualifier { get; init; }
    // public Item Item { get; init; }
}