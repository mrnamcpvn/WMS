using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS
{
    public partial class VW_FGIN_LOCAT_LIST
    {
        [StringLength(10)]
        public string Factory_ID { get; set; }
        [StringLength(3)]
        public string Dept_ID { get; set; }
        [StringLength(10)]
        public string Line_Desc { get; set; }
        [Required]
        [StringLength(15)]
        public string Up_Trno { get; set; }
        [StringLength(15)]
        public string Cdr_No { get; set; }
        [StringLength(50)]
        public string Model_Name { get; set; }
        [StringLength(50)]
        public string Article { get; set; }
        [Column(TypeName = "decimal(7, 0)")]
        public decimal? CTN_Qty { get; set; }
        [Column(TypeName = "decimal(8, 1)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "decimal(8, 5)")]
        public decimal Meas { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Up_UTC_Dat { get; set; }
        [StringLength(50)]
        public string Emp_Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? In_UTC_Dat { get; set; }
        [StringLength(255)]
        public string Locat { get; set; }
        [StringLength(5)]
        public string Suggest_Locat { get; set; }
        [Required]
        [StringLength(6)]
        public string ColorRow { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Plan_Export_Date { get; set; }
    }
}