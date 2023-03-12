using domain.shopping_list;

namespace domain;

public interface IDomainEvent
{
    public Guid Id { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredUtc { get; } = DateTime.Now.ToUniversalTime();
    public Guid Id { get; } = Guid.NewGuid();
}

public class EntryRemovedFromShoppingListDomainEvent : DomainEvent
{
    public EntryRemovedFromShoppingListDomainEvent(Guid shoppingListId, Guid entryId)
    {
        ShoppingListId = shoppingListId;
        EntryId = entryId;
    }

    public Guid ShoppingListId { get; }
    public Guid EntryId { get; }
}

public class EntryCreatedOnShoppingListDomainEvent : DomainEvent
{
    public EntryCreatedOnShoppingListDomainEvent(Guid shoppingListId, Entry entry)
    {
        ShoppingListId = shoppingListId;
        Entry = entry;
    }

    public Guid ShoppingListId { get; }
    public Entry Entry { get; }
}