using Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.api;
using WebApi.hubs;


// if (args.Contains("--run-migration"))
// {
//     var optionsBuilder = new DbContextOptionsBuilder<MealMateContext>();
//     optionsBuilder.UseNpgsql(connectionString);
//
//     await using var dbContext = new MealMateContext(optionsBuilder.Options);
//     await dbContext.Database.MigrateAsync();
// }

var usesSqliteDatabase = (WebApplication app) =>
{
    var scope = app.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetRequiredService<MealMateContext>();
    return ctx.Database.IsSqlite();
};

var builder = WebApplication.CreateBuilder(args);

builder.AddSolutionDependencies();

builder.Services.AddSingleton<MealMateHubToClients>();
builder.Services.AddSingleton<DomainEventInterceptor>();

builder.Services.AddSignalR();

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

if (args.Contains("--run-migration"))
{
    Console.WriteLine("Run migration.");
    // Migrate the database. 
    // This is just used here because there is only one backend instance. If there will be multiple intances
    // the migration should be moved to the deployment process.
    var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<MealMateContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapGet("/", () => Results.Ok("Everything is fine"));
app.MapCommands();
app.MapQueries();

// Todo: The unit tests still use a sqlite database for tests. In this situation no migration should be executed. But it would be better to use a postgresql container for testing purposes.
if (!usesSqliteDatabase(app))
    app.MigrateDatabase();

app.MapHub<MealMateHub>("/shoppingListHub");

app.Run();


public partial class Program
{
} /* use for integration tests */