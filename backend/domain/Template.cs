namespace domain;

public class Template : IEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public List<TemplateItem> TemplateItems { get; init; } = new();

    public string Instructions { get; set; } = null!;

    public void RemoveTemplateItem(Guid templateItemId)
    {
        TemplateItems.RemoveAll(_ => _.Id.Equals(templateItemId));
    }

    public void UpdateTemplateItem(Guid templateItemId, Item? item, string templateItemsName, string templateItemsAmount)
    {
        var templateItem = TemplateItems.FirstOrDefault(_ => _.Id.Equals(templateItemId));
        if (templateItem is null) return;
        
        templateItem.Item = item;
        templateItem.Name = templateItemsName;
        templateItem.Amount = templateItemsAmount;
    }

    public void AddTemplateItem(Item item, string newItemName, string newItemAmount)
    {
        TemplateItems.Add(new TemplateItem()
        {
            Id = Guid.NewGuid(),
            Item = item,
            Name = newItemName,
            Amount = newItemAmount
        });
    }
}

public class TemplateItem
{
    public Guid Id { get; set; }
    public Item? Item { get; set; } = null!;
    public string Amount { get; set; } = null!;
    public string Name { get; set; } = null!;
}