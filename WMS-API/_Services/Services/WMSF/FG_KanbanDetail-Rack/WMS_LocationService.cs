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

        public async Task<PageListUtility<WMS_LocationViewDto>> SearchData(PaginationParams pagination, LocationParamDTO locationParamDTO)
        {
            locationParamDTO.SearchParam.wareHouseId = locationParamDTO.SearchParam.wareHouseId.Trim();
            locationParamDTO.SearchParam.buildingId = locationParamDTO.SearchParam.buildingId.Trim();
            locationParamDTO.SearchParam.floorId = locationParamDTO.SearchParam.floorId.Trim();
            locationParamDTO.SearchParam.areaId = locationParamDTO.SearchParam.areaId.Trim();
            locationParamDTO.SearchParam.rackNo = locationParamDTO.SearchParam.rackNo.Trim();
            locationParamDTO.SearchParam.poNo = locationParamDTO.SearchParam.poNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();
            

            if (locationParamDTO.SearchParam.wareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == locationParamDTO.SearchParam.wareHouseId).ToList();
            }
            if (locationParamDTO.SearchParam.buildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == locationParamDTO.SearchParam.buildingId).ToList();
            }
            if (locationParamDTO.SearchParam.floorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == locationParamDTO.SearchParam.floorId).ToList();
            }
            if (locationParamDTO.SearchParam.areaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == locationParamDTO.SearchParam.areaId).ToList();
            }
            if (locationParamDTO.SearchParam.rackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(locationParamDTO.SearchParam.rackNo.ToLower())).ToList();
            }
            if (locationParamDTO.SearchParam.poNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(locationParamDTO.SearchParam.poNo.ToLower())).ToList();
            }

            if (locationParamDTO.SearchParam.dateType != string.Empty)
            {
                //Search by date
                DateTime formatfromDate = Convert.ToDateTime(locationParamDTO.SearchParam.fromDate + " 00:00");
                DateTime formattoDate = Convert.ToDateTime(locationParamDTO.SearchParam.toDate + " 23:59");
                if (locationParamDTO.SearchParam.fromDate != null && locationParamDTO.SearchParam.toDate == null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? x.Comfirmed_Date >= formatfromDate :
                                           locationParamDTO.SearchParam.dateType == "export_date" ? x.Plan_Ship_Date >= formatfromDate :
                                           x.Real_Finish_Date >= formatfromDate).ToList();
                }
                if (locationParamDTO.SearchParam.fromDate == null && locationParamDTO.SearchParam.toDate != null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? x.Comfirmed_Date <= formattoDate :
                                          locationParamDTO.SearchParam.dateType == "export_date" ? x.Plan_Ship_Date <= formattoDate :
                                          x.Real_Finish_Date <= formattoDate).ToList();
                }
                if (locationParamDTO.SearchParam.fromDate != null && locationParamDTO.SearchParam.toDate != null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? (x.Comfirmed_Date >= formatfromDate && x.Comfirmed_Date <= formattoDate) :
                                          locationParamDTO.SearchParam.dateType == "export_date" ? (x.Plan_Ship_Date >= formatfromDate && x.Plan_Ship_Date <= formattoDate) :
                                          (x.Real_Finish_Date >= formatfromDate && x.Real_Finish_Date <= formattoDate)).ToList();
                }
            }

            data = data.OrderBy(x => x.Comfirmed_Date).ToList();
            foreach (var sort in locationParamDTO.SortParams)
            {
                switch(sort.SortColumn)
                {
                    case nameof(WMS_LocationViewDto.Comfirmed_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Comfirmed_Date).ToList() : data.OrderByDescending(x => x.Comfirmed_Date).ToList();
                        break;

                    case nameof(WMS_LocationViewDto.Plan_Ship_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Plan_Ship_Date).ToList() : data.OrderByDescending(x => x.Plan_Ship_Date).ToList();
                        break;

                    case nameof(WMS_LocationViewDto.Real_Finish_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Real_Finish_Date).ToList() : data.OrderByDescending(x => x.Real_Finish_Date).ToList();
                        break;
                    
                    case nameof(WMS_LocationViewDto.Order_ID) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Order_ID).ToList() : data.OrderByDescending(x => x.Order_ID).ToList();
                        break;

                    default: break;
                }
            }

            return PageListUtility<WMS_LocationViewDto>.PageList(data, pagination.PageNumber, pagination.PageSize);
        }

         public async Task<List<WMS_LocationViewDto>> SearchDataNoPagintion(LocationParamDTO locationParamDTO)
        {
            locationParamDTO.SearchParam.wareHouseId = locationParamDTO.SearchParam.wareHouseId.Trim();
            locationParamDTO.SearchParam.buildingId = locationParamDTO.SearchParam.buildingId.Trim();
            locationParamDTO.SearchParam.floorId = locationParamDTO.SearchParam.floorId.Trim();
            locationParamDTO.SearchParam.areaId = locationParamDTO.SearchParam.areaId.Trim();
            locationParamDTO.SearchParam.rackNo = locationParamDTO.SearchParam.rackNo.Trim();
            locationParamDTO.SearchParam.poNo = locationParamDTO.SearchParam.poNo.Trim();

            // lấy data từ stored procedure
            var data = await _dataContext.WMS_LocationView.FromSqlRaw("EXEC PRD_LOCATION_LIST").ToListAsync();

            if (locationParamDTO.SearchParam.wareHouseId != string.Empty)
            {
                data = data.Where(x => x.Warehouse_Id == locationParamDTO.SearchParam.wareHouseId).ToList();
            }
            if (locationParamDTO.SearchParam.buildingId != string.Empty)
            {
                data = data.Where(x => x.Building_Id == locationParamDTO.SearchParam.buildingId).ToList();
            }
            if (locationParamDTO.SearchParam.floorId != string.Empty)
            {
                data = data.Where(x => x.Floor_Id == locationParamDTO.SearchParam.floorId).ToList();
            }
            if (locationParamDTO.SearchParam.areaId != string.Empty)
            {
                data = data.Where(x => x.Area_ID == locationParamDTO.SearchParam.areaId).ToList();
            }
            if (locationParamDTO.SearchParam.rackNo != string.Empty)
            {
                data = data.Where(x => x.Location_ID.ToLower().Contains(locationParamDTO.SearchParam.rackNo.ToLower())).ToList();
            }
            if (locationParamDTO.SearchParam.poNo != string.Empty)
            {
                data = data.Where(x => x.Order_ID.ToLower().Contains(locationParamDTO.SearchParam.poNo.ToLower())).ToList();
            }

            if (locationParamDTO.SearchParam.dateType != string.Empty)
            {
                //Search by date
                DateTime formatfromDate = Convert.ToDateTime(locationParamDTO.SearchParam.fromDate + " 00:00");
                DateTime formattoDate = Convert.ToDateTime(locationParamDTO.SearchParam.toDate + " 23:59");
                if (locationParamDTO.SearchParam.fromDate != null && locationParamDTO.SearchParam.toDate == null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? x.Comfirmed_Date >= formatfromDate :
                                           locationParamDTO.SearchParam.dateType == "export_date" ? x.Plan_Ship_Date >= formatfromDate :
                                           x.Real_Finish_Date >= formatfromDate).ToList();
                }
                if (locationParamDTO.SearchParam.fromDate == null && locationParamDTO.SearchParam.toDate != null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? x.Comfirmed_Date <= formattoDate :
                                          locationParamDTO.SearchParam.dateType == "export_date" ? x.Plan_Ship_Date <= formattoDate :
                                          x.Real_Finish_Date <= formattoDate).ToList();
                }
                if (locationParamDTO.SearchParam.fromDate != null && locationParamDTO.SearchParam.toDate != null)
                {
                    data = data.Where(x => locationParamDTO.SearchParam.dateType == "cfm_date" ? (x.Comfirmed_Date >= formatfromDate && x.Comfirmed_Date <= formattoDate) :
                                          locationParamDTO.SearchParam.dateType == "export_date" ? (x.Plan_Ship_Date >= formatfromDate && x.Plan_Ship_Date <= formattoDate) :
                                          (x.Real_Finish_Date >= formatfromDate && x.Real_Finish_Date <= formattoDate)).ToList();
                }
            }

            data = data.OrderBy(x => x.Comfirmed_Date)
                        .ThenBy(x => x.Plan_Ship_Date)
                        .ThenBy(x => x.Real_Finish_Date)
                        .ThenBy(x => x.Order_ID).ToList();
            foreach (var sort in locationParamDTO.SortParams)
            {
                switch(sort.SortColumn)
                {
                    case nameof(WMS_LocationViewDto.Comfirmed_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Comfirmed_Date).ToList() : data.OrderByDescending(x => x.Comfirmed_Date).ToList();
                        break;

                    case nameof(WMS_LocationViewDto.Plan_Ship_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Plan_Ship_Date).ToList() : data.OrderByDescending(x => x.Plan_Ship_Date).ToList();
                        break;

                    case nameof(WMS_LocationViewDto.Real_Finish_Date) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Real_Finish_Date).ToList() : data.OrderByDescending(x => x.Real_Finish_Date).ToList();
                        break;
                    
                    case nameof(WMS_LocationViewDto.Order_ID) :
                        data = sort.SortBy == SortBy.Asc ? data.OrderBy(x => x.Order_ID).ToList() : data.OrderByDescending(x => x.Order_ID).ToList();
                        break;

                    default: break;
                }
            }

            return data;
        }
    }
}