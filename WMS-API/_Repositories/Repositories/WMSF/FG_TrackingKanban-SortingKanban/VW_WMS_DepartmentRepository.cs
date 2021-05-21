using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;

namespace WMS_API._Repositories.Repositories.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_WMS_DepartmentRepository : CB_WMSRepository<VW_WMS_Department>, IVW_WMS_DepartmentRepository
    {
        public VW_WMS_DepartmentRepository(CB_WMSContext context) : base(context)
        {
        }
    }
}