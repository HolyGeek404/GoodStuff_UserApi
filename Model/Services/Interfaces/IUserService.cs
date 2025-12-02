using Model.DataAccess.DBSets;
using Model.Features.User.Commands;
using Model.Features.User.Commands.SignUp;

namespace Model.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUpAsync(SignUpCommand model);
    Task<Users?> SignInAsync(string email, string password);
    Task<Users?> GetUserByEmailAsync(string email);
    Task<bool> ActivateUserAsync(string email, Guid providedKey);
}