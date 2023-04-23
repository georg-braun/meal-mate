using domain;
using Infrastructure.database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace application.Commands.Template;

public class UpdateTemplateCommand : IRequest<domain.Template>
{

    public required Guid Id { get; init; }
    public required string Name { get; init; }
            
    public required List<(Guid Id, Guid ItemId, string Name, string Amount)> Items { get; init; }
            
    public required string Instructions { get; init; }

}

// write handler for template
public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, domain.Template>
{
    private readonly MealMateContext _context;

    public UpdateTemplateCommandHandler(MealMateContext context)
    {
        _context = context;
    }

    public async Task<domain.Template> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        // Get the template from the database
        var existingTemplate = await _context.Templates.Include(_ => _.TemplateItems).ThenInclude(_ => _.Item).FirstOrDefaultAsync(_ => _.Id.Equals(request.Id), cancellationToken: cancellationToken);
        if (existingTemplate is null) throw new ArgumentException($"Couldn't find template with id {request.Id}.");

        RemoveTemplateItems(request, existingTemplate);
        await UpdateTemplateItems(request, cancellationToken, existingTemplate);
        await AddNewTemplateItems(request, cancellationToken, existingTemplate);

        existingTemplate.Name = request.Name;
        existingTemplate.Instructions = request.Instructions;
        
        await _context.SaveChangesAsync(cancellationToken);

        return existingTemplate;
    }

    private static void RemoveTemplateItems(UpdateTemplateCommand request, domain.Template existingTemplate)
    {
        // Remove the item in the database if it's not in the request anymore
        foreach (var existingTemplateItemId in existingTemplate.TemplateItems.Where(_ => _.Item != null)
                     .Select(_ => _.Item!.Id))
        {
            if (request.Items.Select(_ => _.ItemId).Contains(existingTemplateItemId))
            {
                continue;
            }

            // todo: check if this is enough or the item should be removed explicitly from the database.
            existingTemplate.RemoveTemplateItem(existingTemplateItemId);
        }
    }

    private async Task UpdateTemplateItems(UpdateTemplateCommand request, CancellationToken cancellationToken,
        domain.Template existingTemplate)
    {
        // Update the existing items
        var templateItemsToUpdate = request.Items.Where(_ => _.ItemId != Guid.Empty).ToList();
        foreach (var templateItemToUpdate in templateItemsToUpdate)
        {
            var item = await _context.Items.FindAsync(new object?[] {templateItemToUpdate.ItemId},
                cancellationToken: cancellationToken);
            existingTemplate.UpdateTemplateItem(templateItemToUpdate.Id, item, templateItemToUpdate.Name,
                templateItemToUpdate.Amount);
        }
    }

    private async Task AddNewTemplateItems(UpdateTemplateCommand request, CancellationToken cancellationToken,
        domain.Template existingTemplate)
    {
        var newTemplateItems = request.Items.Where(_ => _.ItemId == Guid.Empty).ToList();
        foreach (var newTemplateItem in newTemplateItems)
        {
            // Try to get the item from the database
            var item = await _context.Items.FindAsync(new object?[] {newTemplateItem.ItemId},
                cancellationToken: cancellationToken);

            // If the item is not in the database yet, create a new one.
            if (item is null)
            {
                item = Item.Create(newTemplateItem.Name);
                await _context.AddAsync(item, cancellationToken);
            }

            ;

            existingTemplate.AddTemplateItem(item, newTemplateItem.Name, newTemplateItem.Amount);
        }
    }
}