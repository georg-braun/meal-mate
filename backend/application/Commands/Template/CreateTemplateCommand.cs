using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands.Template;

public class CreateTemplateCommand : IRequest<domain.Template>
{

    public required string Name { get; init; }
            
    public required List<(string Name, string Qualifier)> Items { get; init; }
            
    public required string Instructions { get; init; }

}

// write handler for template
public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, domain.Template>
{
    private readonly MealMateContext _context;

    public CreateTemplateCommandHandler(MealMateContext context)
    {
        _context = context;
    }

    public async Task<domain.Template> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
    {
                
        var template = new domain.Template()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Instructions = request.Instructions ?? string.Empty,
            
        };
        
        // Get the items from the database
        foreach(var templateItemInfos in request.Items)
        {
            template.AddTemplateItem(templateItemInfos.Name, templateItemInfos.Qualifier);
        }
        
        await _context.Templates.AddAsync(template, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return template;
    }
}