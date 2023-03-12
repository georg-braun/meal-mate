using domain;
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;

namespace WebApi.api.queries;

public class CategoriesDetailsQuery
{
    public const string Route = nameof(CategoriesDetailsQuery);
    
    public static class Handler
    {
        public static async Task<List<Response>> Handle(MealMateContext context)
        {
            var categories = await context.Categories.Include(_ => _.Items).ToListAsync();
            var dtos = categories.Select(ToDto);
            return dtos.ToList();
        }

        private static Response ToDto(Category category)
        {
            return new Response
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
        public string Name { get; init; }
    }

    public record Response
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<ItemDto> Items { get; init; }
    }
}

