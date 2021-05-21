using WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban;

using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;

namespace WMS_API._Repositories.Repositories.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_FGIN_LOCAT_LISTRepository : CB_WMSRepository<VW_FGIN_LOCAT_LIST>, IVW_FGIN_LOCAT_LISTRepository
    {
        public VW_FGIN_LOCAT_LISTRepository(CB_WMSContext context) : base(context)
        {

        }
    }
}