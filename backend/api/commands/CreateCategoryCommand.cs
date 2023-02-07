namespace api.commands;

public record CreateCategoryCommand
{
    public static string Route = nameof(CreateCategoryCommand);
    public string Name { get; init; }
}

public static class CreateCategoryCommandHandler
{
    public static IResult Handle(CreateCategoryCommand command)
    {
        return Results.Ok();
    }
}