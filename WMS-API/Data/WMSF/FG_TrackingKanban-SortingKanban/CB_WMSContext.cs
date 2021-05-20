using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban;

namespace WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban
{
    public partial class CB_WMSContext : DbContext
    {
        public CB_WMSContext(DbContextOptions<CB_WMSContext> options) : base(options) { }
        public virtual DbSet<VW_WMS_Department> VW_WMS_Department { get; set; }
        public virtual DbSet<WMSF_Carton_Locat> WMSF_Carton_Locat { get; set; }
        public virtual DbSet<WMSF_FGIN_Locat> WMSF_FGIN_Locat { get; set; }
        public virtual DbSet<WMSF_FGIN_PersonInCharge> WMSF_FGIN_PersonInCharge { get; set; }
        public virtual DbSet<WMS_Location> WMS_Location { get; set; }
        public virtual DbSet<VW_FGIN_LOCAT_LIST> VW_FGIN_LOCAT_LIST { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CS_AS");
            modelBuilder.Entity<VW_WMS_Department>().HasNoKey();
            modelBuilder.Entity<VW_WMS_Department>(entity =>
            {
                entity.ToView("VW_WMS_Department");

                entity.Property(e => e.Dept_Desc)
                    .IsUnicode(false)
                    .UseCollation("Chinese_Taiwan_Stroke_CI_AS");

                entity.Property(e => e.Dept_ID)
                    .IsUnicode(false)
                    .UseCollation("Chinese_Taiwan_Stroke_CI_AS");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .UseCollation("Chinese_Taiwan_Stroke_CI_AS");

                entity.Property(e => e.Line_Desc)
                    .IsUnicode(false)
                    .UseCollation("Chinese_Taiwan_Stroke_CI_AS");

                entity.Property(e => e.Work_Center)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .UseCollation("Chinese_Taiwan_Stroke_CI_AS");
            });

            modelBuilder.Entity<WMSF_Carton_Locat>(entity =>
            {
                entity.HasKey(e => new { e.Barcode, e.Order_ID, e.Factory_ID })
                    .HasName("PK_WMS_I67_D");

                entity.HasComment("外箱儲位檔");

                entity.Property(e => e.Barcode)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("條碼");

                entity.Property(e => e.Order_ID)
                    .IsUnicode(false)
                    .HasComment("訂單編號");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別");

                entity.Property(e => e.Carton_Pairs).HasComment("本箱雙數");

                entity.Property(e => e.Create_By)
                    .IsUnicode(false)
                    .HasComment("informix建立者");

                entity.Property(e => e.Create_Time).HasComment("informix建立時間");

                entity.Property(e => e.Location_ID)
                    .IsUnicode(false)
                    .HasComment("儲位編號");

                entity.Property(e => e.Status_Type)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Y')")
                    .IsFixedLength(true)
                    .HasComment("資料狀態");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動時間");

                entity.Property(e => e.Volume).HasComment("材料體積");

                entity.Property(e => e.Warehouse_ID)
                    .IsUnicode(false)
                    .HasComment("庫別");
            });

            modelBuilder.Entity<WMSF_FGIN_Locat>(entity =>
            {
                entity.Property(e => e.Up_Trno)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("轉交單號");

                entity.Property(e => e.CTN_Qty).HasComment("箱數");

                entity.Property(e => e.Cdr_No)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("訂單號碼");

                entity.Property(e => e.Dept_ID)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("生產部門");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別代號");

                entity.Property(e => e.In_UTC_Dat).HasComment("理貨完成時間");

                entity.Property(e => e.Locat)
                    .IsUnicode(false)
                    .HasComment("儲位資料");

                entity.Property(e => e.Meas).HasComment("材積");

                entity.Property(e => e.Qty).HasComment("雙數");

                entity.Property(e => e.Up_UTC_Dat).HasComment("收貨時間");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動時間");
            });

            modelBuilder.Entity<WMSF_FGIN_PersonInCharge>(entity =>
            {
                entity.HasKey(e => new { e.Dept_ID, e.Factory_ID });

                entity.Property(e => e.Dept_ID)
                    .IsUnicode(false)
                    .IsFixedLength(true)
                    .HasComment("部門代號");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別代號");

                entity.Property(e => e.Emp_ID)
                    .IsUnicode(false)
                    .HasComment("理貨人員工號");

                entity.Property(e => e.Emp_Name).HasComment("理貨人員名稱");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動時間");
            });

            modelBuilder.Entity<WMS_Location>(entity =>
            {
                entity.HasKey(e => new { e.Location_ID, e.Warehouse_ID, e.Factory_ID })
                    .HasName("PK_WMS_I108");

                entity.HasComment("儲位主檔");

                entity.Property(e => e.Location_ID)
                    .IsUnicode(false)
                    .HasComment("儲位代碼");

                entity.Property(e => e.Warehouse_ID)
                    .IsUnicode(false)
                    .HasComment("庫別");

                entity.Property(e => e.Factory_ID)
                    .IsUnicode(false)
                    .HasComment("廠別");

                entity.Property(e => e.Area_ID)
                    .IsUnicode(false)
                    .HasComment("儲區");

                entity.Property(e => e.Area_Name).HasComment("儲區名稱");

                entity.Property(e => e.Building_ID)
                    .IsUnicode(false)
                    .HasComment("棟別");

                entity.Property(e => e.Building_Name).HasComment("棟別名稱");

                entity.Property(e => e.CBM).HasComment("儲位空間");

                entity.Property(e => e.Create_By)
                    .IsUnicode(false)
                    .HasComment("infromix建立者");

                entity.Property(e => e.Create_Time).HasComment("informix建立時間");

                entity.Property(e => e.Floor_ID)
                    .IsUnicode(false)
                    .HasComment("樓別");

                entity.Property(e => e.Floor_Name).HasComment("樓別名稱");

                entity.Property(e => e.Location_Name).HasComment("儲位名稱");

                entity.Property(e => e.Max_Percent).HasComment("最大可用比率");

                entity.Property(e => e.Remark).HasComment("備註");

                entity.Property(e => e.Status_Type)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Y')")
                    .IsFixedLength(true)
                    .HasComment("資料狀態");

                entity.Property(e => e.Update_By)
                    .IsUnicode(false)
                    .HasComment("異動者");

                entity.Property(e => e.Update_Time).HasComment("異動時間");

                entity.Property(e => e.Warehouse_Name).HasComment("庫別名稱");
            });
            modelBuilder.Entity<VW_FGIN_LOCAT_LIST>().HasNoKey();
            modelBuilder.Entity<VW_FGIN_LOCAT_LIST>(entity =>
            {
                entity.ToView("VW_FGIN_LOCAT_LIST");

                entity.Property(e => e.Article).IsUnicode(false);

                entity.Property(e => e.Cdr_No)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ColorRow).IsUnicode(false);

                entity.Property(e => e.Dept_ID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Factory_ID).IsUnicode(false);

                entity.Property(e => e.Line_Desc).IsUnicode(false);

                entity.Property(e => e.Locat).IsUnicode(false);

                entity.Property(e => e.Model_Name).IsUnicode(false);

                entity.Property(e => e.Suggest_Locat)
                    .IsUnicode(false)
                    .UseCollation("Chinese_Taiwan_Stroke_90_CI_AS");

                entity.Property(e => e.Up_Trno)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


}