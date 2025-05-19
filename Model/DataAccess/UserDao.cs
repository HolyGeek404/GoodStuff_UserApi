using Microsoft.EntityFrameworkCore;
using Model.DataAccess.Context;
using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;

namespace Model.DataAccess;

public class UserDao(PgpContext context) : IUserDao
{
    public async Task SignUp(User user)
    {
        context.User.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
}