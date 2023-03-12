using domain;
using domain.shopping_list;
using Microsoft.AspNetCore.SignalR;
using WebApi.api.queries;
using WebApi.hubs.Dtos;

namespace WebApi.hubs;

/// <summary>
///     Knows how to send messages to the clients of the Meal Mate Hub.
/// </summary>
public class MealMateHubToClients
{
    public MealMateHubToClients(IHubContext<MealMateHub, IMealMateHubClientMethods> mealMateHub)
    {
        _mealMateHub = mealMateHub;
    }

    private IHubContext<MealMateHub, IMealMateHubClientMethods> _mealMateHub;


    /// <summary>
    ///     Inform the client that an entry got removed from the shopping list
    /// </summary>
    public async Task SendRemoveEntryFromShoppingListAsync(Guid shoppingListId, Guid entryId)
    {
        await _mealMateHub.Clients.Groups(shoppingListId.ToString())
            .RemoveEntryFromShoppingList(shoppingListId, entryId);
    }

    public async Task SendCreateEntryOnShoppingList(Guid shoppingListId, ListEntryCreatedDto entry)
    {
 
        await _mealMateHub.Clients.Groups(shoppingListId.ToString())
            .CreateEntryOnShoppingList(shoppingListId, entry);
    }
}