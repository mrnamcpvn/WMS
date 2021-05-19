using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMS_API.Models
{
    public class WMS_Location
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string Location_ID { get; set; }

        [StringLength(50)]
        public string Location_Name { get; set; }

        [Key]
        [Column(Order = 2)]
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

        public decimal CBM { get; set; }

        public decimal Max_Percent { get; set; }

        [StringLength(100)]
        public string Remark { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Time { get; set; }

        [Required]
        [StringLength(1)]
        public string Status_Type { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
