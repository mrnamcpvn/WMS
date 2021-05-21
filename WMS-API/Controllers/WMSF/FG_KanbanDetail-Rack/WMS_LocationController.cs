using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using WMS_API._Services.Interface;
using WMS_API.Helpers.Params;
using WMS_API.Dtos;

namespace WMS_API.Controllers
{
    public class WMS_LocationController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWMS_LocationService _wMS_LocationService;

        public WMS_LocationController(IConfiguration configuration, IMapper mapper, IWMS_LocationService wMS_LocationService)
        {
            _wMS_LocationService = wMS_LocationService;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost("searchData")]
        public async Task<IActionResult> SearchData([FromQuery] PaginationParams paginationParams, ObjectSearchDto objectSearch)
        {
            var result = await _wMS_LocationService.SearchData(paginationParams, objectSearch);
            return Ok(result);
        }
        [HttpGet("GetListWarehouse")]
        public async Task<IActionResult> GetListWarehouse()
        {
            var result = await _wMS_LocationService.GetListWarehouse();
            return Ok(result);
        }
        [HttpGet("GetListBuilding")]
        public async Task<IActionResult> GetListBuilding()
        {
            var result = await _wMS_LocationService.GetListBuilding();
            return Ok(result);
        }
        [HttpGet("GetListFloor")]
        public async Task<IActionResult> GetListFloor()
        {
            var result = await _wMS_LocationService.GetListFloor();
            return Ok(result);
        }
        [HttpGet("GetListArea")]
        public async Task<IActionResult> GetListArea()
        {
            var result = await _wMS_LocationService.GetListArea();
            return Ok(result);
        }
    }
}