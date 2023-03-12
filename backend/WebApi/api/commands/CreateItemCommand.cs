using domain;
using MediatR;

namespace WebApi.api.commands;

public record CreateItemCommand
{
    public const string Route = nameof(CreateItemCommand);
    public string Name { get; init; }
    public Guid CategoryId { get; init; }

    public static class Handler
    {
        public static async Task<Response> Handle(CreateItemCommand command, IMediator mediator)
        {
            var item = await mediator.Send(new application.Commands.CreateItemCommand
                {CategoryId = command.CategoryId, Name = command.Name});

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