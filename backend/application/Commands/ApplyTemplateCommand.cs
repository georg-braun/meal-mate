using System.Runtime.Versioning;
using Infrastructure.database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace application.Commands;

public record ApplyTemplateCommand : IRequest
{
    public required Guid ListId { get; init; }
    public required Guid TemplateId { get; init; }

    public class ApplyTemplateCommandHandler : IRequestHandler<ApplyTemplateCommand>
    {
        private readonly MealMateContext _context;

        public ApplyTemplateCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(ApplyTemplateCommand request, CancellationToken cancellationToken)
        {
            // todo: Apply the template
            // find the template
            var template = await _context.Templates.Include(_ => _.TemplateItems).ThenInclude(_ => _.Item).FirstOrDefaultAsync(_ => _.Id == request.TemplateId, cancellationToken);
            
            if (template is null)
            {
                throw new ArgumentException("Could not find template with id {RequestTemplateId}", nameof(request.TemplateId));
            }

            // find the list
            var list = await _context.ShoppingLists.Include(_ => _.Entries).FirstOrDefaultAsync(_ => _.Id == request.ListId, cancellationToken);

            if (list is null)
            {
                throw new ArgumentException("Could not find list with id {RequestListId}", nameof(request.ListId));
            }
           
            foreach (var templateItem in template.TemplateItems)
            {
                // check for empty item
                if (templateItem.Item is null)
                {
                    Log.Logger.Warning($"Template item {templateItem.Id} has no item");
                    continue;
                }
                
                list.CreateEntry(templateItem.Item, templateItem.Qualifier);
            }

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}