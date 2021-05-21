using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API.Data.WMSF.FG_REPORT_COMPARE
{
    public partial class DBContext_WMS : DbContext
    {
        public DBContext_WMS(DbContextOptions<DBContext_WMS> options) : base(options) { }

        public virtual DbSet<WMSF_FGIN_ReportCompare> WMSF_FGIN_ReportCompare { get; set; }
        public virtual DbSet<WMSF_FG_CompareReport> WMSF_FG_CompareReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CS_AS");

            modelBuilder.Entity<WMSF_FGIN_ReportCompare>(entity =>
            {
                entity.HasKey(e => new { e.Report_Date, e.Cdr_No });

                entity.HasComment("成品倉庫存比較資料檔");

                entity.Property(e => e.Report_Date).HasComment("報表日期");

                entity.Property(e => e.Cdr_No)
                    .IsUnicode(false)
                    .HasComment("訂單號碼");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別");

                entity.Property(e => e.Order_Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true)
                    .HasComment("訂單狀態");

                entity.Property(e => e.PO_ERP_Qty).HasComment("ERP庫存數");

                entity.Property(e => e.PO_Locat_Qty).HasComment("儲位庫存數");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動日");
            });

            modelBuilder.Entity<WMSF_FG_CompareReport>(entity =>
            {
                entity.HasKey(e => new { e.Closing_Date, e.Cdr_No, e.Location_ID });

                entity.HasComment("成品倉庫存比較資料檔");

                entity.Property(e => e.Closing_Date).HasComment("日結時間");

                entity.Property(e => e.Cdr_No)
                    .IsUnicode(false)
                    .HasComment("訂單號碼");

                entity.Property(e => e.Location_ID).IsUnicode(false);

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別");

                entity.Property(e => e.Order_Status)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true)
                    .HasComment("訂單狀態");

                entity.Property(e => e.PO_ERP_Qty).HasComment("ERP庫存數");

                entity.Property(e => e.PO_WMS_Qty).HasComment("儲位庫存數");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動日");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}