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
                    Id = Guid.Empty,
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
                    Id = Guid.Empty,
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
                    Id = Guid.Empty,
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
    
    // write a test to check if the user can remove a template item
    [Fact]
    public async Task User_can_remove_a_template_item()
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
                    Id = Guid.Empty,
                    Name = "Zwiebel",
                    Amount = "1kg"
                },
                new()
                {
                    Id = Guid.Empty,
                    Name = "Kartoffeln",
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
                    Id = Guid.Empty,
                    Name = "Kartoffeln",
                    Amount = "1kg"
                }
            }
        };
        
        await client.PutTemplateAsync(updatedTemplate);
        template = (await client.GetTemplatesAsync()).First();
        
        // Assert
        template.Items.Should().HaveCount(1);
    }
    
    // write a test to create a template with an empty instruction
    [Fact]
    public async Task User_can_create_a_template_with_an_empty_instruction()
    {
        // arrange
        var client = new ApiBackend().client;
        var template = new TemplateDto()
        {
            Name = "Test Template",
            Instructions = null,
            Items = new List<TemplateItemDto>()
        };
        
        // act
        await client.PostTemplateAsync(template);
        
        // assert
        var templates = await client.GetTemplatesAsync();
        templates.First().Instructions.Should().BeEmpty();
    }

    // write a test to check if the template is deleted correctly
    [Fact]
    public async Task User_can_delete_a_template()
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
                    Id = Guid.Empty,
                    Name = "Zwiebel",
                    Amount = "1kg"
                }
            }
        };
        
        await client.PostTemplateAsync(template);
        
        var templates = await client.GetTemplatesAsync();
        var templateId = templates.First().Id;
        
        // act
        await client.DeleteTemplateAsync(templateId);
        
        // assert
        var updatedTemplates = await client.GetTemplatesAsync();
        updatedTemplates.Should().BeEmpty();
    }
    
    // write a unit test that applies a template
    [Fact]
    public async Task User_can_apply_a_template()
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
                    Id = Guid.Empty,
                    Name = "Zwiebel",
                    Amount = "1kg"
                }
            }
        };
        
        await client.PostTemplateAsync(template);
        var listToApply = await client.CreateShoppingListAsync("MyList");
        
        // act 
        client.ApplyTemplateAsync(listToApply.Id, template.Id);
        
        // assert
        var list = await client.GetShoppingListAsync(listToApply.Id);
        list.Entries.Should().Contain(_ => _.ItemName.Equals("Zwiebel"));
    }

}