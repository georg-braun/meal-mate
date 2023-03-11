using infrastructure.api.commands;
using infrastructure.api.queries;
using infrastructure.database;
using infrastructure.shopping_list;
using Microsoft.EntityFrameworkCore;

var usesSqliteDatabase = (WebApplication app) =>
{
    var scope = app.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetRequiredService<MealMateContext>();
    return ctx.Database.IsSqlite();
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MealMateHubToClients>();
builder.Services.AddSingleton<DomainEventInterceptor>();

builder.Services.AddSignalR();
builder.Services.AddDbContext<MealMateContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase"));
    //optionsBuilder.AddInterceptors(new DomainEventInterceptor());
});
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

app.MapPost($"/{CreateItemCommand.Route}", CreateItemCommand.Handler.Handle).WithTags("Category");
app.MapGet($"/{ItemsQuery.Route}", ItemsQuery.Handler.Handle).WithTags("Category");
app.MapPost($"/{DeleteItemCommand.Route}", DeleteItemCommand.Handler.Handle).WithTags("Category");

app.MapPost($"/{CreateCategoryCommand.Route}", CreateCategoryCommand.Handler.Handle)
    .WithTags("Category");
app.MapGet($"/{CategoriesQuery.Route}", CategoriesQuery.Handler.Handle).WithTags("Category");
app.MapGet($"/{CategoriesDetailsQuery.Route}", CategoriesDetailsQuery.Handler.Handle).WithTags("Category");

app.MapPost($"/{CreateShoppingListCommand.Route}", CreateShoppingListCommand.Handler.Handle).WithTags("ShoppingList");
app.MapGet($"/{ShoppingListsQuery.Route}", GetShoppingListsQueryHandler.Handle).WithTags("ShoppingList");
app.MapGet($"/{ShoppingListQuery.Route}", ShoppingListQueryHandler.Handle).WithTags("ShoppingList");
app.MapPost($"/{CreateEntryCommand.Route}", CreateEntryCommand.Handler.Handle).WithTags("ShoppingList");
app.MapPost($"/{CreateEntryWithFreeTextCommand.Route}", CreateEntryWithFreeTextCommand.Handler.Handle)
    .WithTags("ShoppingList");
app.MapPost($"/{DeleteEntryCommand.Route}", DeleteEntryCommand.Handler.Handle).WithTags("ShoppingList");


// Todo: The unit tests still use a sqlite database for tests. In this situation no migration should be executed. But it would be better to use a postgresql container for testing purposes.
if (!usesSqliteDatabase(app))
    app.MigrateDatabase();

app.MapHub<MealMateHub>("/shoppingListHub");

app.Run();


public partial class Program
{
} /* use for integration tests */