using Microsoft.EntityFrameworkCore;
using Model.DataAccess.DBSets;

namespace Model.DataAccess.Context
{
    public partial class PGPContext : DbContext
    {
        public PGPContext(DbContextOptions<PGPContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

    }
}