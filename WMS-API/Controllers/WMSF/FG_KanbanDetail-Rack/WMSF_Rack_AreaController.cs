using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace WMS_API.Controllers
{
    public class WMSF_Rack_AreaController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public WMSF_Rack_AreaController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
    }
}