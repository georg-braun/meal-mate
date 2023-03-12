using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateItemCommand : IRequest<Item>
{
    public required Guid CategoryId { get; init; }
    public required string Name { get; init; }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Item>
    {
        private readonly MealMateContext _context;

        public CreateItemCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.CreateItem(request.Name, request.CategoryId);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }
    }
}