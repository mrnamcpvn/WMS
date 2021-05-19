using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models
{
    public class WMSF_Carton_Locat
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Barcode { get; set; }

        [Required]
        [StringLength(5)]
        public string Warehouse_ID { get; set; }

        [Required]
        [StringLength(5)]
        public string Location_ID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string Order_ID { get; set; }

        public decimal Carton_Pairs { get; set; }

        public decimal Volume { get; set; }

        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime? Create_Time { get; set; }

        [Required]
        [StringLength(1)]
        public string Status_Type { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}
