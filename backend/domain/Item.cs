namespace domain;

public class Item : IEntity
{
    public string Name { get; set; }

    public Category? Category { get; set; }
    public Guid Id { get; init; }

    public void SetCategory(Category category)
    {
        Category = category;
    }

    public static Item Create(string name)
    {
        return new Item
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}