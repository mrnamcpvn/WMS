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

        public async Task<PageListUtility<WMS_LocationViewDto>> SearchData(PaginationParams pagination, ObjectSearchDto objectSearch)
        {
            objectSearch.WareHouseId = objectSearch.WareHouseId.Trim();
            objectSearch.BuildingId = objectSearch.BuildingId.Trim();
            objectSearch.FloorId = objectSearch.FloorId.Trim();
            objectSearch.AreaId = objectSearch.AreaId.Trim();
            objectSearch.RackNo = objectSearch.RackNo.Trim();
            objectSearch.PoNo = objectSearch.PoNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();

            if (objectSearch.WareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == objectSearch.WareHouseId).ToList();
            }
            if (objectSearch.BuildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == objectSearch.BuildingId).ToList();
            }
            if (objectSearch.FloorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == objectSearch.FloorId).ToList();
            }
            if (objectSearch.AreaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == objectSearch.AreaId).ToList();
            }
            if (objectSearch.RackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(objectSearch.RackNo.ToLower())).ToList();
            }
            if (objectSearch.PoNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(objectSearch.PoNo.ToLower())).ToList();
            }

            if (objectSearch.DateType != string.Empty)
            {
                //Search by date
                DateTime formatFromDate = Convert.ToDateTime(objectSearch.FromDate + " 00:00");
                DateTime formatToDate = Convert.ToDateTime(objectSearch.ToDate + " 23:59");
                if (objectSearch.FromDate != string.Empty && objectSearch.ToDate == string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? x.Comfirmed_Date >= formatFromDate :
                                           objectSearch.DateType == "export_date" ? x.Plan_Ship_Date >= formatFromDate :
                                           x.Real_Finish_Date >= formatFromDate).ToList();
                }
                if (objectSearch.FromDate == string.Empty && objectSearch.ToDate != string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? x.Comfirmed_Date <= formatToDate :
                                          objectSearch.DateType == "export_date" ? x.Plan_Ship_Date <= formatToDate :
                                          x.Real_Finish_Date <= formatToDate).ToList();
                }
                if (objectSearch.FromDate != string.Empty && objectSearch.ToDate != string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? (x.Comfirmed_Date >= formatFromDate && x.Comfirmed_Date <= formatToDate) :
                                          objectSearch.DateType == "export_date" ? (x.Plan_Ship_Date >= formatFromDate && x.Plan_Ship_Date <= formatToDate) :
                                          (x.Real_Finish_Date >= formatFromDate && x.Real_Finish_Date <= formatToDate)).ToList();
                }
            }

            return PageListUtility<WMS_LocationViewDto>.PageList(data, pagination.PageNumber, pagination.PageSize);
        }

         public async Task<List<WMS_LocationViewDto>> SearchDataNoPagintion(ObjectSearchDto objectSearch)
        {
            objectSearch.WareHouseId = objectSearch.WareHouseId.Trim();
            objectSearch.BuildingId = objectSearch.BuildingId.Trim();
            objectSearch.FloorId = objectSearch.FloorId.Trim();
            objectSearch.AreaId = objectSearch.AreaId.Trim();
            objectSearch.RackNo = objectSearch.RackNo.Trim();
            objectSearch.PoNo = objectSearch.PoNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();

            if (objectSearch.WareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == objectSearch.WareHouseId).ToList();
            }
            if (objectSearch.BuildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == objectSearch.BuildingId).ToList();
            }
            if (objectSearch.FloorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == objectSearch.FloorId).ToList();
            }
            if (objectSearch.AreaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == objectSearch.AreaId).ToList();
            }
            if (objectSearch.RackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(objectSearch.RackNo.ToLower())).ToList();
            }
            if (objectSearch.PoNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(objectSearch.PoNo.ToLower())).ToList();
            }

            if (objectSearch.DateType != string.Empty)
            {
                //Search by date
                DateTime formatFromDate = Convert.ToDateTime(objectSearch.FromDate + " 00:00");
                DateTime formatToDate = Convert.ToDateTime(objectSearch.ToDate + " 23:59");
                if (objectSearch.FromDate != string.Empty && objectSearch.ToDate == string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? x.Comfirmed_Date >= formatFromDate :
                                           objectSearch.DateType == "export_date" ? x.Plan_Ship_Date >= formatFromDate :
                                           x.Real_Finish_Date >= formatFromDate).ToList();
                }
                if (objectSearch.FromDate == string.Empty && objectSearch.ToDate != string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? x.Comfirmed_Date <= formatToDate :
                                          objectSearch.DateType == "export_date" ? x.Plan_Ship_Date <= formatToDate :
                                          x.Real_Finish_Date <= formatToDate).ToList();
                }
                if (objectSearch.FromDate != string.Empty && objectSearch.ToDate != string.Empty)
                {
                    data = data.Where(x => objectSearch.DateType == "cfm_date" ? (x.Comfirmed_Date >= formatFromDate && x.Comfirmed_Date <= formatToDate) :
                                          objectSearch.DateType == "export_date" ? (x.Plan_Ship_Date >= formatFromDate && x.Plan_Ship_Date <= formatToDate) :
                                          (x.Real_Finish_Date >= formatFromDate && x.Real_Finish_Date <= formatToDate)).ToList();
                }
            }

            return data;
        }
    }
}