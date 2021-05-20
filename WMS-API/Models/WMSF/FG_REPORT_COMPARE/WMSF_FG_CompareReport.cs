using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_REPORT_COMPARE
{
    public class WMSF_FG_CompareReport
    {
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        public DateTime? Closing_Date { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Cdr_No { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Location_ID { get; set; }

        public string Order_Status { get; set; }
        [Column(TypeName = "decimal(8,1)")]
        public decimal? PO_WMS_Qty { get; set; }
        [Column(TypeName = "decimal(8,1)")]
        public decimal? PO_ERP_Qty { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}