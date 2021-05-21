using System.ComponentModel.DataAnnotations;

namespace WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS
{
    public partial class VW_WMS_Department
    {
        [Required]
        [StringLength(1)]
        public string Factory_ID { get; set; }
        [Required]
        [StringLength(3)]
        public string Dept_ID { get; set; }
        [StringLength(40)]
        public string Dept_Desc { get; set; }
        [StringLength(40)]
        public string Line_Desc { get; set; }
        [StringLength(1)]
        public string Work_Center { get; set; }
    }
}