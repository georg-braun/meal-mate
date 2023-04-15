using domain;

namespace WebApi.api.templates;

public class TemplateDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public List<Guid> Items { get; set; } = new();

    public string Instructions { get; set; } = null!;

    public static TemplateDto FromEntity(Template template)
    {
        return new TemplateDto()
        {
            Id = template.Id,
            Name = template.Name,
            Items = template.Items.Select(_ => _.Id).ToList(),
            Instructions = template.Instructions
        };
    }
}