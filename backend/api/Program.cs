using System.Reflection.Metadata.Ecma335;
using api.commands;
using api.database;
using api.shopping_list;
using Microsoft.EntityFrameworkCore;

var UsesSqliteDatabase = (WebApplication app) =>
{
    var scope = app.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetRequiredService<MealMateContext>();
    return ctx.Database.IsSqlite();
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddDbContext<MealMateContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => 
    options.AddDefaultPolicy(
        policy =>
        {
            // Todo: This have to be more restrictive!
            //policy.AllowAnyOrigin();
            policy.SetIsOriginAllowed(_ => true);
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
            policy.AllowCredentials();

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
app.MapPost($"/{DeleteItemCommand.Route}", DeleteItemCommandHandler.Handle).WithTags("Category");

app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommandHandler.Handle).WithTags("Category");
app.MapGet($"/{GetCategoriesQuery.Route}", GetCategoriesQueryHandler.Handle).WithTags("Category");
app.MapGet($"/{GetCategoriesDetailsQuery.Route}", GetCategoriesDetailsQueryHandler.Handle).WithTags("Category");

app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommandHandler.Handle).WithTags("ShoppingList");
app.MapGet($"/{GetShoppingListsQuery.Route}", GetShoppingListsQueryHandler.Handle).WithTags("ShoppingList");
app.MapGet($"/{ShoppingListQuery.Route}", ShoppingListQueryHandler.Handle).WithTags("ShoppingList");
app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommandHandler.Handle).WithTags("ShoppingList");
app.MapPost($"/{DeleteEntryCommand.Route}", DeleteEntryCommandHandler.Handle).WithTags("ShoppingList");


// Todo: The unit tests still use a sqlite database for tests. In this situation no migration should be executed. But it would be better to use a postgresql container for testing purposes.pp
if (!UsesSqliteDatabase(app))
    app.MigrateDatabase();

app.MapHub<ShoppingListHub>("/shoppingListHub");

app.Run();



public partial class Program
{
} /* use for integration tests */


    
