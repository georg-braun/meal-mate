using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record DeleteEntryCommand : IRequest
{
    public required Guid ShoppingListId { get; init; }
    public required Guid EntryId { get; init; }


    public class DeleteEntryCommandHandler : IRequestHandler<DeleteEntryCommand>
    {
        private readonly MealMateContext _context;

        public DeleteEntryCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteEntryCommand request, CancellationToken cancellationToken)
        {
            await _context.DeleteEntryAsync(request.ShoppingListId, request.EntryId);
        }
    }
}