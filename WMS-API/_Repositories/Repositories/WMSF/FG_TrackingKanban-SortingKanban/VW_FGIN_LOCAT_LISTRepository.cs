using WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using WMS_API._Repositories.Interfaces;

namespace WMS_API._Repositories.Repositories.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_FGIN_LOCAT_LISTRepository : DbContextRepository<VW_FGIN_LOCAT_LIST>, IVW_FGIN_LOCAT_LISTRepository
    {
        public VW_FGIN_LOCAT_LISTRepository(DBContext context) : base(context)
        {

        }
    }
}