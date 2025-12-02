using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DataAccess.Context;
using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;
using Model.Services;

namespace Model.DataAccess;

public class UserDao(GoodStuffContext context, ILogger<UserDao> logger) : IUserDao
{
    public async Task SignUpAsync(Users user)
    {
        try
        {
            Logs.LogAddingUserUseremailToDatabase(logger, user.Email);
            context.User.Add(user);
            await context.SaveChangesAsync();
            Logs.LogAddedUserUseremailToDatabase(logger, user.Email);
        }
        catch (Exception ex)
        {
            Logs.LogCouldnTAddUserUseremailToDatabaseBecauseExmessage(logger, ex, user.Email, ex.Message);
            throw;
        }
    }

    public async Task<Users?> GetUserByEmailAsync(string email)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<bool> ActivateUserAsync(string email, Guid providedKey)
    {
        var user = await context.User.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return false;

        if (user.ActivationKey != providedKey)
            return false;

        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;
        user.ActivationKey = null;

        await context.SaveChangesAsync();
        return true;
    }
}