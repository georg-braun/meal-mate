namespace domain;

public class ShoppingList : AggregateRoot, IEntity
{
    public string Name { get; init; }

    public List<Entry> Entries { get; init; } = new();
    public Guid Id { get; init; }

    public Entry CreateEntry(Item item, string qualifier)
    {
        var entry = Entry.Create(item, this, qualifier);
        Entries.Add(entry);

        RaiseDomainEvent(new EntryCreatedOnShoppingListDomainEvent(Id, entry));
        return entry;
    }

    public bool RemoveEntry(Guid entryId)
    {
        var entryIndex = Entries.FindIndex(_ => _.Id.Equals(entryId));
        if (entryIndex < 0) return false;
        Entries.RemoveAt(entryIndex);

        RaiseDomainEvent(new EntryRemovedFromShoppingListDomainEvent(Id, entryId));

        return true;
    }

    public static ShoppingList Create(string name)
    {
        return new ShoppingList
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}