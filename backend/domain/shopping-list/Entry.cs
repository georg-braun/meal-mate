namespace domain.shopping_list;

public class Entry : IEntity
{
    public Item Item { get; init; } = null!;
    public ShoppingList ShoppingList { get; init; } = new();
    public string Qualifier { get; init; } = null!;
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