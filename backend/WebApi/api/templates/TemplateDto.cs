using domain;

namespace WebApi.api.templates;

public class TemplateItemDto
{
    public required Guid Id { get; set; }
    
    /// <summary>
    ///     Can be null if the item is not in the database yet.
    ///     In this case the <see cref="Name"/> property should be filled.
    /// </summary>
    public Guid ItemId { get; set; }
    
    public string Name { get; set; } = null!;
}
public class TemplateDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public List<TemplateItemDto> Items { get; set; } = new();

    public string Instructions { get; set; } = null!;

    public static TemplateDto FromEntity(Template template)
    {
        return new TemplateDto()
        {
            Id = template.Id,
            Name = template.Name,
            Items = template.TemplateItems.Select(_ => new TemplateItemDto() {Id = _.Id, Name = _.Name}).ToList(),
            Instructions = template.Instructions
        };
    }
}

