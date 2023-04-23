using System.Text;
using WebApi.api.commands;
using WebApi.api.queries;
using domain;
using Newtonsoft.Json;
using WebApi.api;
using WebApi.api.templates;

namespace api_integration_test;

public static class ClientExtensions
{
    public static async Task<Category> CreateCategoryAsync(this HttpClient client, string name)
    {
        var command = new CreateCategoryCommand {Name = name};
        var response = await client.PostAsync(CreateCategoryCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Category>(responseJson);
    }


    public static async Task<CreateShoppingListCommand.Handler.CreateShoppingListCommandResponse>
        CreateShoppingListAsync(this HttpClient client, string name)
    {
        var command = new CreateShoppingListCommand {Name = name};
        var response = await client.PostAsync(CreateShoppingListCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateShoppingListCommand.Handler.CreateShoppingListCommandResponse>(
            responseJson);
    }

    public static async Task CreateEntryAsync(this HttpClient client, Guid listId, string freeText)
    {
        var command = new CreateEntryWithFreeTextCommand {ShoppingListId = listId, FreeText = freeText};
        var response = await client.PostAsync(CreateEntryWithFreeTextCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        //return JsonConvert.DeserializeObject<ShoppingListQueryEntryDto>(responseJson);
    }

    public static async Task<CreateItemCommand.Handler.CreateItemCommandResponse> CreateItemAsync(this HttpClient client, Guid categoryId,
        string name)
    {
        var command = new CreateItemCommand {CategoryId = categoryId, Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommand.Handler.CreateItemCommandResponse>(responseJson);
    }
    
    public static async Task<CreateItemCommand.Handler.CreateItemCommandResponse> CreateItemAsync(this HttpClient client,
        string name)
    {
        var command = new CreateItemCommand { Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommand.Handler.CreateItemCommandResponse>(responseJson);
    }

    public static async Task DeleteItemAsync(this HttpClient client, Guid itemId)
    {
        var command = new DeleteItemCommand {ItemId = itemId};
        var response = await client.PostAsync(DeleteItemCommand.Route, Serialize(command));
    }

    public static async Task<List<ItemsQuery.Handler.ItemsQueryResponse>> GetItemsAsync(this HttpClient client)
    {
        var command = new ItemsQuery();
        var response = await client.GetAsync(ItemsQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ItemsQuery.Handler.ItemsQueryResponse>>(responseJson);
    }

    public static async Task<ShoppingListQueryResponse> GetShoppingListAsync(this HttpClient client, Guid listId)
    {
        var response = await client.GetAsync($"{ShoppingListQuery.Route}?id={listId.ToString()}");

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ShoppingListQueryResponse>(responseJson);
    }


    public static async Task<List<CategoriesWithDetailQuery.CategoriesWithDetailResponse>> GetCategoriesWithItems(this HttpClient client)
    {
        var response = await client.GetAsync(CategoriesWithDetailQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<CategoriesWithDetailQuery.CategoriesWithDetailResponse>>(responseJson);
    }
    
    private static StringContent Serialize(object command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    
    public static async Task PostTemplateAsync(this HttpClient client, TemplateDto template)
    {
       
        var response = await client.PostAsync($"/{ApiExtensions.TemplateRoute}", Serialize(template));

        var responseJson = await response.Content.ReadAsStringAsync();
        //return JsonConvert.DeserializeObject<Category>(responseJson);
    }

    public static async Task<List<TemplateDto>> GetTemplatesAsync(this HttpClient client)
    {
        var response = await client.GetAsync($"/{ApiExtensions.TemplateRoute}");

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<TemplateDto>>(responseJson);
    }
    
    public static async Task UpdateTemplateAsync(this HttpClient client, TemplateDto template)
    {
        var response = await client.PutAsync($"/{ApiExtensions.TemplateRoute}/{template.Id}", Serialize(template));

        // var responseJson = await response.Content.ReadAsStringAsync();
        // return JsonConvert.DeserializeObject<List<TemplateDto>>(responseJson);
    }
    
    // delete a template
    public static async Task DeleteTemplateAsync(this HttpClient client, Guid templateId)
    {
        var response = await client.DeleteAsync($"/{ApiExtensions.TemplateRoute}/{templateId}");
    }
}