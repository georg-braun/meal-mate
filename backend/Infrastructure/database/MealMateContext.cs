using domain;
using domain.shopping_list;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.database;

public class MealMateContext : DbContext
{
    private readonly DomainEventInterceptor _domainEventInterceptor;

    public MealMateContext(DbContextOptions<MealMateContext> contextOptions,
        DomainEventInterceptor domainEventInterceptor) : base(contextOptions)
    {
        _domainEventInterceptor = domainEventInterceptor;
    }

    public DbSet<Item> Items { get; init; } = null!;
    public DbSet<ShoppingList> ShoppingLists { get; init; } = null!;
    public DbSet<Entry> Entries { get; init; } = null!;
    public DbSet<Template> Templates { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_domainEventInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PostgreSQL can't handle the GUIDs. Therefore we have to add a conversion
        var guidToStringConverter = new GuidToStringConverter();
        modelBuilder.Entity<Item>().Property(_ => _.Id).HasConversion(guidToStringConverter);
        modelBuilder.Entity<ShoppingList>().Property(_ => _.Id).HasConversion(guidToStringConverter);
        modelBuilder.Entity<Entry>().Property(_ => _.Id).HasConversion(guidToStringConverter);
        modelBuilder.Entity<Template>().Property(_ => _.Id).HasConversion(guidToStringConverter);
        modelBuilder.Entity<TemplateItem>().Property(_ => _.Id).HasConversion(guidToStringConverter);

        // the entity id is generated by the domain code
        modelBuilder.Entity<Entry>().Property(_ => _.Id).ValueGeneratedNever();
        modelBuilder.Entity<Item>().Property(_ => _.Id).ValueGeneratedNever();
        modelBuilder.Entity<Template>().Property(_ => _.Id).ValueGeneratedNever();
        modelBuilder.Entity<TemplateItem>().Property(_ => _.Id).ValueGeneratedNever();
        
        // Relationships
        modelBuilder.Entity<Template>().HasMany(_ => _.TemplateItems).WithMany();
    }

    public async Task<Item> CreateItem(string itemName, Guid categoryId)
    {
        var item = Item.Create(itemName);
        
        await Items.AddAsync(item);
        await SaveChangesAsync();

        return item;
    }

    public async Task DeleteItemAsync(Guid itemId)
    {
        var item = await Items.FindAsync(itemId);

        if (item is null) return;

        var entryWithReferenceToItemExists = await Entries.AnyAsync(_ => _.Item.Id.Equals(itemId));

        if (entryWithReferenceToItemExists) return;

        // Todo: remove the item from objects?
        Remove(item);
        await SaveChangesAsync();
    }
    

    public async Task DeleteEntryAsync(Guid shoppingListId, Guid entryId)
    {
        var shoppingList = await ShoppingLists.Include(_ => _.Entries)
            .FirstOrDefaultAsync(_ => _.Id.Equals(shoppingListId));

        if (shoppingList is null) return;

        shoppingList.RemoveEntry(entryId);
        await SaveChangesAsync();
    }

    public async Task<bool> ShoppingListExistsAsync(Guid shoppingListId)
    {
        var list = await ShoppingLists.FindAsync(shoppingListId);
        return list != null;
    }

    public async Task<Item> CreateItemIfDoesntExistAsync(string itemName)
    {
        var item = await Items.FirstOrDefaultAsync(_ => _.Name.Equals(itemName));

        if (item is not null)
            return item;

        var newItem = Item.Create(itemName);
        await Items.AddAsync(newItem);
        return newItem;
    }
}