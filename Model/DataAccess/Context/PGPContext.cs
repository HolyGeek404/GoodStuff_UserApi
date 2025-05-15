using Microsoft.EntityFrameworkCore;
using Model.DataAccess.DBSets;

namespace Model.DataAccess.Context;

public partial class PgpContext(DbContextOptions<PgpContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }

}