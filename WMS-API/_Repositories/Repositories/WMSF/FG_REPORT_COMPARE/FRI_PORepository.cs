using WMS_API._Repositories.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Repositories.Repository.WMSF.FG_REPORT_COMPARE;
using WMS_API.Data.WMSF.FG_REPORT_COMPARE;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Repositories.Repositories.WMSF.FG_REPORT_COMPARE
{
    public class FRI_PORepository : DBContext_EEFRepository<FRI_PO>, IFRI_PORepository
    {
        public FRI_PORepository(DBContext_EEP context) : base(context)
        {
        }
    }
}