using domain;
using domain.shopping_list;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateEntryCommand : IRequest
{
    public required Guid ShoppingListId { get; init; }
    
    public required string Name { get; init; }
    

    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand>
    {
        private readonly MealMateContext _context;

        public CreateEntryCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            
            var item = await _context.CreateItemIfDoesntExistAsync(request.Name);
 
            var shoppingList = await _context.ShoppingLists.FindAsync(request.ShoppingListId);

            if (item is null)
            {
                Console.WriteLine($"Can't find item {request.Name}.");
                return;
            }
            if (shoppingList is null)
            {
                Console.WriteLine($"Can't find shopping list {request.ShoppingListId}.");
                return;
            }
            
            shoppingList.CreateEntry(item);
            // update the shopping list aggregate
            _context.ShoppingLists.Update(shoppingList);
            
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}