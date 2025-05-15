using Model.DataAccess.DBSets;

namespace Model.DataAccess.Interfaces;

public interface IUserDao
{
    Task SignUp(User user);
    Task<User?> GetUserByEmail(string email);
}