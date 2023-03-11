using infrastructure.api.queries;

namespace infrastructure.shopping_list;

public interface IMealMateHubClientMethods
{
    Task RemoveEntryFromShoppingList(Guid shoppingListId, Guid entryId);
    Task CreateEntryOnShoppingList(Guid shoppingListId, ShoppingListQueryEntryDto entry);
}