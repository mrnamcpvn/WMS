using System.Collections.Generic;
using System.Threading.Tasks;
using WMS_API.Dtos;
using WMS_API.Models;

namespace WMS_API._Services.Interface
{
    public interface IWMSF_Rack_AreaService 
    {
        Task<object> GetListRackPairs();
        Task<List<SelectOptionsDto>> GetListAreaTotal();
    }
}