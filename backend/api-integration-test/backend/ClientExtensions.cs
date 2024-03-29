using System.Text;
using Newtonsoft.Json;
using WebApi.api;
using WebApi.api.commands;
using WebApi.api.queries;
using WebApi.api.templates;

namespace api_integration_test.backend;

public static class ClientExtensions
{


    public static async Task<CreateShoppingListCommand.Handler.CreateShoppingListCommandResponse>
        CreateShoppingListAsync(this HttpClient client, string name)
    {
        var command = new CreateShoppingListCommand {Name = name};
        var response = await client.PostAsync(CreateShoppingListCommand.Route, SerializeAsJson(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateShoppingListCommand.Handler.CreateShoppingListCommandResponse>(
            responseJson);
    }

    public static async Task CreateEntryAsync(this HttpClient client, Guid listId, string freeText)
    {
        var command = new CreateEntryWithFreeTextCommand {ShoppingListId = listId, FreeText = freeText};
        var response = await client.PostAsync(CreateEntryWithFreeTextCommand.Route, SerializeAsJson(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        //return JsonConvert.DeserializeObject<ShoppingListQueryEntryDto>(responseJson);
    }

    public static async Task<CreateItemCommand.Handler.CreateItemCommandResponse> CreateItemAsync(this HttpClient client, Guid categoryId,
        string name)
    {
        var command = new CreateItemCommand {CategoryId = categoryId, Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, SerializeAsJson(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommand.Handler.CreateItemCommandResponse>(responseJson);
    }
    
    public static async Task<CreateItemCommand.Handler.CreateItemCommandResponse> CreateItemAsync(this HttpClient client,
        string name)
    {
        var command = new CreateItemCommand { Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, SerializeAsJson(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommand.Handler.CreateItemCommandResponse>(responseJson);
    }

    public static async Task DeleteItemAsync(this HttpClient client, Guid itemId)
    {
        var command = new DeleteItemCommand {ItemId = itemId};
        var response = await client.PostAsync(DeleteItemCommand.Route, SerializeAsJson(command));
    }
    
    public static async Task<ShoppingListQueryResponse> GetShoppingListAsync(this HttpClient client, Guid listId)
    {
        var response = await client.GetAsync($"{ShoppingListQuery.Route}?id={listId.ToString()}");

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ShoppingListQueryResponse>(responseJson);
    }
    
    
    private static StringContent SerializeAsJson(object command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    
    public static async Task<TemplateDto> PostTemplateAsync(this HttpClient client, TemplateDto template)
    {
       
        var response = await client.PostAsync($"/{ApiExtensions.TemplateRoute}", SerializeAsJson(template));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TemplateDto>(responseJson);
    }
    
    public static async Task PutTemplateAsync(this HttpClient client, TemplateDto template)
    {
       
        var response = await client.PutAsync($"/{ApiExtensions.TemplateRoute}/{template.Id}", SerializeAsJson(template));

        var responseJson = await response.Content.ReadAsStringAsync();
    }

    public static async Task<List<TemplateDto>> GetTemplatesAsync(this HttpClient client)
    {
        var response = await client.GetAsync($"/{ApiExtensions.TemplateRoute}");

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<TemplateDto>>(responseJson);
    }
    
    public static async Task UpdateTemplateAsync(this HttpClient client, TemplateDto template)
    {
        var response = await client.PutAsync($"/{ApiExtensions.TemplateRoute}/{template.Id}", SerializeAsJson(template));

        // var responseJson = await response.Content.ReadAsStringAsync();
        // return JsonConvert.DeserializeObject<List<TemplateDto>>(responseJson);
    }
    
    // delete a template
    public static async Task DeleteTemplateAsync(this HttpClient client, Guid templateId)
    {
        var response = await client.DeleteAsync($"/{ApiExtensions.TemplateRoute}/{templateId}");
    }
    
    // apply a template
    public static async Task<TemplateDto> ApplyTemplateAsync(this HttpClient client, Guid listId, Guid templateId)
    {
        var command = new ApplyTemplateCommand()
        {
            TemplateId = templateId,
            ListId = listId
        };
        var response = await client.PostAsync($"/{ApplyTemplateCommand.Route}", SerializeAsJson(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TemplateDto>(responseJson);
    }
}