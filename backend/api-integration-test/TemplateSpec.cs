using budget_backend_integration_tests.backend;
using FluentAssertions;
using WebApi.api.templates;

namespace api_integration_test;

public class TemplateSpec
{
    // write a test to check if the template is created correctly
    [Fact]
    public async Task User_can_create_a_template()
    {
        // arrange
        var client = new ApiBackend().client;
        
        var template = new TemplateDto()
        {
            Name = "Test Template",
            Instructions = "Test Instructions",
            Items = new List<TemplateItemDto>()
            {
                new()
                {
                    Name = "Zwiebel",
                    Amount = "1kg"
                }
            }
        };
        
        // act
        await client.PostTemplateAsync(template);
        
        // assert
        var templates = await client.GetTemplatesAsync();
        templates.First().Name.Should().Be("Test Template");
        templates.First().Items.Should().Contain(_ => _.Name.Equals("Zwiebel"));
    }
    
    // write a test to check if the template is updated correctly
    [Fact]
    public async Task User_can_update_a_template()
    {
        // arrange
        var client = new ApiBackend().client;
        var template = new TemplateDto()
        {
            Name = "Test Template",
            Instructions = "Test Instructions",
            Items = new List<TemplateItemDto>()
            {
                new()
                {
                    Name = "Zwiebel",
                    Amount = "1kg"
                }
            }
        };
        
        await client.PostTemplateAsync(template);
        
        var templates = await client.GetTemplatesAsync();
        var templateId = templates.First().Id;
        
        var updatedTemplate = new TemplateDto()
        {
            Id = templateId,
            Name = "Updated Template",
            Instructions = "Updated Instructions",
            Items = new List<TemplateItemDto>()
            {
                new()
                {
                    Name = "Tomate",
                    Amount = "1kg"
                }
            }
        };
        
        // act
        await client.UpdateTemplateAsync(updatedTemplate);
        
        // assert
        var updatedTemplates = await client.GetTemplatesAsync();
        updatedTemplates.First().Name.Should().Be("Updated Template");
        updatedTemplates.First().Items.Should().Contain(_ => _.Name.Equals("Tomate"));
    }

}