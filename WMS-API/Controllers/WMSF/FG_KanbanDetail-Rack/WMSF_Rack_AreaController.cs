using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using WMS_API._Services.Services;
using WMS_API._Services.Interface;
using System.Collections.Generic;
using WMS_API.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Aspose.Cells;

namespace WMS_API.Controllers
{
    public class WMSF_Rack_AreaController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWMSF_Rack_AreaService _wMSF_Rack_AreaService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WMSF_Rack_AreaController(IConfiguration configuration, IMapper mapper, IWMSF_Rack_AreaService wMSF_Rack_AreaService, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _mapper = mapper;
            _wMSF_Rack_AreaService = wMSF_Rack_AreaService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetListRackPairs")]
        public async Task<IActionResult> GetListRackPairs()
        {
            var result = await _wMSF_Rack_AreaService.GetListRackPairs();
            return Ok(result);
        }
        [HttpGet("GetListAreaTotal")]
        public async Task<IActionResult> GetListAreaTotal()
        {
            var result = await _wMSF_Rack_AreaService.GetListAreaTotal();
            return Ok(result);
        }
        [HttpPost("exportExcel")]
        public async Task<IActionResult> ExportExcel([FromBody] List<WMS_LocationViewDto> data)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, @"Resources\Template\Export_KanBan_Template.xlsx");
            WorkbookDesigner designer = new WorkbookDesigner();
            designer.Workbook = new Workbook(path);
            designer.SetDataSource("result", data);
            designer.Workbook.Worksheets.ActiveSheetIndex = 0;
            designer.Process();
            MemoryStream stream = new MemoryStream();

            if (data != null)
            {
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            byte[] result = stream.ToArray();

            return File(result, "application/xlsx", "Export_KanBan_Template" + "_"+ DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".xlsx");

        }
    }
}