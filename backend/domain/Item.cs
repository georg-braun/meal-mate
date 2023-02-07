namespace domain;

public class Item : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    public Category Category { get; init; }
    
    public static Item Create(string name)
    {
        return new Item()
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}