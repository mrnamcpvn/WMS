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
using System;
using WMS_API.Models;
using Microsoft.Extensions.Configuration;

namespace WMS_API._Services.Services
{
    public class WMSF_Carton_LocatService : IWMSF_Carton_LocatService
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private OperationResult operationResult;
        private readonly IWMSF_Carton_LocatRepository _wMSF_Carton_LocatRepository;
        private readonly IWMSF_Rack_AreaRepository _wMSF_Rack_AreaRepository;
        private readonly IWMS_LocationRepository _wMS_LocationRepository;
        private readonly IConfiguration _configuration;
        public WMSF_Carton_LocatService(IMapper mapper, MapperConfiguration mapperConfiguration, IConfiguration configuration, IWMSF_Rack_AreaRepository wMSF_Rack_AreaRepository, IWMSF_Carton_LocatRepository wMSF_Carton_LocatRepository, IWMS_LocationRepository wMS_LocationRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
            _wMS_LocationRepository = wMS_LocationRepository;
            _wMSF_Carton_LocatRepository = wMSF_Carton_LocatRepository;
            _wMSF_Rack_AreaRepository = wMSF_Rack_AreaRepository;
        }

        public async Task<List<ChartModelDto>> LoadDataChart()
        {
            var areaListCheck = _wMSF_Rack_AreaRepository.FindAll().Where(x => x.Warehouse_A != null && x.Warehouse_A == "Y").Select(x => x.Area_ID).ToList();
            var dataCartonLocat = new List<WMSF_Carton_Locat>(_wMSF_Carton_LocatRepository.FindAll().Where(x => x.Status_Type == "Y" || x.Status_Type == "y"));
            var dataLocation = new List<WMS_Location>(_wMS_LocationRepository.FindAll().Where(x => x.Status_Type == "Y" || x.Status_Type == "y"));

            var data = (from b in dataLocation
                        join a in dataCartonLocat on b.Factory_ID equals a.Factory_ID into joined
                        from j in joined.DefaultIfEmpty()
                        where b.Warehouse_ID == j.Warehouse_ID && b.Location_ID == j.Location_ID
                        select new LocationViewDto
                        {
                            Factory_ID = j == null ? default : j.Factory_ID,
                            Location_ID = j == null ? default : j.Location_ID,
                            Barcode = j == null ? default : j.Barcode,
                            Order_ID = j == null ? default : j.Order_ID,
                            Carton_Pairs = j == null ? 0 : j.Carton_Pairs,
                            Location_Name = b.Location_Name,
                            Warehouse_Name = b.Warehouse_Name,
                            Warehouse_Id = b.Warehouse_ID,
                            Building_Name = b.Building_Name,
                            Building_Id = b.Building_ID,
                            Floor_Name = b.Floor_Name,
                            Floor_Id = b.Floor_ID,
                            Area_ID = b.Area_ID,
                            Area_Name = b.Area_Name,
                            CBM = b.CBM,
                            Volume = j == null ? 0 : j.Volume
                        }).ToList();

            var classTextTitle = "classTextTitle";
            var classTextData = "classTextData";
            var listWareHouse = dataLocation.GroupBy(w => w.Warehouse_ID).Select(w => new ChartModelDto
            {
                factoryName = _configuration.GetSection("AppSettings:FactoryName").Value,
                id = w.Key,
                className = "class1",
                title =
                         "<span class=" + classTextTitle + ">PRS</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID && areaListCheck.Contains(x.Area_ID)).Sum(x => x.Carton_Pairs)) + "</span> " + "\n" +
                         "<span class=" + classTextTitle + ">CTN</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID).Count(x => x.Barcode != string.Empty)) + "</span> " + "\n" +
                         "<span class=" + classTextTitle + ">STO</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID).Sum(x => x.Volume)) + "</span> " + "\n" +
                         "<span class=" + classTextTitle + ">BAL</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", (w.Sum(x => x.CBM) - data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID).Sum(x => x.Volume))) + "</span> ",
                name = w.FirstOrDefault().Warehouse_ID.Trim(),

                childs = dataLocation.Where(b => b.Warehouse_ID == w.FirstOrDefault().Warehouse_ID).GroupBy(b => b.Building_ID).Select(b => new ChartModelDto
                {
                    id = b.Key,
                    className = "class2",
                    title =
                            "<span class=" + classTextTitle + ">PRS</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                        x.Building_Id == b.FirstOrDefault().Building_ID && areaListCheck.Contains(x.Area_ID)).Sum(x => x.Carton_Pairs)) + "</span> " + "\n" +
                            "<span class=" + classTextTitle + ">CTN</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                        x.Building_Id == b.FirstOrDefault().Building_ID).Count(x => x.Barcode != string.Empty)) + "</span> " + "\n" +
                              "<span class=" + classTextTitle + ">STO</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                        x.Building_Id == b.FirstOrDefault().Building_ID).Sum(x => x.Volume)) + "</span> " + "\n" +
                               "<span class=" + classTextTitle + ">BAL</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", (b.Sum(x => x.CBM) - data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                       x.Building_Id == b.FirstOrDefault().Building_ID).Sum(x => x.Volume))) + "</span> ",
                    name = b.FirstOrDefault().Building_Name.Trim(),

                    childs = dataLocation.Where(f => f.Warehouse_ID == w.FirstOrDefault().Warehouse_ID &&
                    f.Building_ID == b.FirstOrDefault().Building_ID).GroupBy(f => f.Floor_ID).Select(f => new ChartModelDto
                    {
                        id = f.Key,
                        className = "class3",
                        title =
                                  "<span class=" + classTextTitle + ">PRS</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                x.Floor_Id == f.FirstOrDefault().Floor_ID && areaListCheck.Contains(x.Area_ID)).Sum(x => x.Carton_Pairs)) + "</span> " + "\n" +
                                  "<span class=" + classTextTitle + ">CTN</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                x.Floor_Id == f.FirstOrDefault().Floor_ID).Count(x => x.Barcode != string.Empty)) + "</span> " + "\n" +
                                  "<span class=" + classTextTitle + ">STO</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                x.Floor_Id == f.FirstOrDefault().Floor_ID).Sum(x => x.Volume)) + "</span> " + "\n" +
                                   "<span class=" + classTextTitle + ">BAL</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", (f.Sum(x => x.CBM)
                                                - data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                               x.Building_Id == b.FirstOrDefault().Building_ID &&
                                               x.Floor_Id == f.FirstOrDefault().Floor_ID).Sum(x => x.Volume))) + "</span> ",
                        name = f.FirstOrDefault().Floor_Name.Trim(),

                        childs = dataLocation.Where(a => a.Warehouse_ID == w.FirstOrDefault().Warehouse_ID &&
                                    a.Building_ID == b.FirstOrDefault().Building_ID &&
                                    a.Floor_ID == f.FirstOrDefault().Floor_ID).GroupBy(a => a.Area_ID).Select(a => new ChartModelDto
                                    {
                                        id = a.Key,
                                        name = "<span class='text-capitalize'>" + (a.FirstOrDefault().Area_Name.Trim().Length > 13 ? a.FirstOrDefault().Area_Name.Trim().Substring(0, 13) + "..." : a.FirstOrDefault().Area_Name.Trim()) + "</span>",
                                        className = "class4",
                                        title =
                                       "<span class=" + classTextTitle + ">PRS</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                        x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                        x.Floor_Id == f.FirstOrDefault().Floor_ID &&
                                                        x.Area_ID == a.FirstOrDefault().Area_ID && areaListCheck.Contains(x.Area_ID)).Sum(x => x.Carton_Pairs)) + "</span> " + "\n" +
                                           "<span class=" + classTextTitle + ">CTN</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                        x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                        x.Floor_Id == f.FirstOrDefault().Floor_ID &&
                                                        x.Area_ID == a.FirstOrDefault().Area_ID).Count(x => x.Barcode != string.Empty)) + "</span> " + "\n" +
                                        "<span class=" + classTextTitle + ">STO</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                        x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                        x.Floor_Id == f.FirstOrDefault().Floor_ID &&
                                                        x.Area_ID == a.FirstOrDefault().Area_ID).Sum(x => x.Volume)) + "</span> " + "\n" +
                                           //String.Format("{0:#,##0.##}",
                                           "<span class=" + classTextTitle + ">BAL</span>  " + "<span class=" + classTextData + ">" + String.Format("{0:#,##0}", (a.Sum(x => x.CBM)
                                                        - data.Where(x => x.Warehouse_Id == w.FirstOrDefault().Warehouse_ID &&
                                                         x.Building_Id == b.FirstOrDefault().Building_ID &&
                                                         x.Floor_Id == f.FirstOrDefault().Floor_ID &&
                                                         x.Area_ID == a.FirstOrDefault().Area_ID).Sum(x => x.Volume))) + "</span> ",
                                    }).ToList()
                    }).OrderBy(f => f.id).ToList()
                }).ToList()
            }).ToList();

            return listWareHouse;
        }
    }
}