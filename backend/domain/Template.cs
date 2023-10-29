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

    public void UpdateTemplateItem(Guid templateItemId, string templateItemsName)
    {
        var templateItem = TemplateItems.FirstOrDefault(_ => _.Id.Equals(templateItemId));
        if (templateItem is null) return;
        
        templateItem.Name = templateItemsName;
    }

    public void AddTemplateItem(string itemName)
    {
        TemplateItems.Add(new TemplateItem()
        {
            Id = Guid.NewGuid(),
            Name = itemName,
        });
    }
}

public class TemplateItem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
}