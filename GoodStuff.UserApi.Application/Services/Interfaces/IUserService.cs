using GoodStuff.UserApi.Application.Features.User.Commands.SignUp;
using GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;

namespace GoodStuff.UserApi.Application.Services.Interfaces;

public interface IUserService
{
    Task<bool> SignUpAsync(SignUpCommand model);
    Task<Users?> SignInAsync(string email, string password);
    Task<Users?> GetUserByEmailAsync(string email);
    Task<bool> ActivateUserAsync(string email, Guid providedKey);
}