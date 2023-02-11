namespace domain;

public class Category : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public List<Item> Items { get; init; } = new();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var index = Items.FindIndex(_ => _.Id.Equals(itemId));
        if (index < 0) return;
        
        Items.RemoveAt(index);
    }
    
    public static Category Create(string name)
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}