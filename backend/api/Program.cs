using System.Reflection.Metadata.Ecma335;
using api.commands;
using api.database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MealMateContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=meal-mate.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            // policy.AllowAnyMethod();
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors();


app.MapGet("/", () => Results.Ok("Everything is fine"));

app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommandHandler.Handle).WithTags("Category");
app.MapGet($"/{GetItemsQuery.Route}", GetItemsQueryHandler.Handle).WithTags("Category");

app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommandHandler.Handle).WithTags("Category");
app.MapGet($"/{GetCategoriesQuery.Route}", GetCategoriesQueryHandler.Handle).WithTags("Category");
app.MapGet($"/{GetCategoriesDetailsQuery.Route}", GetCategoriesWithItemsQueryHandler.Handle).WithTags("Category");

app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommandHandler.Handle).WithTags("ShoppingList");
app.MapGet($"/{GetShoppingListsQuery.Route}", GetShoppingListsQueryHandler.Handle).WithTags("ShoppingList");
app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommandCommandHandler.Handle).WithTags("ShoppingList");






app.Run();


public partial class Program
{
} /* use for integration tests */