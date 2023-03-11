using budget_backend_integration_tests.backend;
using FluentAssertions;

namespace api_integration_test;

public class ShoppingListSpecs
{
    [Fact]
    public async Task User_can_create_a_category()
    {
        // arrange
        var client = new ApiBackend().client;

        // act
        var result = await client.CreateCategoryAsync("Gemüse");

        // assert
        result.Name.Should().Be("Gemüse");
    }

    [Fact]
    public async Task User_enters_add_a_free_text_of_one_word_and_a_item_is_corresponding_item_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act

        const string itemName = "Sugar";
        const string entryFreeText = $"{itemName}";
        await client.CreateEntryAsync(list.Id, entryFreeText);

        // assert
        var items = await client.GetItemsAsync();
        items.Should().Contain(_ => _.Name.Equals(itemName));
    }

    [Fact]
    public async Task
        User_enters_a_free_text_entry_with_quantity_and_a_item_is_corresponding_item_without_the_quantity_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act

        const string itemName = "Sugar";
        const string entryFreeText = $"{itemName} 300g";
        await client.CreateEntryAsync(list.Id, entryFreeText);


        // assert
        var items = await client.GetItemsAsync();
        items.Should().Contain(_ => _.Name.Equals(itemName));
    }

    [Fact]
    public async Task User_enters_a_free_text_entry_with_quantity_and_a_entry_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act
        const string itemName = "Sugar";
        const string itemQualifier = "300g";
        const string entryFreeText = $"{itemName} {itemQualifier}";
        await client.CreateEntryAsync(list.Id, entryFreeText);


        // assert
        var newList = await client.GetShoppingListAsync(list.Id);

        newList.Entries.First().ItemName.Should().Be(itemName);
        newList.Entries.First().Qualifier.Should().Be(itemQualifier);
    }

    [Fact]
    public async Task User_enters_a_free_text_entry_with_multiple_item_words_and_a_entry_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act
        const string itemName = "Hot chocolate";
        const string itemQualifier = "200ml";
        const string entryFreeText = $"{itemName} {itemQualifier}";
        await client.CreateEntryAsync(list.Id, entryFreeText);


        // assert
        var newList = await client.GetShoppingListAsync(list.Id);
        newList.Entries.First().ItemName.Should().Be(itemName);
        newList.Entries.First().Qualifier.Should().Be(itemQualifier);
    }
}