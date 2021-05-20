using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API.Data.WMSF.FG_REPORT_COMPARE
{
    public partial class DBContext_EEP : DbContext
    {
        public DbSet<FRI_PO> FRI_PO { get; set; }
        public DBContext_EEP(DbContextOptions<DBContext_EEP> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FRI_PO>(entity =>
            {
                entity.HasKey(e => new { e.PO, e.Factory_ID });
            });
        }
    }
}