
using Microsoft.EntityFrameworkCore;
using WMS_API.Models.SHCQ;

namespace Machine_API.Data
{
    public class SHCQDataContext : DbContext
    {
        public SHCQDataContext(DbContextOptions<SHCQDataContext> options) : base(options) { }
        public DbSet<FRI_PO> Building { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FRI_PO>().HasKey(x => new { x.Factory_ID, x.PO });
        }
    }
}