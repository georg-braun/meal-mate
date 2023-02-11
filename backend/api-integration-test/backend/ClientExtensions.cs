using System.Text;
using api.commands;
using domain;
using Newtonsoft.Json;

namespace api_integration_test;

public static class ClientExtensions
{
    public static async Task<Category> CreateCategory(this HttpClient client, string name)
    {
        var command = new CreateCategoryCommand(){Name = name};
        var response = await client.PostAsync(CreateCategoryCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Category>(responseJson);
    }
    
    public static async Task<CreateItemCommandHandler.CreateItemDto> CreateItem(this HttpClient client, Guid categoryId, string name)
    {
        var command = new CreateItemCommand(){CategoryId = categoryId, Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommandHandler.CreateItemDto>(responseJson);
    }
    
    public static async Task DeleteItemAsync(this HttpClient client, Guid itemId)
    {
        var command = new DeleteItemCommand(){ItemId = itemId};
        var response = await client.PostAsync(DeleteItemCommand.Route, Serialize(command));
    }
    
    public static async Task<List<GetItemsQueryHandler.GetItemsDto>> GetItemsAsync(this HttpClient client)
    {
        var command = new GetItemsQuery();
        var response = await client.GetAsync(GetItemsQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<GetItemsQueryHandler.GetItemsDto>>(responseJson);
    }
    
    public static async Task<List<GetCategoriesWithDetailsDto>> GetCategoriesWithItems(this HttpClient client)
    {
        var command = new GetCategoriesDetailsQuery();
        var response = await client.GetAsync(GetCategoriesDetailsQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<GetCategoriesWithDetailsDto>>(responseJson);
    }

    
    private static StringContent Serialize(object command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}