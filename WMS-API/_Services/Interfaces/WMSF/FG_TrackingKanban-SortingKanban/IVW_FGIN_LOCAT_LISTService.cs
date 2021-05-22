using System.Collections.Generic;
using WMS_API.Helpers.Params.WMSF.FG_TrackingKanban_SortingKanban;
using System.Threading.Tasks;
using WMS_API.Dtos.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;
using WMS_API.Helpers.Params;

namespace WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban
{
    public interface IVW_FGIN_LOCAT_LISTService
    {
        Task<List<VW_FGIN_LOCAT_LIST>> ExportExcel(SearchParams dataExport);
        Task<List<VW_FGIN_LOCAT_LIST>> GetData(string deptId, string receivedTime, string optionData, string sortBy, string sortType);
        Task<VW_FGIN_LOCAT_LISTDto> SearchFginLocat(SearchParams searchParams, PaginationParams paginationParams);
    }
}