using domain;
using domain.shopping_list;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateEntryCommand : IRequest
{
    public required Guid ItemId { get; init; }

    public required Guid ShoppingListId { get; init; }
    
    public required string Qualifier { get; init; }

    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand>
    {
        private readonly MealMateContext _context;

        public CreateEntryCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.ItemId);
            var shoppingList = await _context.ShoppingLists.FindAsync(request.ShoppingListId);
            
            shoppingList.CreateEntry(item, request.Qualifier);
            // update the shopping list aggregate
            _context.ShoppingLists.Update(shoppingList);
            
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}