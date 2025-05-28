using Microsoft.Azure.Cosmos;

namespace Model;

public class ProductDao(CosmosClient cosmosClient) : IProductDao
{
    public List<IProduct> GetAllProductsByType(string type)
    {
        var container = cosmosClient.GetContainer("GoodStuff", "Products");
        var query = new QueryDefinition("SELECT * FROM c WHERE c.Category = @category")
            .WithParameter("@category", type.ToUpper());

        var queryOptions = new QueryRequestOptions
        {
            PartitionKey = new PartitionKey(type)
        };

        using var iterator = container.GetItemQueryIterator<dynamic>(query, requestOptions: queryOptions);
        
        return new List<IProduct>();
    }
}