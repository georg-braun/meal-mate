namespace domain;

public class Item : IEntity
{
    public string Name { get; set; } = null!;
    
    public Guid Id { get; init; }


    public static Item Create(string name)
    {
        return new Item
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}