using Model.DataAccess.DBSets;

namespace Model.DataAccess.Interfaces;

public interface IUserDao
{
    Task SignUp(Users user);
    Task<Users?> GetUserByEmail(string email);
}