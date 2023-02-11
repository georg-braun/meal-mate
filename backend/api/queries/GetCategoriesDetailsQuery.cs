using api.database;
using domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.commands;

public class GetCategoriesDetailsQuery
{
    public static string Route = nameof(GetCategoriesDetailsQuery);
}


public static class GetCategoriesWithItemsQueryHandler
{
    public static async Task<List<GetCategoriesWithDetailsDto>> Handle(MealMateContext context)
    {
        var categories = await context.Categories.Include(_ => _.Items).ToListAsync();
        var dtos = categories.Select(ToDto);
        return dtos.ToList();
    }

    private static GetCategoriesWithDetailsDto ToDto(Category category)
    {
        return new GetCategoriesWithDetailsDto()
        {
            Id = category.Id,
            Name = category.Name,
            Items = category.Items.Select(ToDto).ToList()
        };
    }

    private static GetCategoriesWithDetailsItemDto ToDto(Item item)
    {
        return new GetCategoriesWithDetailsItemDto()
        {
            Id = item.Id,
            Name = item.Name
        };
    }
}

public record GetCategoriesWithDetailsItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}

public record GetCategoriesWithDetailsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<GetCategoriesWithDetailsItemDto> Items { get; init; }
}