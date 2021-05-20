using WMS_API._Repositories.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Repositories.Repository.WMSF.FG_REPORT_COMPARE;
using WMS_API.Data.WMSF.FG_REPORT_COMPARE;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Repositories.Repositories.WMSF.FG_REPORT_COMPARE
{
    public class WMSF_FG_CompareReportRepository : DBContext_WMSRepository<WMSF_FG_CompareReport>, IWMSF_FG_CompareReportRepository
    {
        public WMSF_FG_CompareReportRepository(DBContext_WMS context) : base(context)
        {
        }
    }
}