using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban;

namespace WMS_API._Services.Services.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_WMS_DepartmentService : IVW_WMS_DepartmentService
    {
        private readonly IVW_WMS_DepartmentRepository _vW_WMS_DepartmentRepository;

        public VW_WMS_DepartmentService(IVW_WMS_DepartmentRepository vW_WMS_DepartmentRepository)
        {
            _vW_WMS_DepartmentRepository = vW_WMS_DepartmentRepository;
        }

        public async Task<List<VW_WMS_Department>> GetAllDepartments()
        {
            return await _vW_WMS_DepartmentRepository.FindAll().ToListAsync();
        }

        public async Task<VW_WMS_Department> GetDepartment(string dept_id)
        {
            return await _vW_WMS_DepartmentRepository.FindById(dept_id);
        }
    }
}