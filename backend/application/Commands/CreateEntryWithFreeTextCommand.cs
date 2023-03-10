using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateEntryWithFreeTextCommand : IRequest
{
    public required Guid ShoppingListId { get; init; }

    public required string FreeText { get; init; }

    public class CreateEntryWithFreeTextCommandHandler : IRequestHandler<CreateEntryWithFreeTextCommand>
    {
        private readonly MealMateContext _context;

        public CreateEntryWithFreeTextCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateEntryWithFreeTextCommand request, CancellationToken cancellationToken)
        {
            var itemName = string.Empty;
            var qualifier = string.Empty;
            var parts = request.FreeText.Split(" ");

            if (parts.Length == 1)
            {
                itemName = parts.First();
            }
            else
            {
                itemName = string.Join(" ", parts[..^1]);
                qualifier = parts[^1];
            }

            var item = await _context.CreateItemIfDoesntExistAsync(itemName);

            var shoppingList = await _context.ShoppingLists.FindAsync(request.ShoppingListId);
            shoppingList.CreateEntry(item, qualifier);
            // update the shopping list aggregate
            _context.ShoppingLists.Update(shoppingList);


            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}