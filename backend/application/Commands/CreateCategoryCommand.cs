using domain;
using Infrastructure.database;
using MediatR;

namespace application.Commands;

public record CreateCategoryCommand : IRequest<Category>
{
    public required string Name { get; init; }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly MealMateContext _context;

        public CreateCategoryCommandHandler(MealMateContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = Category.Create(request.Name);
            _context.Add(category);
            await _context.SaveChangesAsync(cancellationToken);
            return category;
        }
    }
}