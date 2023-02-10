using System.Reflection.Metadata.Ecma335;
using api.commands;
using api.database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MealMateContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/", () => Results.Ok("Everything is fine"));


app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommandHandler.Handle);
app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommandHandler.Handle);
app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommandHandler.Handle);
app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommandCommandHandler.Handle);

app.MapGet($"/{GetItemsQuery.Route}", GetItemsQueryHandler.Handle);
app.MapGet($"/{GetShoppingListsQuery.Route}", GetShoppingListsQueryHandler.Handle);
app.MapGet($"/{GetCategoriesQuery.Route}", GetCategoriesQueryHandler.Handle);

app.Run();

