using domain;
using infrastructure.database;

namespace infrastructure.api.commands;

public record CreateItemCommand
{
    public static string Route = nameof(CreateItemCommand);
    public string Name { get; init; }
    public Guid CategoryId { get; init; }

    public static class Handler
    {
        public static async Task<Response> Handle(CreateItemCommand command, MealMateContext context)
        {
            var item = await context.CreateItem(command.Name, command.CategoryId);
            return ToDto(item);
        }

        private static Response ToDto(Item item)
        {
            return new Response
            {
                Id = item.Id,
                CategoryId = item.Category.Id,
                Name = item.Name
            };
        }

        public record Response
        {
            public Guid Id { get; init; }
            public Guid CategoryId { get; init; }
            public string Name { get; init; }
        }
    }
}