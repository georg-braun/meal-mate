using System.Reflection.Metadata.Ecma335;
using api.commands;
using api.database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MealMateContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=meal-mate.db"));
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

app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommandHandler.Handle);
app.MapGet($"/{GetItemsQuery.Route}", GetItemsQueryHandler.Handle);

app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommandHandler.Handle);
app.MapGet($"/{GetCategoriesQuery.Route}", GetCategoriesQueryHandler.Handle);
app.MapGet($"/{GetCategoriesDetailsQuery.Route}", GetCategoriesWithItemsQueryHandler.Handle);

app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommandHandler.Handle);
app.MapGet($"/{GetShoppingListsQuery.Route}", GetShoppingListsQueryHandler.Handle);

app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommandCommandHandler.Handle);






app.Run();


public partial class Program
{
} /* use for integration tests */