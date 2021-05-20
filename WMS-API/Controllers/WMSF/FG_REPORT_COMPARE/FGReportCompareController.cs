using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QMS_API.Helpers.Utilities;
using WMS_API._Services.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API.Helpers.Params;

namespace WMS_API.Controllers.WMSF.FG_REPORT_COMPARE
{
    public class FGReportCompareController : ApiController
    {
        private readonly IWebHostEnvironment _webHostEnv;
        private readonly IWMSF_FG_CompareReportService _FG_CompareReportService;
        public FGReportCompareController(IWebHostEnvironment webHostEnv, IWMSF_FG_CompareReportService wMSF_FG_CompareReportService)
        {
            _FG_CompareReportService = wMSF_FG_CompareReportService;
            _webHostEnv = webHostEnv;
        }

        [HttpGet("GetCompareByRack")]
        public async Task<IActionResult> GetCompareByRack(string reportTime, [FromQuery] PaginationParams pagination)
        {
            var result = await _FG_CompareReportService.GetAll(reportTime, pagination);
            return Ok(result);
        }

        [HttpGet("ExportExcelByRack")]
        public async Task<IActionResult> ExportExcelByRack(string reportTime)
        {
            var data = await _FG_CompareReportService.ExportExcelByRack(reportTime);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                // Create Sheet1
                var ws = package.Workbook.Worksheets.Add("Sheet1");

                // Add header
                // Add header
                ws.Row(1).Style.SetAlignCenter();
                ws.Row(1).Style.Font.Bold = true;
                ws.Column(3).Style.SetDateFormat();
                ws.Column(10).Style.SetPercentFormat();
                ws.Cells[1, 1, 1, 9].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, 9].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#20a8d8"));
                ws.Cells[1, 1, 1, 9].Style.Font.Color.SetColor(Color.White);
                ws.Cells[1, 1].Value = "Status";
                ws.Cells[1, 2].Value = "PO #";
                ws.Cells[1, 3].Value = "Model Name";
                ws.Cells[1, 4].Value = "Articel #";
                ws.Cells[1, 5].Value = "WMS Qty";
                ws.Cells[1, 6].Value = "HP Qty";
                ws.Cells[1, 7].Value = "Balance";
                ws.Cells[1, 8].Value = "Rack";
                ws.Cells[1, 9].Value = "Accuracy";

                // Add body
                int index = 2;
                foreach (var item in data)
                {
                    ws.Cells[index, 1].Value = item.Status;
                    ws.Cells[index, 2].Value = item.Cdr_No;
                    ws.Cells[index, 3].Value = item.Model_Name;
                    ws.Cells[index, 4].Value = item.Article;
                    ws.Cells[index, 5].Value = item.Location_ID;
                    ws.Cells[index, 6].Value = item.PO_Locat_Qty;
                    ws.Cells[index, 7].Value = item.PO_ERP_Qty;
                    ws.Cells[index, 8].Value = item.Balance;
                    ws.Cells[index, 9].Value = item.Accuracy;
                    index++;
                }
                index--;

                ws.Cells[1, 1, index, 9].Style.SetAllBorders();
                ws.Column(1).AutoFit(10);
                ws.Column(2).AutoFit(15);
                ws.Column(3).AutoFit(15);
                ws.Column(4).AutoFit(15);
                ws.Column(5).AutoFit(15);
                ws.Column(6).AutoFit(30);
                ws.Column(7).AutoFit(15);
                ws.Column(8).AutoFit(15);
                ws.Column(9).AutoFit(15);
                package.Save();
            }

            // Export
            stream.Position = 0;
            string excelName = "Report_Compare_By_Rack.xlsx";

            return File(stream, "application/xlsx", excelName + DateTime.Now.ToString("-MM.dd.yyyy"));
        }
        [HttpGet("ExportExcelByPO")]
        public async Task<IActionResult> ExportExcelByPO(string reportTime)
        {
            var data = await _FG_CompareReportService.ExportExcelByRack(reportTime);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                // Create Sheet1
                var ws = package.Workbook.Worksheets.Add("Sheet1");

                // Add header
                ws.Row(1).Style.SetAlignCenter();
                ws.Row(1).Style.Font.Bold = true;
                ws.Column(3).Style.SetDateFormat();
                ws.Column(7).Style.SetPercentFormat();
                ws.Cells[1, 1, 1, 7].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells[1, 1, 1, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#20a8d8"));
                ws.Cells[1, 1, 1, 7].Style.Font.Color.SetColor(Color.White);
                ws.Cells[1, 1].Value = "Status";
                ws.Cells[1, 2].Value = "PO #";
                ws.Cells[1, 3].Value = "Model Name";
                ws.Cells[1, 4].Value = "Articel #";
                ws.Cells[1, 5].Value = "WMS Qty";
                ws.Cells[1, 6].Value = "HP Qty";
                ws.Cells[1, 7].Value = "Balance";
                // Add body
                int index = 2;
                foreach (var item in data)
                {
                    ws.Cells[index, 1].Value = item.Status;
                    ws.Cells[index, 2].Value = item.Cdr_No;
                    ws.Cells[index, 3].Value = item.Model_Name;
                    ws.Cells[index, 4].Value = item.Article;
                    ws.Cells[index, 5].Value = item.PO_Locat_Qty;
                    ws.Cells[index, 6].Value = item.PO_ERP_Qty;
                    ws.Cells[index, 7].Value = item.Balance;
                    index++;
                }
                index--;

                ws.Cells[1, 1, index, 7].Style.SetAllBorders();
                ws.Column(1).AutoFit(10);
                ws.Column(2).AutoFit(15);
                ws.Column(3).AutoFit(15);
                ws.Column(4).AutoFit(15);
                ws.Column(5).AutoFit(15);
                ws.Column(6).AutoFit(30);
                ws.Column(7).AutoFit(15);
                package.Save();
            }

            // Export
            stream.Position = 0;
            string excelName = "Report_Compare_By_PO.xlsx";

            return File(stream, "application/xlsx", excelName + DateTime.Now.ToString("-MM.dd.yyyy"));
        }

    }
}