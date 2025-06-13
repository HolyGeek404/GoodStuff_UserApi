using Microsoft.EntityFrameworkCore;
using Model.DataAccess.Context;
using Model.DataAccess.DBSets;
using Model.DataAccess.Interfaces;

namespace Model.DataAccess;

public class UserDao(GoodStuffContext context) : IUserDao
{
    public async Task SignUp(Users user)
    {
        context.User.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<Users?> GetUserByEmail(string email)
    {
        return await context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
}