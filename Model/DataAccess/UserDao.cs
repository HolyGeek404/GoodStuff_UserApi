using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DataAccess.Context;
using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;

namespace Model.DataAccess;

public class UserDao(GoodStuffContext context, ILogger<UserDao> logger) : IUserDao
{
    public async Task SignUp(Users user)
    {
        try
        {
            logger.LogInformation($"Adding user {user.Email} to database.");
            context.User.Add(user);
            await context.SaveChangesAsync();
            logger.LogInformation($"Added user {user.Email} to database.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Couldn't add user {user.Email} to database because: {ex.Message}.");
            throw;
        }
    }

    public async Task<Users?> GetUserByEmail(string email)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
}