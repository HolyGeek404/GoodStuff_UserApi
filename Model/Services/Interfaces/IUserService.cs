using Model.DataAccess.DBSets;

namespace Model.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUp(User model);
    Task<User?> SignIn(string email, string password);
    Task<User?> GetUserByEmail(string email);
}