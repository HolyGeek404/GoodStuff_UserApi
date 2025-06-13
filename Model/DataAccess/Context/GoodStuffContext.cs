using Microsoft.EntityFrameworkCore;
using Model.DataAccess.DBSets;

namespace Model.DataAccess.Context;

public partial class GoodStuffContext(DbContextOptions<GoodStuffContext> options) : DbContext(options)
{
    public DbSet<Users> User { get; set; }

}