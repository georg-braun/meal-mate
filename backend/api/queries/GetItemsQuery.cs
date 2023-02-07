namespace api.commands;

public class GetItemsQuery
{
    public static string Route = nameof(GetItemsQuery);
}

public static class GetItemsQueryHandler
{
    public static IResult Handle(GetItemsQuery command)
    {
        return Results.Ok();
    }
}