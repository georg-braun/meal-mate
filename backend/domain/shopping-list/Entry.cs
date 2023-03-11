namespace domain;

public class Entry : IEntity
{
    public Item Item { get; init; }
    public ShoppingList ShoppingList { get; init; }
    public string Qualifier { get; init; }
    public Guid Id { get; init; }

    public static Entry Create(Item item, ShoppingList list, string qualifier)
    {
        return new Entry
        {
            Id = Guid.NewGuid(),
            Item = item,
            ShoppingList = list,
            Qualifier = qualifier
        };
    }
}