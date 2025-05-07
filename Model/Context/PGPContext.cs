using Microsoft.EntityFrameworkCore;

namespace Model.Context
{
    public partial class PGPContext : DbContext
    {
        public PGPContext(DbContextOptions<PGPContext> options) : base(options)
        {

        }
    }
}
