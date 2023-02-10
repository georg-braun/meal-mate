using api.database;
using domain;

namespace api.commands;

public record CreateCategoryCommand
{
    public static string Route = nameof(CreateCategoryCommand);
    public string Name { get; init; }
}

public static class CreateCategoryCommandHandler
{
    public static async Task<Category> Handle(CreateCategoryCommand command, MealMateContext context)
    {
        var category = Category.Create(command.Name);
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }
}