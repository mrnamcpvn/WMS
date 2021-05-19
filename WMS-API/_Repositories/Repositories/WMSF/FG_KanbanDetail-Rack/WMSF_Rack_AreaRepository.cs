
using WMS_API._Repositories.Interfaces;
using WMS_API.Data;
using WMS_API.Models;

namespace WMS_API._Repositories.Repositories
{
    public class WMSF_Rack_AreaRepository : Repository<WMSF_Rack_Area>, IWMSF_Rack_AreaRepository
    {
        public WMSF_Rack_AreaRepository(DataContext context) : base(context)
        {

        }
    }
}