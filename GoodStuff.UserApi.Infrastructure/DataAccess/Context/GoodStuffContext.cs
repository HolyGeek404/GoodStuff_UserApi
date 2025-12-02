using GoodStuff.UserApi.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace GoodStuff.UserApi.Infrastructure.DataAccess.Context;

public class GoodStuffContext(DbContextOptions<GoodStuffContext> options) : DbContext(options)
{
    public DbSet<Users> User { get; set; }
}