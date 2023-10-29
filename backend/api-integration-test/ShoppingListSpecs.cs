using api_integration_test.backend;
using budget_backend_integration_tests.backend;
using FluentAssertions;

namespace api_integration_test;

public class ShoppingListSpecs
{
    [Fact]
    public async Task User_enters_a_free_text_entry_with_quantity_and_a_entry_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act
        const string itemName = "Sugar";
        const string entryFreeText = $"{itemName}";
        await client.CreateEntryAsync(list.Id, entryFreeText);


        // assert
        var newList = await client.GetShoppingListAsync(list.Id);

        newList.Entries.First().ItemName.Should().Be(itemName);
    }

    [Fact]
    public async Task User_enters_a_free_text_entry_with_multiple_item_words_and_a_entry_is_created()
    {
        // arrange
        var client = new ApiBackend().client;
        var list = await client.CreateShoppingListAsync("MyList");

        // act
        const string itemName = "Hot chocolate";
        const string entryFreeText = $"{itemName}";
        await client.CreateEntryAsync(list.Id, entryFreeText);


        // assert
        var newList = await client.GetShoppingListAsync(list.Id);
        newList.Entries.First().ItemName.Should().Be(itemName);
    }
}