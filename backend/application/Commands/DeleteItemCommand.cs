using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record DeleteItemCommand : IRequest
{
    public required Guid ItemId { get; init; }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
    {
        private readonly MealMateContext _context;

        public DeleteItemCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            await _context.DeleteItemAsync(request.ItemId);
        }
    }
}