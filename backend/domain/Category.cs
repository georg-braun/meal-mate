namespace domain;

public class Category : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    private List<Item> _items = new();

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var index = _items.FindIndex(_ => _.Id.Equals(itemId));
        if (index < 0) return;
        
        _items.RemoveAt(index);
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