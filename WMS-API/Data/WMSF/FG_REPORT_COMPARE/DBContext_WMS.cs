using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API.Data.WMSF.FG_REPORT_COMPARE
{
    public class DBContext_WMS : DbContext
    {
        public DBContext_WMS(DbContextOptions<DBContext_WMS> options) : base(options){}
        public DbSet<WMSF_FG_CompareReport> WMSF_FG_CompareReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WMSF_FG_CompareReport>(entity =>
            {
                entity.HasKey(e => new { e.Closing_Date, e.Cdr_No, e.Location_ID });
            });

        }
    }
}