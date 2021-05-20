using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using WMS_API.Dtos.WMSF.FG_REPORT_COMPARE;
using WMS_API.Helpers.Params;
using WMS_API.Helpers.Utilities;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Services.Interfaces.WMSF.FG_REPORT_COMPARE
{
    public interface IWMSF_FG_CompareReportService
    {
        Task<PageListUtility<WMSF_FG_CompareReportDto>> GetAll(string reportTime, PaginationParams pagination);
        Task<List<FRI_PODto>> ExportExcelByRack(string reportTime);
    }
}