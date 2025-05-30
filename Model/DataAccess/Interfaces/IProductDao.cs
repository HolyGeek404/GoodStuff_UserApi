namespace Model.DataAccess.Interfaces;

public interface IProductDao
{
    Task<string> GetAllProductsByType(string type);
}
