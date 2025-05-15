using Model.DataAccess.DBSets;

namespace Model.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUp(User model);
    Task<User?> GetUserByEmail(string email);
}