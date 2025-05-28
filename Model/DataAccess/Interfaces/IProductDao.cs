namespace Model;

public interface IProductDao
{
    List<IProduct> GetAllProductsByType(string type);
}
