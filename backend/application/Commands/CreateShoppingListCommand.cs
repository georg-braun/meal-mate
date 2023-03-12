using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateShoppingListCommand : IRequest<ShoppingList>
{
    public required string Name { get; init; }

    public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, ShoppingList>
    {
        private readonly MealMateContext _context;

        public CreateShoppingListCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task<ShoppingList> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = ShoppingList.Create(request.Name);
            var createdList = await _context.ShoppingLists.AddAsync(shoppingList);
            await _context.SaveChangesAsync(cancellationToken);
            return createdList.Entity;
        }
    }
}