using WebApi.hubs.Dtos;

namespace WebApi.hubs;

public interface IMealMateHubClientMethods
{
    Task RemoveEntryFromShoppingList(Guid shoppingListId, Guid entryId);
    Task CreateEntryOnShoppingList(Guid shoppingListId, ListEntryCreatedDto entry);
}