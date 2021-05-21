using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WMS_API.Helpers.Params.WMSF.FG_TrackingKanban_SortingKanban;
using Aspose.Cells;
using System.Linq;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Helpers.Params;

namespace WMS_API.Controllers.WMSF.FG_TrackingKanban_SortingKanban
{
    [ApiController]
    [Route("api/[controller]")]
    public class FG_TrackingKanban_SortingKanbanController : ControllerBase
    {
        private readonly IVW_WMS_DepartmentService _vW_WMS_DepartmentService;
        private readonly IVW_FGIN_LOCAT_LISTService _vW_FGIN_LOCAT_LISTService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FG_TrackingKanban_SortingKanbanController(IVW_WMS_DepartmentService vW_WMS_DepartmentService, IVW_FGIN_LOCAT_LISTService vW_FGIN_LOCAT_LISTService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _vW_FGIN_LOCAT_LISTService = vW_FGIN_LOCAT_LISTService;
            _vW_WMS_DepartmentService = vW_WMS_DepartmentService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchParams searchParams, [FromQuery] PaginationParams paginationParams)
        {
            var result = await _vW_FGIN_LOCAT_LISTService.SearchFginLocat(searchParams, paginationParams);
            return Ok(result);
        }
        [HttpGet("getAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            var result = await _vW_WMS_DepartmentService.GetAllDepartments();
            return Ok(result);
        }

        [HttpPost("exportExcel")]
        public async Task<IActionResult> ExportExcel(SearchParams searchParams)
        {
            var data = await _vW_FGIN_LOCAT_LISTService.ExportExcel(searchParams);
            var dataExport = data.Select(x => new
            {
                x.Line_Desc,
                x.Up_Trno,
                x.Cdr_No,
                Plan_Export_Date = x.Plan_Export_Date.ToString().Trim() != string.Empty ? Convert.ToDateTime(x.Plan_Export_Date).ToString("yyyy/MM/dd") : "",
                x.Model_Name,
                x.Article,
                x.CTN_Qty,
                x.Qty,
                x.Meas,
                Up_UTC_Dat = x.Up_UTC_Dat.ToString().Trim() != string.Empty ? Convert.ToDateTime(x.Up_UTC_Dat).ToString("yyyy/MM/dd HH:mm:ss") : "",
                x.Emp_Name,
                x.In_UTC_Dat,
                x.Locat,
                x.Suggest_Locat
            }).ToList();
            WorkbookDesigner designer = new WorkbookDesigner();
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\FG_TrackingKanban-SortingKanban\\FG_TrackingKanban_SortingKanban.xlsx");
            designer.Workbook = new Workbook(path);
            Worksheet ws = designer.Workbook.Worksheets[0];
            designer.SetDataSource("result", dataExport);
            designer.Process();

            MemoryStream stream = new MemoryStream();
            designer.Workbook.Save(stream, SaveFormat.Xlsx);
            byte[] result = stream.ToArray();

            return File(result, "application/xlsx", "ExportData" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx");
        }

    }
}