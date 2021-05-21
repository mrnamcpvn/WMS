
using WMS_API.Helpers.Utilities;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;

namespace WMS_API.Dtos.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_FGIN_LOCAT_LISTDto
    {
        public PageListUtility<VW_FGIN_LOCAT_LIST> Dtos { get; set; }
        public decimal sumCartons { get; set; }
        public decimal sumPairs { get; set; }
        public decimal sumCBM { get; set; }
    }
}