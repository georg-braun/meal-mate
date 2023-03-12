using domain.shopping_list;
using MediatR;

namespace Infrastructure.notifications;

public class ListEntryCreatedNotification : INotification
{
    public ListEntryCreatedNotification(Guid shoppingListId, Entry entry)
    {
        ShoppingListId = shoppingListId;
        Entry = entry;
    }

    public Entry Entry { get; }

    public Guid ShoppingListId { get; }
}