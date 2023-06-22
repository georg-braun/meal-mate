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

    public void UpdateTemplateItem(Guid templateItemId, string templateItemsName, string templateItemsAmount)
    {
        var templateItem = TemplateItems.FirstOrDefault(_ => _.Id.Equals(templateItemId));
        if (templateItem is null) return;
        
        templateItem.Name = templateItemsName;
        templateItem.Qualifier = templateItemsAmount;
    }

    public void AddTemplateItem(string itemName, string qualifier)
    {
        TemplateItems.Add(new TemplateItem()
        {
            Id = Guid.NewGuid(),
            Name = itemName,
            Qualifier = qualifier
        });
    }
}

public class TemplateItem
{
    public Guid Id { get; set; }
    public string Qualifier { get; set; } = null!;
    public string Name { get; set; } = null!;
}