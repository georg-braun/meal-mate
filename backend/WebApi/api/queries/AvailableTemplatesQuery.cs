using domain;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public class AvailableTemplatesQuery
{
    public const string Route = nameof(AvailableTemplatesQuery);
    
    public static class Handler
    {
        public static async Task<List<AvailableTemplateDto>> Handle(MealMateContext context)
        {
            var templates = await context.Templates.ToListAsync();
            var dtos = templates.Select(ToDto);
            return dtos.ToList();
        }
        

        private static AvailableTemplateDto ToDto(Template template)
        {
            return new()
            {
                TemplateId = template.Id,
                Name = template.Name
            };
        }
    }
    

    public record AvailableTemplateDto
    {
        public Guid TemplateId { get; init; }
        public string Name { get; init; } = null!;
    }
}