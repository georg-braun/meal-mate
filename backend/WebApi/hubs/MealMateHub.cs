using Infrastructure.database;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.hubs;

public class MealMateHub : Hub<IMealMateHubClientMethods>
{
    private readonly MealMateContext _mealMateContext;
    
    /// <summary>
    ///     This is just an informative overview over connected clients.
    ///     The purpose is to be able to detect if a client is still connected.
    /// </summary>
    public static readonly List<SignalRConnectionInfo> ConnectedIds = new();

    public MealMateHub(MealMateContext mealMateContext)
    {
        _mealMateContext = mealMateContext;
    }

    public async Task<bool> StartListeningToShoppingListChanges(string id)
    {
        var shoppingListId = Guid.Parse(id);
        if (!await _mealMateContext.ShoppingListExistsAsync(shoppingListId))
            return false;
        
        await Groups.AddToGroupAsync(Context.ConnectionId, id);
        
        ConnectedIds.Add(new SignalRConnectionInfo(id, Context.ConnectionId, DateTime.UtcNow));
        
        Console.WriteLine($"{Context.ConnectionId} starts listening to shopping list {id}.");
        return true;
        
        
    }

    public async Task<bool> StopListeningToShoppingListChanges(string id)
    {
        var shoppingListId = Guid.Parse(id);
        if (!await _mealMateContext.ShoppingListExistsAsync(shoppingListId))
            return false;
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
        ConnectedIds.RemoveAll(_ => _.GroupId.Equals(id) && _.ConnectionId.Equals(Context.ConnectionId));
        
        Console.WriteLine($"{Context.ConnectionId} stop listening to shopping list {id}.");
        return true;
    }
}

public record SignalRConnectionInfo(string GroupId, string ConnectionId, DateTime ConnectedAt);