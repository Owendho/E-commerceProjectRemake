using Microsoft.EntityFrameworkCore;
using NovicellCaseRemake.Entities;

namespace NovicellCaseRemake
{
    public class NovicellAppDBContext : DbContext
    {
        public NovicellAppDBContext(DbContextOptions<NovicellAppDBContext> options) : base(options) 
        {

        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
