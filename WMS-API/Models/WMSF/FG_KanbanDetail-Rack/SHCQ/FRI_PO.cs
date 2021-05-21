using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WMS_API.Models.SHCQ
{
    public class FRI_PO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string PO { get; set; }

        [StringLength(50)]
        public string Article { get; set; }

        [StringLength(20)]
        public string Model_ID { get; set; }

        [StringLength(50)]
        public string Model_Name { get; set; }

        [StringLength(10)]
        public string IC_Number { get; set; }

        public DateTime? Comfirmed_Date { get; set; }

        public decimal? Order_Qty { get; set; }

        public decimal? Actual_Qty { get; set; }

        [StringLength(50)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string Customer_Id { get; set; }

        public DateTime? Complete_Date { get; set; }

        [StringLength(1)]
        public string Complete { get; set; }

        public DateTime? Biz_Time { get; set; }

        [StringLength(50)]
        public string Production_Line { get; set; }

        public DateTime? Plan_Ship_Date { get; set; }

        [StringLength(16)]
        public string Updated_By { get; set; }

        public DateTime? Updated_Time { get; set; }

        public DateTime? Real_Ship_Date { get; set; }

        public DateTime? Plan_Finish_Date { get; set; }

        public DateTime? Real_Finish_Date { get; set; }

        public decimal? In_Qty { get; set; }

        [StringLength(1)]
        public string Ship_Complete { get; set; }

        [StringLength(200)]
        public string Location_Bin { get; set; }

        public DateTime? Last_Release { get; set; }

        [StringLength(30)]
        public string Upload_File { get; set; }

        public DateTime? Upload_Datetime { get; set; }

        [StringLength(50)]
        public string Nation { get; set; }

        [StringLength(1)]
        public string Ctntyp { get; set; }

        [StringLength(20)]
        public string Shipping_Way { get; set; }

        [StringLength(40)]
        public string Category { get; set; }

        public DateTime? BA_Complete_Date { get; set; }

        [StringLength(1)]
        public string BA_Complete { get; set; }

        [StringLength(20)]
        public string Pono { get; set; }

        [StringLength(4)]
        public string Service_Seq { get; set; }

        public DateTime? Plan_Start_ASY { get; set; }

        [StringLength(4)]
        public string Customer_No { get; set; }

        [StringLength(1)]
        public string Flag { get; set; }
    }
}
