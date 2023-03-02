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
    public async Task User_can_create_a_category_and_item_and_get_this_data_back()
    {
        // arrange
        var client = new ApiBackend().client;
        var category = await client.CreateCategoryAsync("Gemüse");
        var item = await client.CreateItem(category.Id, "Zwiebel");
        
        // act
        var categoriesWithItems = await client.GetCategoriesWithItems();
        

        // assert
        categoriesWithItems.First().Id.Should().Be(category.Id);
        categoriesWithItems.First().Name.Should().Be(category.Name);
        categoriesWithItems.First().Items.Should().Contain(_ => _.Name.Equals("Zwiebel"));
    }

    [Fact]
    public async Task An_item_without_existing_entries_can_be_deleted()
    {
        // arrange  
        var client = new ApiBackend().client;
        var category = await client.CreateCategoryAsync("Gemüse");
        var item = await client.CreateItem(category.Id, "Zwiebel");

        // act
        await client.DeleteItemAsync(item.Id);

        // assert
        var items = await client.GetItemsAsync();
        items.Should().HaveCount(0);
    }
}