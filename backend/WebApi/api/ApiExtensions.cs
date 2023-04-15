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

        app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommand.Handler.Handle)
            .WithTags("Category");
        app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommand.Handler.Handle).WithTags("ShoppingList");
        app.MapPost($"/{CreateEntryWithFreeTextCommand.Route}", CreateEntryWithFreeTextCommand.Handler.Handle)
            .WithTags("ShoppingList");
        app.MapPost($"/{DeleteEntryCommand.Route}", DeleteEntryCommand.Handler.Handle).WithTags("ShoppingList");
    }

    public static void MapQueries(this WebApplication app)
    {
        app.MapGet($"/{ItemsQuery.Route}", ItemsQuery.Handler.Handle).WithTags("Category");
        app.MapGet($"/{CategoriesQuery.Route}", CategoriesQuery.Handler.Handle).WithTags("Category");
        app.MapGet($"/{CategoriesWithDetailQuery.Route}", CategoriesWithDetailQuery.Handler.Handle)
            .WithTags("Category");
        app.MapGet($"/{ShoppingListQuery.Route}", ShoppingListQueryHandler.Handle).WithTags("ShoppingList");
    }


    public static void MapTemplateEndpoints(this WebApplication app)
    {
        // get endpoint for templates
        app.MapGet("/template", async (MealMateContext context) =>
        {
            var templates = await context.Templates.ToListAsync();
            var templateDtos = templates.Select(TemplateDto.FromEntity);
            return templateDtos;
        }).WithTags("Template");

        // get endpoint for single template
        app.MapGet("/template/{id}", new Func<MealMateContext, Guid, Task<IResult>>(async (context, id) =>
        {
            var template = await context.Templates.FindAsync(id);
            if (template == null) return Results.NotFound();

            return Results.Ok(TemplateDto.FromEntity(template));
        })).WithTags("Template");

        // post endpoint for template
        app.MapPost("/template", new Func<IMediator, TemplateDto, Task<IResult>>(
            async (mediator, templateDto) =>
            {
                var template = await mediator.Send(new CreateTemplateCommand
                {
                    Name = templateDto.Name,
                    Instructions = templateDto.Instructions,
                    Items = templateDto.Items
                });

                return Results.CreatedAtRoute("/template", new {id = template.Id}, template);
            })).WithTags("Template");
        
        // endpoint for updating template
        app.MapPut("/template/{id}", new Func<IMediator, Guid, TemplateDto, Task<IResult>>(async (mediator, id, templateDto) =>
        {
            var template = await mediator.Send(new UpdateTemplateCommand()
            {
                Id = id,
                Name = templateDto.Name,
                Instructions = templateDto.Instructions,
                Items = templateDto.Items
            });


            return Results.Ok();
        })).WithTags("Template");
        
        // endpoint for deletion of template
        app.MapDelete("/template/{id}", new Func<IMediator, Guid, Task<IResult>>(async (mediator, id) =>
        {
            var deletionSuccessful = await mediator.Send(new DeleteTemplateCommand() {Id = id});
            
            return deletionSuccessful ? Results.NoContent() : Results.NotFound();
        })).WithTags("Template");
    }
}