using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands.Template;

public class CreateTemplateCommand : IRequest<domain.Template>
{

    public required string Name { get; init; }
            
    public required List<(Guid ItemId, string Name, string Amount)> Items { get; init; }
            
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
            Instructions = request.Instructions,
        };
        
        // Get the items from the database
        var items = new List<TemplateItem>();
        foreach(var templateItemInfos in request.Items)
        {
            // Try to get the item from the database
            var item = await _context.Items.FindAsync(new object?[] { templateItemInfos.ItemId }, cancellationToken: cancellationToken);

            // If the item is not in the database yet, create a new one.
            if (item is null)
            {
                item = Item.Create(templateItemInfos.Name);
                await _context.AddAsync(item, cancellationToken);
            };
            
            template.AddTemplateItem(item, templateItemInfos.Name, templateItemInfos.Amount);
        }


        await _context.Templates.AddAsync(template, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return template;
    }
}