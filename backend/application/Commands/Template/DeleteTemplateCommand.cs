using domain;
using Infrastructure.database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Commands.Template;

public class DeleteTemplateCommand : IRequest<bool>
{
    public Guid Id { get; init; }
}


// write handler for template
public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, bool>
{
    private readonly MealMateContext _context;

    public DeleteTemplateCommandHandler(MealMateContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _context.Templates.Include(_ => _.TemplateItems).FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if (template == null) return false;

        // Delete the template items
        foreach (var templateItem in template.TemplateItems)
        {
            _context.Remove(templateItem);
        }

        _context.Templates.Remove(template);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}