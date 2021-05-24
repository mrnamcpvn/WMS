using System.Collections.Generic;
using System.Threading.Tasks;
using WMS_API.Dtos;

namespace WMS_API._Services.Interface
{
    public interface IWMSF_Carton_LocatService 
    {
          Task<List<ChartModelDto>> LoadDataChart();
    }
}