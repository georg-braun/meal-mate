using api.database;
using domain;

namespace api.commands;

public record CreateItemCommand
{
    public static string Route = nameof(CreateItemCommand);
    public string Name { get; init; }
    public Guid CategoryId { get; init; }
}

public static class CreateItemCommandHandler
{
    public static async Task<CreateItemDto> Handle(CreateItemCommand command, MealMateContext context)
    {
        var item = await context.CreateItem(command.Name, command.CategoryId);
        return ToDto(item);
    }

    private static CreateItemDto ToDto(Item item)
    {
        return new CreateItemDto
        {
            Id = item.Id, 
            CategoryId = item.Category.Id, 
            Name = item.Name
        };
    }

    public record CreateItemDto
    {
        public Guid Id { get; init; }
        public Guid CategoryId { get; init; }
        public string Name { get; init; }
    }
}