using WMS_API._Repositories.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Repositories.Repository.WMSF.FG_REPORT_COMPARE;
using WMS_API.Data.WMSF.FG_REPORT_COMPARE;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Repositories.Repositories.WMSF.FG_REPORT_COMPARE
{
    public class WMSF_FGIN_CompareReportRepository : DBContext_WMSRepository<WMSF_FGIN_ReportCompare>, IWMSF_FGIN_ReportCompareRepository
    {
        public WMSF_FGIN_CompareReportRepository(DBContext_WMS context) : base(context)
        {

        }
    }
}