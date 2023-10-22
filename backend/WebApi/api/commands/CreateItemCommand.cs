using domain;
using MediatR;

namespace WebApi.api.commands;

public record CreateItemCommand
{
    public const string Route = nameof(CreateItemCommand);
    public string Name { get; init; } = null!;
    public Guid CategoryId { get; init; }

    public static class Handler
    {
        public static async Task<CreateItemCommandResponse> Handle(CreateItemCommand command, IMediator mediator)
        {
            var item = await mediator.Send(new application.Commands.CreateItemCommand
                {CategoryId = command.CategoryId, Name = command.Name});

            return ToDto(item);
        }

        private static CreateItemCommandResponse ToDto(Item item)
        {
            return new CreateItemCommandResponse
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public record CreateItemCommandResponse
        {
            public Guid Id { get; init; }
            public Guid CategoryId { get; init; }
            public string Name { get; init; } = null!;
        }
    }
}