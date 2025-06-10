using Model.DataAccess.DBSets;
using Model.Features.User.Commands;

namespace Model.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUp(SignUpCommand model);
    Task<User?> SignIn(string email, string password);
    Task<User?> GetUserByEmail(string email);
}