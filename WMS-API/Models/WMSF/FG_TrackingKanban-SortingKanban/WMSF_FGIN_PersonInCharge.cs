using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban
{
    public partial class WMSF_FGIN_PersonInCharge
    {

        /// <summary>
        /// 廠別代號
        /// </summary>
        [Key]
        [StringLength(10)]
        public string Factory_ID { get; set; }

        /// <summary>
        /// &#37096;&#38272;&#20195;&#34399;
        /// </summary>
        [Key]
        [StringLength(3)]
        public string Dept_ID { get; set; }

        /// <summary>
        /// &#29702;&#36008;&#20154;&#21729;&#24037;&#34399;
        /// </summary>
        [StringLength(10)]
        public string Emp_ID { get; set; }

        /// <summary>
        /// &#29702;&#36008;&#20154;&#21729;&#21517;&#31281;
        /// </summary>
        [StringLength(50)]
        public string Emp_Name { get; set; }

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