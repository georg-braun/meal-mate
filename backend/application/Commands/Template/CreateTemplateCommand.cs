using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands.Template;

public class CreateTemplateCommand : IRequest<domain.Template>
{

    public required string Name { get; init; }
            
    public required List<Guid> Items { get; init; }
            
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
        var itemsInRepo = request.Items.Select( _ => _context.Items.Find(_)).ToList();
        List<Item> items = itemsInRepo.Where(item => item != null).ToList()!;
        
        var template = new domain.Template()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Instructions = request.Instructions,
            Items = items
            
        };

        await _context.Templates.AddAsync(template, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return template;
    }
}