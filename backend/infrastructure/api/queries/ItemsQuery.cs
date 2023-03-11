using domain;
using infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.api.queries;

public class ItemsQuery
{
    public const string Route = nameof(ItemsQuery);
    
    public static class Handler
    {
        public static async Task<IEnumerable<Response>> Handle(MealMateContext context)
        {
            var items = await context.Items.Include(_ => _.Category).ToListAsync();
            return items.Select(ToDto);
        }

        private static Response ToDto(Item item) =>
            new()
            {
                Id = item.Id,
                CategoryId = item.Category?.Id ?? Guid.Empty,
                Name = item.Name
            };

        public record Response
        {
            public Guid Id { get; init; }
            public Guid CategoryId { get; init; }
            public string Name { get; init; }
        }
    }
}

