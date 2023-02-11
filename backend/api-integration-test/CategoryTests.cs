using budget_backend_integration_tests.backend;
using FluentAssertions;

namespace api_integration_test;

public class UnitTest1
{
    [Fact]
    public async Task CreateCategory()
    {
        // arrange
        var client = new ApiBackend().client;
        
        // act
        var result = await client.CreateCategory("Gemüse");

        // assert
        result.Name.Should().Be("Gemüse");
    }

    [Fact]
    public async Task GetCategoriesWithItemsQuery_WithCorrectData_ShouldReturnTheDesiredDto()
    {
        // arrange
        var client = new ApiBackend().client;
        var category = await client.CreateCategory("Gemüse");
        var item = await client.CreateItem(category.Id, "Zwiebel");
        
        // act
        var categoriesWithItems = await client.GetCategoriesWithItems();
        

        // assert
        categoriesWithItems.First().Id.Should().Be(category.Id);
        categoriesWithItems.First().Name.Should().Be(category.Name);
        categoriesWithItems.First().Items.Should().Contain(_ => _.Name.Equals("Zwiebel"));
        


    }

}