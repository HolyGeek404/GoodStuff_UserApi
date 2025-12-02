using GoodStuff.UserApi.Infrastructure.DataAccess.DBSets;

namespace GoodStuff.UserApi.Infrastructure.DataAccess.Interfaces;

public interface IUserDao
{
    Task SignUpAsync(Users user);
    Task<Users?> GetUserByEmailAsync(string email);
    Task<bool> ActivateUserAsync(string email, Guid providedKey);
}