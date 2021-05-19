using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_REPORT_COMPARE
{
    public partial class WMSF_FG_CompareReport
    {
        [Required]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Closing_Date { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string Cdr_No { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string Location_ID { get; set; }

        [Required]
        [StringLength(1)]
        public string Order_Status { get; set; }
        public decimal PO_WMS_Qty { get; set; }
        public decimal PO_ERP_Qty { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}