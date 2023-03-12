using domain;
using Infrastructure.database;
using MediatR;

namespace WebApi.api.commands;

public record CreateCategoryCommand
{
    public const string Route = nameof(CreateCategoryCommand);
    public string Name { get; init; }

    public static class Handler
    {
        public static async Task<Category> Handle(CreateCategoryCommand command, IMediator mediator)
        {
            var category = await mediator.Send(new application.Commands.CreateCategoryCommand() {Name = command.Name});
            return category;
        }
    }
}