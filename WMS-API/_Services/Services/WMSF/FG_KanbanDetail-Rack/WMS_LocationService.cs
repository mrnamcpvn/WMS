using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using WMS_API.Helpers.Utilities;
using WMS_API._Services.Interface;
using System.Collections.Generic;
using WMS_API.Dtos;
using WMS_API._Repositories.Interfaces;
using WMS_API.Helpers.Params;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using Machine_API.Data;
using WMS_API.Data;
using WMS_API.ViewModels;

namespace WMS_API._Services.Services
{
    public class WMS_LocationService : IWMS_LocationService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private OperationResult operationResult;
        private readonly IWMS_LocationRepository _wMS_LocationRepository;
        private IConfiguration _configuration;
        private DataContext _dataContext;
        public WMS_LocationService(IMapper mapper, MapperConfiguration mapperConfiguration, DataContext dataContext, IWMS_LocationRepository wMS_LocationRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
            _wMS_LocationRepository = wMS_LocationRepository;
            _configuration = configuration;
            _dataContext = dataContext;
        }

        public async Task<List<SelectOptionsDto>> GetListWarehouse()
        {
            var result = _wMS_LocationRepository.FindAll().GroupBy(x => new { x.Warehouse_ID, x.Warehouse_Name }).Select(g => new SelectOptionsDto
            {
                Value = g.Key.Warehouse_ID,
                Label = g.Key.Warehouse_Name.Trim()
            }).ToListAsync();
            return await result;
        }
        public async Task<List<SelectOptionsDto>> GetListBuilding()
        {
            var result = _wMS_LocationRepository.FindAll().GroupBy(x => new { x.Building_ID, x.Building_Name }).Select(g => new SelectOptionsDto
            {
                Value = g.Key.Building_ID,
                Label = g.Key.Building_Name.Trim()
            }).ToListAsync();
            return await result;
        }
        public async Task<List<SelectOptionsDto>> GetListFloor()
        {
            var result = _wMS_LocationRepository.FindAll().GroupBy(x => new { x.Floor_ID, x.Floor_Name }).Select(g => new SelectOptionsDto
            {
                Value = g.Key.Floor_ID,
                Label = g.Key.Floor_Name.Trim()
            }).ToListAsync();
            return await result;
        }

        public async Task<List<SelectOptionsDto>> GetListArea()
        {
            var result = _wMS_LocationRepository.FindAll().GroupBy(x => new { x.Area_ID, x.Area_Name }).Select(g => new SelectOptionsDto
            {
                Value = g.Key.Area_ID,
                Label = g.Key.Area_Name.Trim()
            }).ToListAsync();
            return await result;
        }

        public async Task<PageListUtility<WMS_LocationViewDto>> SearchData(PaginationParams pagination, SearchParam searchParam)
        {
            searchParam.wareHouseId = searchParam.wareHouseId.Trim();
            searchParam.buildingId = searchParam.buildingId.Trim();
            searchParam.floorId = searchParam.floorId.Trim();
            searchParam.areaId = searchParam.areaId.Trim();
            searchParam.rackNo = searchParam.rackNo.Trim();
            searchParam.poNo = searchParam.poNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();
            data = data.OrderBy(x => x.Comfirmed_Date).ToList();

            if (searchParam.wareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == searchParam.wareHouseId).ToList();
            }
            if (searchParam.buildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == searchParam.buildingId).ToList();
            }
            if (searchParam.floorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == searchParam.floorId).ToList();
            }
            if (searchParam.areaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == searchParam.areaId).ToList();
            }
            if (searchParam.rackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(searchParam.rackNo.ToLower())).ToList();
            }
            if (searchParam.poNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(searchParam.poNo.ToLower())).ToList();
            }

            if (searchParam.dateType != string.Empty)
            {
                //Search by date
                DateTime formatfromDate = Convert.ToDateTime(searchParam.fromDate + " 00:00");
                DateTime formattoDate = Convert.ToDateTime(searchParam.toDate + " 23:59");
                if (searchParam.fromDate != null && searchParam.toDate == null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? x.Comfirmed_Date >= formatfromDate :
                                           searchParam.dateType == "export_date" ? x.Plan_Ship_Date >= formatfromDate :
                                           x.Real_Finish_Date >= formatfromDate).ToList();
                }
                if (searchParam.fromDate == null && searchParam.toDate != null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? x.Comfirmed_Date <= formattoDate :
                                          searchParam.dateType == "export_date" ? x.Plan_Ship_Date <= formattoDate :
                                          x.Real_Finish_Date <= formattoDate).ToList();
                }
                if (searchParam.fromDate != null && searchParam.toDate != null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? (x.Comfirmed_Date >= formatfromDate && x.Comfirmed_Date <= formattoDate) :
                                          searchParam.dateType == "export_date" ? (x.Plan_Ship_Date >= formatfromDate && x.Plan_Ship_Date <= formattoDate) :
                                          (x.Real_Finish_Date >= formatfromDate && x.Real_Finish_Date <= formattoDate)).ToList();
                }
            }

            return PageListUtility<WMS_LocationViewDto>.PageList(data, pagination.PageNumber, pagination.PageSize);
        }

         public async Task<List<WMS_LocationViewDto>> SearchDataNoPagintion(SearchParam searchParam)
        {
            searchParam.wareHouseId = searchParam.wareHouseId.Trim();
            searchParam.buildingId = searchParam.buildingId.Trim();
            searchParam.floorId = searchParam.floorId.Trim();
            searchParam.areaId = searchParam.areaId.Trim();
            searchParam.rackNo = searchParam.rackNo.Trim();
            searchParam.poNo = searchParam.poNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();
            data = data.OrderBy(x => x.Comfirmed_Date).ToList();

            if (searchParam.wareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == searchParam.wareHouseId).ToList();
            }
            if (searchParam.buildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == searchParam.buildingId).ToList();
            }
            if (searchParam.floorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == searchParam.floorId).ToList();
            }
            if (searchParam.areaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == searchParam.areaId).ToList();
            }
            if (searchParam.rackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(searchParam.rackNo.ToLower())).ToList();
            }
            if (searchParam.poNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(searchParam.poNo.ToLower())).ToList();
            }

            if (searchParam.dateType != string.Empty)
            {
                //Search by date
                DateTime formatfromDate = Convert.ToDateTime(searchParam.fromDate + " 00:00");
                DateTime formattoDate = Convert.ToDateTime(searchParam.toDate + " 23:59");
                if (searchParam.fromDate != null && searchParam.toDate == null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? x.Comfirmed_Date >= formatfromDate :
                                           searchParam.dateType == "export_date" ? x.Plan_Ship_Date >= formatfromDate :
                                           x.Real_Finish_Date >= formatfromDate).ToList();
                }
                if (searchParam.fromDate == null && searchParam.toDate != null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? x.Comfirmed_Date <= formattoDate :
                                          searchParam.dateType == "export_date" ? x.Plan_Ship_Date <= formattoDate :
                                          x.Real_Finish_Date <= formattoDate).ToList();
                }
                if (searchParam.fromDate != null && searchParam.toDate != null)
                {
                    data = data.Where(x => searchParam.dateType == "cfm_date" ? (x.Comfirmed_Date >= formatfromDate && x.Comfirmed_Date <= formattoDate) :
                                          searchParam.dateType == "export_date" ? (x.Plan_Ship_Date >= formatfromDate && x.Plan_Ship_Date <= formattoDate) :
                                          (x.Real_Finish_Date >= formatfromDate && x.Real_Finish_Date <= formattoDate)).ToList();
                }
            }

            return data;
        }
    }
}