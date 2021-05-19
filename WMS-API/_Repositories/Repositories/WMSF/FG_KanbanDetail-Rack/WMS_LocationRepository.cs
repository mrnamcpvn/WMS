
using WMS_API._Repositories.Interfaces;
using WMS_API.Data;
using WMS_API.Models;

namespace WMS_API._Repositories.Repositories
{
    public class WMS_LocationRepository : Repository<WMS_Location>, IWMS_LocationRepository
    {
        public WMS_LocationRepository(DataContext context) : base(context)
        {

        }
    }
}