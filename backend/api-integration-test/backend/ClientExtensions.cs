using System.Text;
using api.commands;
using domain;
using Newtonsoft.Json;

namespace api_integration_test;

public static class ClientExtensions
{
    public static async Task<Category> AddCategory(this HttpClient client, string name)
    {
        var command = new CreateCategoryCommand(){Name = "Gemüse"};
        var response = await client.PostAsync(CreateCategoryCommand.Route, Serialize(command));

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Category>(responseJson);
    }
    
    private static StringContent Serialize(object command)
    {
        var json = JsonConvert.SerializeObject(command);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}