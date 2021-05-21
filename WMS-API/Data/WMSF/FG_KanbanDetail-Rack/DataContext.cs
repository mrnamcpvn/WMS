
using Microsoft.EntityFrameworkCore;
using WMS_API.Dtos;
using WMS_API.Models;

namespace WMS_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<WMS_Location> WMS_Location { get; set; }
        public DbSet<WMSF_Carton_Locat> WMSF_Carton_Locat { get; set; }
        public DbSet<WMSF_Rack_Area> WMSF_Rack_Area { get; set; }
        public DbSet<WMS_LocationViewDto> WMS_LocationView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.id });
            modelBuilder.Entity<WMS_Location>().HasKey(x => new { x.Factory_ID, x.Location_ID, x.Warehouse_ID });
            modelBuilder.Entity<WMSF_Carton_Locat>().HasKey(x => new { x.Factory_ID, x.Barcode, x.Order_ID });
            modelBuilder.Entity<WMSF_Rack_Area>().HasKey(x => new { x.Area_ID });
            modelBuilder.Entity<WMS_LocationViewDto>().HasNoKey();

        }
    }
}