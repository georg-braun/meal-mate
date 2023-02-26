using api.shopping_list;
using domain;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.database;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    public DomainEventInterceptor(MealMateHubToClients mealMateHubToClients)
    {
        _mealMateHubToClients = mealMateHubToClients;
    }

    private MealMateHubToClients _mealMateHubToClients { get; }
    
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        
        HandleShoppingListChanges(dbContext);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void HandleShoppingListChanges(DbContext dbContext)
    {
       
        var shoppingListEvents = dbContext.ChangeTracker.Entries<ShoppingList>().Select(_ => _.Entity).SelectMany(shoppingList =>
        {
            var domainEvents = shoppingList.GetDomainEvents();
            
            shoppingList.ClearDomainEvents();
            return domainEvents;
        });
        
        foreach (var shoppingListEvent in shoppingListEvents)
        {
            switch (shoppingListEvent)
            {
                case EntryRemovedFromShoppingListDomainEvent removeEntryEvent:
                {
                    _mealMateHubToClients.SendRemoveEntryFromShoppingList(removeEntryEvent.ShoppingListId,
                        removeEntryEvent.EntryId);
                    break;    
                }
                case EntryCreatedOnShoppingListDomainEvent entryCreatedEvent:
                {
                    // todo: send the entity to the cleint
                    _mealMateHubToClients.SendCreateEntryOnShoppingList(entryCreatedEvent.ShoppingListId,
                        entryCreatedEvent.Entry );
                    break;    
                }
            }
                
        }

    }
}