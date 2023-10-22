using application.Commands.Template;
using Infrastructure.database;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.api.commands;
using WebApi.api.queries;
using WebApi.api.templates;

namespace WebApi.api;

public static class ApiExtensions
{
    public static void MapCommands(this WebApplication app)
    {
        app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommand.Handler.Handle).WithTags("Category");
        app.MapPost($"/{DeleteItemCommand.Route}", DeleteItemCommand.Handler.Handle).WithTags("Category");
        
        app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommand.Handler.Handle).WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryWithFreeTextCommand.Route}", CreateEntryWithFreeTextCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{DeleteEntryCommand.Route}", DeleteEntryCommand.Handler.Handle).WithTags("ShoppingList");
       
        app.MapPost($"/{ApplyTemplateCommand.Route}", ApplyTemplateCommand.Handler.Handle).WithTags("Template");
    }

    public static void MapQueries(this WebApplication app)
    {
      
        app.MapGet($"/{ShoppingListQuery.Route}", ShoppingListQueryHandler.Handle).WithTags("ShoppingList");
        app.MapGet($"/{AvailableTemplatesQuery.Route}", AvailableTemplatesQuery.Handler.Handle).WithTags("Template");
    }


    public const string TemplateRoute = "template";

    public static void MapTemplateEndpoints(this WebApplication app)
    {
        // get endpoint for templates
        app.MapGet($"/{TemplateRoute}", async (MealMateContext context) =>
        {
            var templates = await context.Templates.Include(_ => _.TemplateItems).ToListAsync();
            var templateDtos = templates.Select(TemplateDto.FromEntity);
            return templateDtos;
        }).WithTags("Template");

        // get endpoint for single template
        app.MapGet($"/{TemplateRoute}/{{id}}", new Func<MealMateContext, Guid, Task<IResult>>(async (context, id) =>
        {
            var template = await context.Templates
                .Include(_ => _.TemplateItems)
                .FirstOrDefaultAsync(_ => _.Id == id);

            if (template == null) return Results.NotFound();

            return Results.Ok(TemplateDto.FromEntity(template));
        })).WithTags("Template");

        // post endpoint for template
        app.MapPost($"/{TemplateRoute}", new Func<IMediator, TemplateDto, Task<IResult>>(
            async (mediator, templateDto) =>
            {
                var template = await mediator.Send(new CreateTemplateCommand
                {
                    Name = templateDto.Name,
                    Instructions = templateDto.Instructions,
                    Items = templateDto.Items.Select(_ => (_.Name, Amount: _.Qualifier)).ToList()
                });

                return Results.Ok(TemplateDto.FromEntity(template));
                
            })).WithTags("Template");
        // endpoint for updating template
        app.MapPut($"/{TemplateRoute}/{{id}}", new Func<IMediator, Guid, TemplateDto, Task<IResult>>(
            async (mediator, id, templateDto) =>
            {
                var template = await mediator.Send(new UpdateTemplateCommand()
                {
                    Id = id,
                    Name = templateDto.Name,
                    Instructions = templateDto.Instructions,
                    Items = templateDto.Items.Select(_ => (_.Id, _.Name, _.Qualifier)).ToList()
                });


                return Results.Ok();
            })).WithTags("Template");


        // endpoint for deletion of template
        app.MapDelete($"/{TemplateRoute}/{{id}}", new Func<IMediator, Guid, Task<IResult>>(async (mediator, id) =>
        {
            var deletionSuccessful = await mediator.Send(new DeleteTemplateCommand() {Id = id});

            return deletionSuccessful ? Results.NoContent() : Results.NotFound();
        })).WithTags("Template");
    }
}