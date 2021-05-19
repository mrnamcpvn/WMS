using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMS_API.Models
{
    public class WMSF_Rack_Area
    {
        [Key]
        [StringLength(5)]
        public string Area_ID { get; set; }

        [StringLength(50)]
        public string Area_Name { get; set; }

        [StringLength(50)]
        public string Area_Short_Title { get; set; }

        [StringLength(1)]
        public string Warehouse_A { get; set; }

        [StringLength(1)]
        public string Hide_Rack { get; set; }

        [StringLength(1)]
        public string Audit { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
