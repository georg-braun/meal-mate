using domain;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public class ItemsQuery
{
    public const string Route = nameof(ItemsQuery);
    
    public static class Handler
    {
        public static async Task<IEnumerable<ItemsQueryResponse>> Handle(MealMateContext context)
        {
            var items = await context.Items.Include(_ => _.Category).ToListAsync();
            return items.Select(ToDto);
        }

        private static ItemsQueryResponse ToDto(Item item) =>
            new()
            {
                Id = item.Id,
                CategoryId = item.Category?.Id ?? Guid.Empty,
                CategoryName = item.Category?.Name ?? string.Empty,
                Name = item.Name
            };

        public record ItemsQueryResponse
        {
            public Guid Id { get; init; }
            public Guid CategoryId { get; init; }
            public string CategoryName { get; init; }
            public string Name { get; init; }
        }
    }
}

