using Infrastructure.notifications;
using MediatR;
using WebApi.hubs;

namespace WebApi.Notifications;

public class ListEntryRemovedNotificationHandler : INotificationHandler<ListEntryRemovedNotification>
{
    private readonly MealMateHubToClients _mealMateHubToClients;

    public ListEntryRemovedNotificationHandler(MealMateHubToClients mealMateHubToClients)
    {
        _mealMateHubToClients = mealMateHubToClients;
    }

    public async Task Handle(ListEntryRemovedNotification notification, CancellationToken cancellationToken)
    {
        await _mealMateHubToClients.SendRemoveEntryFromShoppingListAsync(notification.ShoppingListId,
            notification.EntryId);
    }
}