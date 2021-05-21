using LinqKit;
using System.Collections.Generic;
using AutoMapper;
using WMS_API._Repositories.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API._Services.Interfaces.WMSF.FG_TrackingKanban_SortingKanban;
using WMS_API.Helpers.Params.WMSF.FG_TrackingKanban_SortingKanban;
using System;
using System.Linq;
using WMS_API.Helpers.Utilities;
using WMS_API.Dtos.WMSF.FG_TrackingKanban_SortingKanban;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_TrackingKanban_SortingKanban.CB_WMS;
using WMS_API.Helpers.Params;

namespace WMS_API._Services.Services.WMSF.FG_TrackingKanban_SortingKanban
{
    public class VW_FGIN_LOCAT_LISTService : IVW_FGIN_LOCAT_LISTService
    {

        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configMapper;
        private readonly IVW_FGIN_LOCAT_LISTRepository _vW_FGIN_LOCAT_LISTRepository;

        public VW_FGIN_LOCAT_LISTService(IVW_FGIN_LOCAT_LISTRepository vW_FGIN_LOCAT_LISTRepository, IMapper mapper, MapperConfiguration configMapper)
        {
            _vW_FGIN_LOCAT_LISTRepository = vW_FGIN_LOCAT_LISTRepository;
            _mapper = mapper;
            _configMapper = configMapper;
        }
        public async Task<List<VW_FGIN_LOCAT_LIST>> ExportExcel(SearchParams dataExport)
        {
            return await GetData(dataExport.deptId, dataExport.receivedTime, dataExport.optionData, dataExport.sortBy, dataExport.sortType);
        }

        public async Task<List<VW_FGIN_LOCAT_LIST>> GetData(string deptId, string receivedTime, string optionData, string sortBy, string sortType)
        {
            var pred = PredicateBuilder.New<VW_FGIN_LOCAT_LIST>(true);

            // Line
            if (!string.IsNullOrEmpty(deptId))
            {
                pred = pred.And(x => x.Dept_ID.Contains(deptId));
            }

            // Received Time
            DateTime startTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            if (receivedTime != string.Empty)
            {
                startTime = Convert.ToDateTime(receivedTime + " 00:00:00");
                endTime = Convert.ToDateTime(receivedTime + " 23:59:59");
            }
            pred.And(x => x.Up_UTC_Dat >= startTime && x.Up_UTC_Dat <= endTime);

            // completed、nocompleted、Manual Cancel dropdown list
            if (optionData == "completed")
            {
                pred = pred.And(x => x.In_UTC_Dat != null && x.Locat.Trim() != "Manual Cancel");
            }
            else if (optionData == "nocompleted")
            {
                pred = pred.And(x => x.In_UTC_Dat == null && x.Locat.Trim() != "Manual Cancel");
            }
            else if (optionData == "manual")
            {
                pred = pred.And(x => x.In_UTC_Dat == null && x.Locat.Trim() == "Manual Cancel");
            }
            var data = await _vW_FGIN_LOCAT_LISTRepository.FindAll(pred).OrderByDescending(x => x.Up_UTC_Dat).ToListAsync();

            if (sortBy != string.Empty && sortType != string.Empty)
            {
                if (sortBy == "transfer_form")
                {
                    if (sortType == "asc")
                        data = data.OrderBy(x => x.Up_Trno).ToList();
                    else
                        data = data.OrderByDescending(x => x.Up_Trno).ToList();
                }
                else if (sortBy == "received_time")
                {
                    if (sortType == "asc")
                        data = data.OrderBy(x => x.Up_UTC_Dat).ToList();
                    else
                        data = data.OrderByDescending(x => x.Up_UTC_Dat).ToList();
                }
                else if (sortBy == "completed_time")
                {
                    if (sortType == "asc")
                        data = data.OrderBy(x => x.In_UTC_Dat).ToList();
                    else
                        data = data.OrderByDescending(x => x.In_UTC_Dat).ToList();
                }
            }

            return data;
        }

        public async Task<VW_FGIN_LOCAT_LISTDto> SearchFginLocat(SearchParams searchParams, PaginationParams paginationParams)
        {
            var data = await GetData(searchParams.deptId, searchParams.receivedTime, searchParams.optionData, searchParams.sortBy, searchParams.sortType);

            decimal sumCartons = data.Sum(x => x.CTN_Qty ?? 0);
            decimal sumPairs = data.Sum(x => x.Qty ?? 0);
            decimal sumCBM = data.Sum(x => x.Meas);
            var result = PageListUtility<VW_FGIN_LOCAT_LIST>.PageList(data, paginationParams.PageNumber, paginationParams.PageSize);

            return new VW_FGIN_LOCAT_LISTDto
            {
                Dtos = result,
                sumCartons = sumCartons,
                sumCBM = sumCBM,
                sumPairs = sumPairs
            };
        }
    }
}