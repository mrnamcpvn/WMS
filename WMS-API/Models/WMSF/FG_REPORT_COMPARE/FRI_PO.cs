using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_REPORT_COMPARE
{
    public class FRI_PO
    {
        [Key]
        [Column(Order = 0)]
        public string Factory_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string PO { get; set; }

        public string Article { get; set; }

        public string Model_ID { get; set; }

        public string Model_Name { get; set; }

        public string IC_Number { get; set; }

        public DateTime? Comfirmed_Date { get; set; }

        public decimal? Order_Qty { get; set; }

        public decimal? Actual_Qty { get; set; }

        public string Customer { get; set; }

        public string Customer_Id { get; set; }

        public DateTime? Complete_Date { get; set; }

        public string Complete { get; set; }

        public string Updated_By { get; set; }

        public DateTime? Updated_Time { get; set; }

        public DateTime? Biz_Time { get; set; }

        public string Production_Line { get; set; }

        public DateTime? Plan_Ship_Date { get; set; }

        public DateTime? Real_Ship_Date { get; set; }

        public DateTime? Plan_Finish_Date { get; set; }

        public DateTime? Real_Finish_Date { get; set; }

        public decimal? In_Qty { get; set; }

        public string Ship_Complete { get; set; }

        public string Location_Bin { get; set; }

        public DateTime? Last_Release { get; set; }

        public string Upload_File { get; set; }

        public DateTime? Upload_Datetime { get; set; }

        public string Nation { get; set; }

        public string Ctntyp { get; set; }

        public string Shipping_Way { get; set; }

        public string Category { get; set; }

        public DateTime? BA_Complete_Date { get; set; }


        public string BA_Complete { get; set; }


        public string Pono { get; set; }


        public string Service_Seq { get; set; }

        public DateTime? Plan_Start_ASY { get; set; }


        public string Customer_No { get; set; }


        public string Flag { get; set; }


        public string Kind { get; set; }


        public string Scolor { get; set; }
    }
}