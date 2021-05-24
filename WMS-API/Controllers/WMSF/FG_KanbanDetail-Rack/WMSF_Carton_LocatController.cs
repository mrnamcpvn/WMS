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

namespace WMS_API.Controllers
{
    public class WMSF_Carton_LocatController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWMSF_Carton_LocatService _wMSF_Carton_LocatService;

        public WMSF_Carton_LocatController(IConfiguration configuration, IMapper mapper, IWMSF_Carton_LocatService wMSF_Carton_LocatService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _wMSF_Carton_LocatService = wMSF_Carton_LocatService;
        }
         [HttpGet("LoadDataChart")]
        public async Task<IActionResult> LoadDataChart()
        {
            var result = await _wMSF_Carton_LocatService.LoadDataChart();
            return Ok(result);
        }
    }
}