using Infrastructure.database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Commands;

public record ApplyTemplateCommand : IRequest
{
    public required Guid ListId { get; init; }
    public required Guid TemplateId { get; init; }

    public class ApplyTemplateCommandHandler : IRequestHandler<ApplyTemplateCommand>
    {
        private readonly MealMateContext _context;
        private readonly IMediator _mediator;

        public ApplyTemplateCommandHandler(MealMateContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(ApplyTemplateCommand request, CancellationToken cancellationToken)
        {
            // find the template
            var template = await _context.Templates.Include(_ => _.TemplateItems).FirstOrDefaultAsync(_ => _.Id == request.TemplateId, cancellationToken);
            
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

                await _mediator.Send(new CreateEntryCommand()
                {
                    Name = templateItem.Name,
                    Qualifier = templateItem.Qualifier,
                    ShoppingListId = list.Id
                }, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}