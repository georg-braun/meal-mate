using budget_backend_integration_tests.backend;
using FluentAssertions;

namespace api_integration_test;

public class CategorySpecs
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
    public async Task User_can_create_an_item_without_category()
    {
        // arrange
        var client = new ApiBackend().client;
        var item = await client.CreateItemAsync("Zwiebel");

        // act
        var items = await client.GetItemsAsync();
        
        // assert
        items.First().Id.Should().Be(item.Id);
        items.First().Name.Should().Be(item.Name);
    }

    [Fact]
    public async Task An_item_without_existing_entries_can_be_deleted()
    {
        // arrange  
        var client = new ApiBackend().client;
        var category = await client.CreateCategoryAsync("Gemüse");
        var item = await client.CreateItemAsync(category.Id, "Zwiebel");

        // act
        await client.DeleteItemAsync(item.Id);

        // assert
        var items = await client.GetItemsAsync();
        items.Should().HaveCount(0);
    }
}