using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using WMS_API.Helpers.Utilities;
using WMS_API._Services.Interface;

namespace WMS_API._Services.Services
{
    public class WMSF_Carton_LocatService : IWMSF_Carton_LocatService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private OperationResult operationResult;

        public WMSF_Carton_LocatService(IMapper mapper, MapperConfiguration mapperConfiguration)
        {
            _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
        }


    }
}