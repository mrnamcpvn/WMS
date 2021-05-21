using System.Collections.Generic;
using System.Threading.Tasks;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;

namespace WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban
{
    public interface IVW_WMS_DepartmentService
    {
        Task<List<VW_WMS_Department>> GetAllDepartments();
        Task<VW_WMS_Department> GetDepartment(string dept_id);
    }
}