
using WMS_API._Repositories.Interfaces;
using WMS_API.Data;
using WMS_API.Models;

namespace WMS_API._Repositories.Repositories
{
    public class WMSF_Carton_LocatRepository : Repository<WMSF_Carton_Locat>, IWMSF_Carton_LocatRepository
    {
        public WMSF_Carton_LocatRepository(DataContext context) : base(context)
        {

        }
    }
}