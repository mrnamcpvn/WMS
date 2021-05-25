using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_REPORT_COMPARE
{
    /// <summary>
    ///     /// &#25104;&#21697;&#20489;&#24235;&#23384;&#27604;&#36611;&#36039;&#26009;&#27284;
    /// </summary>
    public partial class WMSF_FG_CompareReport
    {
        /// <summary>
        ///         /// 廠別
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Factory_ID { get; set; }
        /// <summary>
        ///         /// &#26085;&#32080;&#26178;&#38291;
        /// </summary>
        [Key]
        [Column(TypeName = "date")]
        public DateTime Closing_Date { get; set; }
        /// <summary>
        ///         /// &#35330;&#21934;&#34399;&#30908;
        /// </summary>
        [Key]
        [StringLength(15)]
        public string Cdr_No { get; set; }
        [Key]
        [StringLength(5)]
        public string Location_ID { get; set; }
        /// <summary>
        ///         /// &#35330;&#21934;&#29376;&#24907;
        /// </summary>
        [Required]
        [StringLength(1)]
        public string Order_Status { get; set; }
        /// <summary>
        ///         /// &#20786;&#20301;&#24235;&#23384;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(8, 1)")]
        public decimal PO_WMS_Qty { get; set; }
        /// <summary>
        ///         /// ERP&#24235;&#23384;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(8, 1)")]
        public decimal PO_ERP_Qty { get; set; }
        /// <summary>
        ///         /// &#30064;&#21205;&#32773;
        /// </summary>
        [StringLength(50)]
        public string Update_By { get; set; }
        /// <summary>
        ///         /// &#30064;&#21205;&#26085;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Update_Time { get; set; }
    }
}