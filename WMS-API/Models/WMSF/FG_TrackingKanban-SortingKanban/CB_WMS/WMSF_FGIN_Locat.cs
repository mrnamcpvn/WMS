using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS
{
    public partial class WMSF_FGIN_Locat
    {

        /// <summary>
        /// 廠別代號
        /// </summary>
        [StringLength(10)]
        public string Factory_ID { get; set; }

        /// <summary>
        /// &#29983;&#29986;&#37096;&#38272;
        /// </summary>
        [StringLength(3)]
        public string Dept_ID { get; set; }

        /// <summary>
        /// &#36681;&#20132;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(15)]
        public string Up_Trno { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#34399;&#30908;
        /// </summary>
        [StringLength(15)]
        public string Cdr_No { get; set; }

        /// <summary>
        /// &#31665;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 0)")]
        public decimal? CTN_Qty { get; set; }

        /// <summary>
        /// &#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(8, 1)")]
        public decimal? Qty { get; set; }

        /// <summary>
        /// &#26448;&#31309;
        /// </summary>
        [Column(TypeName = "decimal(8, 5)")]
        public decimal Meas { get; set; }

        /// <summary>
        /// &#25910;&#36008;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Up_UTC_Dat { get; set; }

        /// <summary>
        /// &#29702;&#36008;&#23436;&#25104;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? In_UTC_Dat { get; set; }

        /// <summary>
        /// &#20786;&#20301;&#36039;&#26009;
        /// </summary>
        [StringLength(255)]
        public string Locat { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#32773;
        /// </summary>
        [StringLength(50)]
        public string Update_By { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Update_Time { get; set; }
    }
}