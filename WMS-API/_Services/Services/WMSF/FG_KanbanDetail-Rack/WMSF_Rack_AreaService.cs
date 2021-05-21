using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using WMS_API.Helpers.Utilities;
using WMS_API._Services.Interface;
using WMS_API._Repositories.Interfaces;
using System;
using WMS_API.Dtos;
using Microsoft.Extensions.Configuration;

namespace WMS_API._Services.Services
{
    public class WMSF_Rack_AreaService : IWMSF_Rack_AreaService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private OperationResult operationResult;

        private readonly IWMSF_Rack_AreaRepository _wMSF_Rack_AreaRepository;
        private readonly IWMSF_Carton_LocatRepository _wMSF_Carton_LocatRepository;
        private readonly IWMS_LocationRepository _wMS_LocationRepository;
        private readonly IConfiguration _configuration;

        public WMSF_Rack_AreaService(IMapper mapper, MapperConfiguration mapperConfiguration, IConfiguration configuration
        , IWMSF_Rack_AreaRepository wMSF_Rack_AreaRepository, IWMSF_Carton_LocatRepository wMSF_Carton_LocatRepository, IWMS_LocationRepository wMS_LocationRepository)
        {
            _configuration = configuration;
            _wMS_LocationRepository = wMS_LocationRepository;
            _wMSF_Carton_LocatRepository = wMSF_Carton_LocatRepository;
            _wMSF_Rack_AreaRepository = wMSF_Rack_AreaRepository;
            _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;

        }

        public async Task<object> GetListRackPairs()
        {
            var warehouse = _configuration.GetSection("AppSettings:WarehouseName").Value;
            var AreaShows = await _wMSF_Rack_AreaRepository.FindAll(x => x.Hide_Rack == null || x.Hide_Rack == String.Empty)
                .Select(x => new { Area_ID = x.Area_ID, Area_Short_Title = x.Area_Short_Title }).ToListAsync();
            var carton_LocatList = await _wMSF_Carton_LocatRepository.FindAll(x => x.Status_Type == "Y" && x.Warehouse_ID == warehouse).ToListAsync();
            var locations = await _wMS_LocationRepository.FindAll(x => x.Status_Type == "Y").ToListAsync();
            var data = (from a in carton_LocatList
                        join b in locations
                        on a.Location_ID.Trim() equals b.Location_ID.Trim()
                        join c in AreaShows on b.Area_ID.Trim() equals c.Area_ID.Trim()
                        where a.Warehouse_ID == b.Warehouse_ID
                        select new RackPairsDto()
                        {
                            Area_ID = c.Area_ID,
                            Area_Short_Title = c.Area_Short_Title,
                            Area_Name = b.Area_Name.Trim(),
                            Pairs_Subtotal = a.Carton_Pairs
                        }).ToList();
            var result = data.GroupBy(x => x.Area_ID).Select(y => new RackPairsDto()
            {
                Area_ID = y.First().Area_ID,
                Area_Number = ConvertUtility.ConvertString(y.First().Area_ID),
                Area_Name = y.First().Area_Name,
                Area_Short_Title = y.First().Area_Short_Title,
                Pairs_Subtotal = y.Sum(cl => cl.Pairs_Subtotal)
            }).ToList();
            var rackAll = await _wMSF_Rack_AreaRepository.FindAll().Select(x => x.Area_ID.Trim()).Distinct().ToListAsync();
            var AreaShowsConvert = AreaShows.Select(x => x.Area_ID.Trim()).ToList();
            foreach (var item in rackAll)
            {
                var rackModel = result.Where(x => x.Area_ID.Trim() == item.Trim()).FirstOrDefault();
                // Nếu List Rack Setting cần show ra mà chứa rackItem và ở trong result lại không có.
                // Thì vẫn show ra nhưng với giá trị  = 0;
                if (AreaShowsConvert.Contains(item.Trim()) && rackModel == null)
                {
                    var RackFind = await _wMSF_Rack_AreaRepository.FindAll(x => x.Area_ID.Trim() == item.Trim()).FirstOrDefaultAsync();
                    var rackItem = new RackPairsDto()
                    {
                        Area_ID = item,
                        Area_Name = RackFind.Area_Name,
                        Area_Number = ConvertUtility.ConvertString(item),
                        Area_Short_Title = RackFind.Area_Short_Title,
                        Pairs_Subtotal = 0,
                    };
                    result.Add(rackItem);
                }
            }
            result = result.OrderBy(x => x.Area_Number).ToList();
            return result;
        }
    }
}