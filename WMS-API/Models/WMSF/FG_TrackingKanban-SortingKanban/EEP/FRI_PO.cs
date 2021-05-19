using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.EEP
{
    public partial class FRI_PO
    {

        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [StringLength(5)]
        public string Factory_ID { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#34399;&#30908;
        /// </summary>
        [Key]
        [StringLength(20)]
        public string PO { get; set; }

        /// <summary>
        /// &#27454;&#34399;
        /// </summary>
        [StringLength(50)]
        public string Article { get; set; }

        /// <summary>
        /// &#22411;&#39636;&#20195;&#34399;
        /// </summary>
        [StringLength(20)]
        public string Model_ID { get; set; }

        /// <summary>
        /// &#22411;&#39636;&#21517;&#31281;
        /// </summary>
        [StringLength(50)]
        public string Model_Name { get; set; }

        /// <summary>
        /// IC&#32232;&#34399;
        /// </summary>
        [StringLength(10)]
        public string IC_Number { get; set; }

        /// <summary>
        /// &#29983;&#31649;&#25215;&#35582;&#26085;&#26399;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Comfirmed_Date { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#25976;&#37327;
        /// </summary>
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? Order_Qty { get; set; }

        /// <summary>
        /// &#23526;&#38555;&#25976;&#37327;
        /// </summary>
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? Actual_Qty { get; set; }

        /// <summary>
        /// &#23458;&#25142;&#21517;&#31281;
        /// </summary>
        [StringLength(50)]
        public string Customer { get; set; }

        /// <summary>
        /// &#23458;&#25142;&#20195;&#34399;
        /// </summary>
        [StringLength(50)]
        public string Customer_Id { get; set; }

        /// <summary>
        /// &#27298;&#39511;&#23436;&#25104;&#26085;&#26399;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Complete_Date { get; set; }

        /// <summary>
        /// &#27298;&#39511;&#23436;&#25104;&#21542;
        /// </summary>
        [StringLength(1)]
        public string Complete { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#32773;
        /// </summary>
        [StringLength(16)]
        public string Updated_By { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Updated_Time { get; set; }

        /// <summary>
        /// &#36681;&#20837;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Biz_Time { get; set; }

        /// <summary>
        /// &#29983;&#29986;&#32218;&#21029;
        /// </summary>
        [StringLength(50)]
        public string Production_Line { get; set; }

        /// <summary>
        /// &#38928;&#35336;&#20986;&#36008;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Plan_Ship_Date { get; set; }

        /// <summary>
        /// &#23526;&#38555;&#35201;&#20986;&#36008;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Real_Ship_Date { get; set; }

        /// <summary>
        /// &#38928;&#35336;&#29983;&#29986;&#23436;&#25104;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Plan_Finish_Date { get; set; }

        /// <summary>
        /// &#23526;&#38555;&#29983;&#29986;&#23436;&#25104;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Real_Finish_Date { get; set; }

        /// <summary>
        /// &#20837;&#24235;&#32317;&#25976;&#37327;
        /// </summary>
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? In_Qty { get; set; }

        /// <summary>
        /// &#30906;&#35469;&#20986;&#36008;
        /// </summary>
        [StringLength(1)]
        public string Ship_Complete { get; set; }

        /// <summary>
        /// &#20786;&#20301;
        /// </summary>
        [StringLength(200)]
        public string Location_Bin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Last_Release { get; set; }
        [StringLength(30)]
        public string Upload_File { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Upload_Datetime { get; set; }

        /// <summary>
        /// &#20986;&#36008;&#23458;&#25142;&#22283;&#23478;&#21029;
        /// </summary>
        [StringLength(50)]
        public string Nation { get; set; }

        /// <summary>
        /// &#35037;&#31665;&#26041;&#24335;
        /// </summary>
        [StringLength(1)]
        public string Ctntyp { get; set; }

        /// <summary>
        /// &#20986;&#36008;&#26041;&#24335;
        /// </summary>
        [StringLength(20)]
        public string Shipping_Way { get; set; }

        /// <summary>
        /// &#20998;&#39006;
        /// </summary>
        [StringLength(40)]
        public string Category { get; set; }

        /// <summary>
        /// BA&#27298;&#39511;&#23436;&#25104;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? BA_Complete_Date { get; set; }

        /// <summary>
        /// BA&#27298;&#39511;&#23436;&#25104;&#21542;
        /// </summary>
        [StringLength(1)]
        public string BA_Complete { get; set; }

        /// <summary>
        /// &#23458;&#25142;&#35330;&#21934;&#34399;&#30908;
        /// </summary>
        [StringLength(20)]
        public string Pono { get; set; }

        /// <summary>
        /// Service ID&#20195;&#30908;
        /// </summary>
        [StringLength(4)]
        public string Service_Seq { get; set; }

        /// <summary>
        /// &#25104;&#22411;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Plan_Start_ASY { get; set; }

        /// <summary>
        /// &#19979;&#21934;&#23458;&#25142;
        /// </summary>
        [StringLength(4)]
        public string Customer_No { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#29376;&#24907;
        /// </summary>
        [StringLength(1)]
        public string Flag { get; set; }
        [StringLength(2)]
        public string Scolor { get; set; }
        [StringLength(70)]
        public string Scolna { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Fetd { get; set; }
        [StringLength(1)]
        public string Kind { get; set; }
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? QA_Inspected_Qty { get; set; }
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? QA_Passed_Qty { get; set; }
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? QA_Not_Passed_Qty { get; set; }
        [Column(TypeName = "decimal(11, 0)")]
        public decimal? QA_Defect_Qty_First { get; set; }
    }
}