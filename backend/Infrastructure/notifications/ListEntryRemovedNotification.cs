using MediatR;

namespace Infrastructure.notifications;

public class ListEntryRemovedNotification : INotification
{
    public ListEntryRemovedNotification(Guid shoppingListId, Guid entryId)
    {
        ShoppingListId = shoppingListId;
        EntryId = entryId;
    }

    public Guid EntryId { get; }

    public Guid ShoppingListId { get; }
}