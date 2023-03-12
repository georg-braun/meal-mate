using domain;
using Infrastructure.notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Infrastructure.database;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DomainEventInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        HandleShoppingListChanges(dbContext);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void HandleShoppingListChanges(DbContext dbContext)
    {
        var shoppingListEvents = dbContext.ChangeTracker.Entries<ShoppingList>().Select(_ => _.Entity).SelectMany(
            shoppingList =>
            {
                var domainEvents = shoppingList.GetDomainEvents();

                shoppingList.ClearDomainEvents();
                return domainEvents;
            });

        foreach (var shoppingListEvent in shoppingListEvents)
            switch (shoppingListEvent)
            {
                case EntryRemovedFromShoppingListDomainEvent removeEntryEvent:
                {
                    _mediator.Publish(new ListEntryRemovedNotification(removeEntryEvent.ShoppingListId,
                        removeEntryEvent.EntryId));
                    break;
                }
                case EntryCreatedOnShoppingListDomainEvent entryCreatedEvent:
                {
                    _mediator.Publish(new ListEntryCreatedNotification(entryCreatedEvent.ShoppingListId,
                        entryCreatedEvent.Entry));
                    break;
                }
            }
    }
}