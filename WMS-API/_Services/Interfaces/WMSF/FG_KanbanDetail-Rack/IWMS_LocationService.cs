using System.Collections.Generic;
using System.Threading.Tasks;
using WMS_API.Dtos;
using WMS_API.Helpers.Params;
using WMS_API.Helpers.Utilities;
using WMS_API.ViewModels;

namespace WMS_API._Services.Interface
{
    public interface IWMS_LocationService
    {
        Task<List<SelectOptionsDto>> GetListWarehouse();
        Task<List<SelectOptionsDto>> GetListBuilding();
        Task<List<SelectOptionsDto>> GetListFloor();
        Task<List<SelectOptionsDto>> GetListArea();

        Task<PageListUtility<WMS_LocationViewDto>> SearchData(PaginationParams paginationParams, SearchParam searchParam);
        Task<List<WMS_LocationViewDto>> SearchDataNoPagintion(SearchParam searchParam);
    }
}