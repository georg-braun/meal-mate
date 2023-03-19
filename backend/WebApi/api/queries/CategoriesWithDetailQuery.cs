using domain;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public class CategoriesWithDetailQuery
{
    public const string Route = nameof(CategoriesWithDetailQuery);
    
    public static class Handler
    {
        public static async Task<List<CategoriesWithDetailResponse>> Handle(MealMateContext context)
        {
            var categories = await context.Categories.Include(_ => _.Items).ToListAsync();
            var dtos = categories.Select(ToDto);
            return dtos.ToList();
        }

        private static CategoriesWithDetailResponse ToDto(Category category)
        {
            return new CategoriesWithDetailResponse
            {
                Id = category.Id,
                Name = category.Name,
                Items = category.Items.Select(ToDto).ToList()
            };
        }

        private static ItemDto ToDto(Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name
            };
        }
    }

    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }

    public record CategoriesWithDetailResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public List<ItemDto> Items { get; init; } = new();
    }
}

