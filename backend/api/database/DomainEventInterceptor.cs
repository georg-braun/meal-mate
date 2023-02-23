using domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.database;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChanges(eventData, result);
        
        HandleShoppingListChanges(dbContext);

        return base.SavingChanges(eventData, result);
    }

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
       
        dbContext.ChangeTracker.Entries<ShoppingList>().Select(_ => _.Entity).SelectMany(shoppingList =>
        {
            var domainEvents = shoppingList.GetDomainEvents();
            shoppingList.ClearDomainEvents();
            return domainEvents;
        });

    }
}