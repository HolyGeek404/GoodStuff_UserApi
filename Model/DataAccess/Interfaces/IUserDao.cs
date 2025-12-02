using Model.DataAccess.DBSets;

namespace Model.DataAccess.Interfaces;

public interface IUserDao
{
    Task SignUpAsync(Users user);
    Task<Users?> GetUserByEmailAsync(string email);
    Task<bool> ActivateUserAsync(string email, Guid providedKey);
}