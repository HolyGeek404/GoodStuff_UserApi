using Microsoft.Azure.Cosmos;
using Model.DataAccess.Interfaces;
using Model.Services;
namespace Model.DataAccess;

public class ProductDao(CosmosClient cosmosClient) : IProductDao
{
    public async Task<string> GetAllProductsByType(string type)
    {
        var container = cosmosClient.GetContainer("GoodStuff", "Products");
        var query = new QueryDefinition("SELECT * FROM c WHERE c.Category = @category")
            .WithParameter("@category", type);

        var queryOptions = new QueryRequestOptions
        {
            PartitionKey = new PartitionKey(type)
        };

        using var iterator = container.GetItemQueryIterator<dynamic>(query, requestOptions: queryOptions);
        var response = await iterator.ReadNextAsync();
        return ProductService.SerlializeProducts(response);
    }
}