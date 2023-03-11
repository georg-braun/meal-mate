using System.Text;
using infrastructure.api.commands;
using infrastructure.api.queries;
using domain;
using Newtonsoft.Json;

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


    public static async Task<CreateShoppingListCommand.Handler.Response>
        CreateShoppingListAsync(this HttpClient client, string name)
    {
        var command = new CreateShoppingListCommand {Name = name};
        var response = await client.PostAsync(CreateShoppingListCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateShoppingListCommand.Handler.Response>(
            responseJson);
    }

    public static async Task CreateEntryAsync(this HttpClient client, Guid listId, string freeText)
    {
        var command = new CreateEntryWithFreeTextCommand {ShoppingListId = listId, FreeText = freeText};
        var response = await client.PostAsync(CreateEntryWithFreeTextCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        //return JsonConvert.DeserializeObject<ShoppingListQueryEntryDto>(responseJson);
    }

    public static async Task<CreateItemCommand.Handler.Response> CreateItem(this HttpClient client, Guid categoryId,
        string name)
    {
        var command = new CreateItemCommand {CategoryId = categoryId, Name = name};
        var response = await client.PostAsync(CreateItemCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<CreateItemCommand.Handler.Response>(responseJson);
    }

    public static async Task DeleteItemAsync(this HttpClient client, Guid itemId)
    {
        var command = new DeleteItemCommand {ItemId = itemId};
        var response = await client.PostAsync(DeleteItemCommand.Route, Serialize(command));
    }

    public static async Task<List<ItemsQuery.Handler.Response>> GetItemsAsync(this HttpClient client)
    {
        var command = new ItemsQuery();
        var response = await client.GetAsync(ItemsQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ItemsQuery.Handler.Response>>(responseJson);
    }

    public static async Task<ShoppingListQueryResponse> GetShoppingListAsync(this HttpClient client, Guid listId)
    {
        var response = await client.GetAsync($"{ShoppingListQuery.Route}?id={listId.ToString()}");

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ShoppingListQueryResponse>(responseJson);
    }


    public static async Task<List<CategoriesDetailsQuery.Response>> GetCategoriesWithItems(this HttpClient client)
    {
        var response = await client.GetAsync(CategoriesDetailsQuery.Route);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<CategoriesDetailsQuery.Response>>(responseJson);
    }


    private static StringContent Serialize(object command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}