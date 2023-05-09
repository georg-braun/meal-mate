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

    /// <summary>
    ///     Removes template items from the template which aren't used anymore.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="existingTemplate"></param>
    private static void RemoveTemplateItems(UpdateTemplateCommand request, domain.Template existingTemplate)
    {
        var oldTemplateItems= existingTemplate.TemplateItems.Select(_ => _.Id).ToList();
        var newTemplateItems = request.Items.Select(_ => _.Id).ToHashSet();
        foreach (var oldTemplateItem in oldTemplateItems)
        {
            // Skip if the TemplateItem should still be there.
            if (newTemplateItems.Contains(oldTemplateItem))
            {
                continue;
            }
            
            existingTemplate.RemoveTemplateItem(oldTemplateItem);
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