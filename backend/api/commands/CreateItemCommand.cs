using api.database;

namespace api.commands;

public record CreateItemCommand
{
    public static string Route = nameof(CreateItemCommand);
    public string Name { get; init; }
    public Guid CategoryId { get; init; }
}

public static class CreateItemCommandHandler
{
    public static IResult Handle(CreateItemCommand command, MealMateContext context)
    {
        var item = context.CreateItem(command.Name, command.CategoryId);
        
        return Results.Created("todo", item);
    }
}