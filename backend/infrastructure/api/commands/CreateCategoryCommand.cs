using domain;
using infrastructure.database;

namespace infrastructure.api.commands;

public record CreateCategoryCommand
{
    public const string Route = nameof(CreateCategoryCommand);
    public string Name { get; init; }

    public static class Handler
    {
        public static async Task<Category> Handle(CreateCategoryCommand command, MealMateContext context)
        {
            var category = Category.Create(command.Name);
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }
    }
}