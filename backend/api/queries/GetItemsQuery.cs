using api.database;
using domain;
using Microsoft.EntityFrameworkCore;

namespace api.commands;

public class GetItemsQuery
{
    public static string Route = nameof(GetItemsQuery);
}

public static class GetItemsQueryHandler
{
    public static async Task<IEnumerable<GetItemsDto>> Handle(MealMateContext context)
    {
        var items = await context.Items.Include(_ => _.Category).ToListAsync();
        return items.Select(ToDto);
    }
    
    private static GetItemsDto ToDto(Item item)
    {
        return new GetItemsDto
        {
            Id = item.Id, 
            CategoryId = item.Category?.Id ?? Guid.Empty, 
            Name = item.Name
        };
    }

    public record GetItemsDto
    {
        public Guid Id { get; init; }
        public Guid CategoryId { get; init; }
        public string Name { get; init; }
    }
}