using api.commands;

namespace api.shopping_list;

public interface IMealMateHubClientMethods
{
    Task RemoveEntryFromShoppingList(Guid shoppingListId, Guid entryId);
    Task CreateEntryOnShoppingList(Guid shoppingListId, ShoppingListQueryEntryDto entry);
}