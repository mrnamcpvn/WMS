using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS
{
    public partial class WMSF_Carton_Locat
    {

        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        /// <summary>
        /// &#26781;&#30908;
        /// </summary>
        [Key]
        [StringLength(30)]
        public string Barcode { get; set; }

        /// <summary>
        /// &#24235;&#21029;
        /// </summary>
        [Required]
        [StringLength(5)]
        public string Warehouse_ID { get; set; }

        /// <summary>
        /// &#20786;&#20301;&#32232;&#34399;
        /// </summary>
        [Required]
        [StringLength(5)]
        public string Location_ID { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#32232;&#34399;
        /// </summary>
        [Key]
        [StringLength(15)]
        public string Order_ID { get; set; }

        /// <summary>
        /// &#26412;&#31665;&#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal Carton_Pairs { get; set; }

        /// <summary>
        /// &#26448;&#26009;&#39636;&#31309;
        /// </summary>
        [Column(TypeName = "decimal(8, 5)")]
        public decimal Volume { get; set; }

        /// <summary>
        /// informix&#24314;&#31435;&#32773;
        /// </summary>
        [StringLength(50)]
        public string Create_By { get; set; }

        /// <summary>
        /// informix&#24314;&#31435;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Create_Time { get; set; }

        /// <summary>
        /// &#36039;&#26009;&#29376;&#24907;
        /// </summary>
        [Required]
        [StringLength(1)]
        public string Status_Type { get; set; }

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