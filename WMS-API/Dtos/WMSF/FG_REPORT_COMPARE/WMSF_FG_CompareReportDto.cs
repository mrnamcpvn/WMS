using System;

namespace WMS_API.Dtos.WMSF.FG_REPORT_COMPARE
{
    public class WMSF_FG_CompareReportDto
    {

        public string Status { get; set; }
        public string Cdr_No { get; set; }
        public string Model_Name { get; set; }
        public string Article { get; set; }
        public decimal? PO_Locat_Qty { get; set; }
        public decimal? PO_ERP_Qty { get; set; }
        public decimal? Balance { get; set; }
        public string Location_ID { get; set; }
        public int Accuracy { get; set; }
    }

}