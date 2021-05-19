using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Data.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban;

namespace WMS_API._Repositories.Repositories.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_WMS_DepartmentRepository : DbContextRepository<VW_WMS_Department>, IVW_WMS_DepartmentRepository
    {
        public VW_WMS_DepartmentRepository(DBContext context) : base(context)
        {
        }
    }
}