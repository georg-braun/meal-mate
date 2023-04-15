namespace domain;

public class Template : IEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public List<Item> Items { get; init; } = new();

    public string Instructions { get; set; } = null!;
}