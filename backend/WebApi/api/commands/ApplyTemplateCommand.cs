using domain;
using MediatR;

namespace WebApi.api.commands;

public record ApplyTemplateCommand
{
    public const string Route = nameof(ApplyTemplateCommand);
    public Guid ListId { get; init; }
    public Guid TemplateId { get; init; }

    public static class Handler
    {
        public static async Task Handle(ApplyTemplateCommand command, IMediator mediator)
        {
            await mediator.Send(new application.Commands.ApplyTemplateCommand
                {TemplateId = command.TemplateId, ListId = command.ListId});

        }
        
    }
}