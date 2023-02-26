using api.commands;
using domain;
using Microsoft.AspNetCore.SignalR;

namespace api.shopping_list;

/// <summary>
///     Knows how to send messages to the clients of the Meal Mate Hub.
/// </summary>
public class MealMateHubToClients
{
    
    public MealMateHubToClients(IHubContext<MealMateHub, IMealMateHubClientMethods> mealMateHub)
    {
        _mealMateHub = mealMateHub;
    }

    private IHubContext<MealMateHub, IMealMateHubClientMethods> _mealMateHub { get; }
    
    
    /// <summary>
    ///     Inform the client that an entry got removed from the shopping list
    /// </summary>
    public async Task SendRemoveEntryFromShoppingList(Guid shoppingListId, Guid entryId)
    {
        await _mealMateHub.Clients.Groups(shoppingListId.ToString()).RemoveEntryFromShoppingList(shoppingListId, entryId);
    }    
    
    public async Task SendCreateEntryOnShoppingList(Guid shoppingListId, Entry entry)
    {
        var entryDto = ShoppingListQueryHandler.ToDto(entry);
        await _mealMateHub.Clients.Groups(shoppingListId.ToString()).CreateEntryOnShoppingList(shoppingListId, entryDto);
    }
}