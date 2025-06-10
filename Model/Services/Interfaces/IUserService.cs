using Model.DataAccess.DBSets;
using Model.Features.User.Commands;
using Model.Features.User.Commands.SignUp;

namespace Model.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUp(SignUpCommand model);
    Task<Users?> SignIn(string email, string password);
    Task<Users?> GetUserByEmail(string email);
}