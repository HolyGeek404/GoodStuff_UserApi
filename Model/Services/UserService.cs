using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;
using Model.Services.Interfaces;

namespace Model.Services;

public class UserService(IUserDao userDao, IPasswordService passwordService) : IUserService
{
    public async Task<bool> SignUp(User model)
    {
        model.CreatedAt = DateTime.UtcNow;

        var existingUser = await userDao.GetUserByEmail(model.Email);

        if (existingUser != null)
        {
            return false; // User already exists
        }

        // Hash the password before saving
        model.Password = passwordService.HashPassword(model.Password);

        await userDao.SignUp(model);
        return true;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await userDao.GetUserByEmail(email);
    }
}