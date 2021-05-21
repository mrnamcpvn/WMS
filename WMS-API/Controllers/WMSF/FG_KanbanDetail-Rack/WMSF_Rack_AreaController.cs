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

namespace WMS_API.Controllers
{
    public class WMSF_Rack_AreaController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWMSF_Rack_AreaService _wMSF_Rack_AreaService;

        public WMSF_Rack_AreaController(IConfiguration configuration, IMapper mapper, IWMSF_Rack_AreaService wMSF_Rack_AreaService )
        {
            _configuration = configuration;
            _mapper = mapper;
            _wMSF_Rack_AreaService = wMSF_Rack_AreaService;
        }
        [HttpGet("GetListRackPairs")]
        public async Task<IActionResult> GetListRackPairs()
        {
            var result = await _wMSF_Rack_AreaService.GetListRackPairs();
            return Ok(result);
        }
    }
}