using infrastructure.database;
using Microsoft.AspNetCore.SignalR;

namespace infrastructure.shopping_list;

public class MealMateHub : Hub<IMealMateHubClientMethods>
{
    private readonly MealMateContext _mealMateContext;

    public MealMateHub(MealMateContext mealMateContext)
    {
        _mealMateContext = mealMateContext;
    }

    public async Task Listen(string id)
    {
        Console.WriteLine(id);
    }

    public async Task<bool> StartListeningToShoppingListChanges(string id)
    {
        var shoppingListId = Guid.Parse(id);
        if (!await _mealMateContext.ShoppingListExistsAsync(shoppingListId))
            return false;

        await Groups.AddToGroupAsync(Context.ConnectionId, id);
        Console.WriteLine($"{Context.ConnectionId} starts listening to shopping list {id}.");
        return true;
    }

    public async Task<bool> StopListeningToShoppingListChanges(string id)
    {
        var shoppingListId = Guid.Parse(id);
        if (!await _mealMateContext.ShoppingListExistsAsync(shoppingListId))
            return false;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
        Console.WriteLine($"{Context.ConnectionId} stop listening to shopping list {id}.");
        return true;
    }
}