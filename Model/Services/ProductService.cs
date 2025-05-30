using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

namespace Model.Services;

public static class ProductService
{
    public static string SerlializeProducts(FeedResponse<dynamic> response)
    {
        if (response is null)
            return "";

        var result = new List<dynamic>();
        result.AddRange(response);
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }   
}