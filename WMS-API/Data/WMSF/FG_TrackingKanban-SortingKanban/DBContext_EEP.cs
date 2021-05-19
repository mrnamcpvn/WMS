using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.EEP;

namespace WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban
{
    public partial class DBContext_EEP : DbContext
    {
        public DBContext_EEP(DbContextOptions<DBContext_EEP> options) : base(options) { } //Add this

        public virtual DbSet<FRI_PO> FRI_PO { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CS_AS");

            modelBuilder.Entity<FRI_PO>(entity =>
            {
                entity.HasKey(e => new { e.PO, e.Factory_ID });

                entity.HasComment("成品訂單檔");

                entity.HasIndex(e => new { e.Ship_Complete, e.Customer_No, e.Flag })
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => new { e.Customer_No, e.Plan_Ship_Date })
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => e.IC_Number)
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => e.Model_ID)
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => new { e.Plan_Ship_Date, e.Factory_ID })
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => e.Production_Line)
                    .HasFillFactor((byte)80);

                entity.HasIndex(e => e.Ship_Complete)
                    .HasFillFactor((byte)80);

                entity.Property(e => e.PO)
                    .IsUnicode(false)
                    .HasComment("訂單號碼");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別");

                entity.Property(e => e.Actual_Qty).HasComment("實際數量");

                entity.Property(e => e.Article)
                    .IsUnicode(false)
                    .HasComment("款號");

                entity.Property(e => e.BA_Complete)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')")
                    .HasComment("BA檢驗完成否");

                entity.Property(e => e.BA_Complete_Date).HasComment("BA檢驗完成日");

                entity.Property(e => e.Biz_Time).HasComment("轉入時間");

                entity.Property(e => e.Category)
                    .IsUnicode(false)
                    .HasComment("分類");

                entity.Property(e => e.Comfirmed_Date).HasComment("生管承諾日期");

                entity.Property(e => e.Complete)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')")
                    .HasComment("檢驗完成否");

                entity.Property(e => e.Complete_Date).HasComment("檢驗完成日期");

                entity.Property(e => e.Ctntyp).HasComment("裝箱方式");

                entity.Property(e => e.Customer)
                    .IsUnicode(false)
                    .HasComment("客戶名稱");

                entity.Property(e => e.Customer_Id)
                    .IsUnicode(false)
                    .HasComment("客戶代號");

                entity.Property(e => e.Customer_No)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("下單客戶");

                entity.Property(e => e.Flag)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("訂單狀態");

                entity.Property(e => e.IC_Number)
                    .IsUnicode(false)
                    .HasComment("IC編號");

                entity.Property(e => e.In_Qty).HasComment("入庫總數量");

                entity.Property(e => e.Kind)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Location_Bin)
                    .IsUnicode(false)
                    .HasComment("儲位");

                entity.Property(e => e.Model_ID)
                    .IsUnicode(false)
                    .HasComment("型體代號");

                entity.Property(e => e.Model_Name)
                    .IsUnicode(false)
                    .HasComment("型體名稱");

                entity.Property(e => e.Nation)
                    .IsUnicode(false)
                    .HasComment("出貨客戶國家別");

                entity.Property(e => e.Order_Qty).HasComment("訂單數量");

                entity.Property(e => e.Plan_Finish_Date).HasComment("預計生產完成日");

                entity.Property(e => e.Plan_Ship_Date).HasComment("預計出貨日");

                entity.Property(e => e.Plan_Start_ASY).HasComment("成型日");

                entity.Property(e => e.Pono)
                    .IsUnicode(false)
                    .HasComment("客戶訂單號碼");

                entity.Property(e => e.Production_Line).HasComment("生產線別");

                entity.Property(e => e.QA_Defect_Qty_First).HasDefaultValueSql("((0))");

                entity.Property(e => e.QA_Inspected_Qty).HasDefaultValueSql("((0))");

                entity.Property(e => e.QA_Not_Passed_Qty).HasDefaultValueSql("((0))");

                entity.Property(e => e.QA_Passed_Qty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Real_Finish_Date).HasComment("實際生產完成日");

                entity.Property(e => e.Real_Ship_Date).HasComment("實際要出貨日");

                entity.Property(e => e.Scolor)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Service_Seq)
                    .IsUnicode(false)
                    .HasComment("Service ID代碼");

                entity.Property(e => e.Ship_Complete)
                    .IsUnicode(false)
                    .HasComment("確認出貨");

                entity.Property(e => e.Shipping_Way).HasComment("出貨方式");

                entity.Property(e => e.Updated_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Updated_Time).HasComment("異動時間");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}