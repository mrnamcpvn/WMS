using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban
{
    public partial class WMS_Location
    {

        [Key]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        [Key]
        [StringLength(5)]
        public string Location_ID { get; set; }

        [StringLength(50)]
        public string Location_Name { get; set; }

        [Key]
        [StringLength(5)]
        public string Warehouse_ID { get; set; }

        [StringLength(50)]
        public string Warehouse_Name { get; set; }

        [StringLength(5)]
        public string Building_ID { get; set; }

        [StringLength(50)]
        public string Building_Name { get; set; }

        [StringLength(5)]
        public string Floor_ID { get; set; }

        [StringLength(50)]
        public string Floor_Name { get; set; }

        [StringLength(5)]
        public string Area_ID { get; set; }

        [StringLength(50)]
        public string Area_Name { get; set; }

        [Column(TypeName = "decimal(11, 2)")]
        public decimal CBM { get; set; }

        [Column(TypeName = "decimal(7, 5)")]
        public decimal Max_Percent { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Create_Time { get; set; }

        [Required]
        [StringLength(1)]
        public string Status_Type { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Update_Time { get; set; }
    }
}