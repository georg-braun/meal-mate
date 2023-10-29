using Infrastructure.notifications;
using MediatR;
using WebApi.hubs;
using WebApi.hubs.Dtos;

namespace WebApi.Notifications;

public class ListEntryCreatedNotificationHandler : INotificationHandler<ListEntryCreatedNotification>
{
    private readonly MealMateHubToClients _mealMateHubToClients;

    public ListEntryCreatedNotificationHandler(MealMateHubToClients mealMateHubToClients)
    {
        _mealMateHubToClients = mealMateHubToClients;
    }

    public async Task Handle(ListEntryCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _mealMateHubToClients.SendCreateEntryOnShoppingList(notification.ShoppingListId,
            new ListEntryCreatedDto()
            {
                Id = notification.Entry.Id,
                ShoppingListId = notification.ShoppingListId,
                ItemName = notification.Entry.Item.Name,
                ItemId = notification.Entry.Item.Id
            });
    }
}