namespace Model.DataAccess.Interfaces;

public interface IProductDao
{
    Task<object> GetAllProductsByType(string type);
}
