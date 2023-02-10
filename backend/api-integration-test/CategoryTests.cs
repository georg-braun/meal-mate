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
        var result = await client.AddCategory("Gemüse");

        // assert
        result.Name.Should().Be("Gemüse");
    }

}