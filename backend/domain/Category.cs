namespace domain;

public class Category : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    private List<Item> _items = new();
    
    public static Category Create(string name)
    {
        return new Category()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}