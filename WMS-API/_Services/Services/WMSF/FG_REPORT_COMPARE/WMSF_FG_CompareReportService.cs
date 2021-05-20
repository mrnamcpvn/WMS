using System.Threading;


using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using WMS_API._Repositories.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API._Services.Interfaces.WMSF.FG_REPORT_COMPARE;
using WMS_API.Dtos.WMSF.FG_REPORT_COMPARE;
using WMS_API.Helpers.Params;
using WMS_API.Helpers.Utilities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WMS_API.Models.WMSF.FG_REPORT_COMPARE;

namespace WMS_API._Services.Services.WMSF.FG_REPORT_COMPARE
{
    public class WMSF_FG_CompareReportService : IWMSF_FG_CompareReportService
    {
        private readonly IWMSF_FG_CompareReportRepository _wMSF_FG_CompareReportRepository;
        private readonly IFRI_PORepository _fRI_PORepository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WMSF_FG_CompareReportService(
            IWMSF_FG_CompareReportRepository wMSF_FG_CompareReportRepository,
            IFRI_PORepository fRI_PORepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            MapperConfiguration configuration)
        {
            _wMSF_FG_CompareReportRepository = wMSF_FG_CompareReportRepository;
            _fRI_PORepository = fRI_PORepository;
            _mapper = mapper;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PageListUtility<WMSF_FG_CompareReportDto>> GetAll(string reportTime, PaginationParams pagination)
        {
            var reportTimeConvert = Convert.ToDateTime(reportTime);
            var dataFGIN_ReportCompare = await _wMSF_FG_CompareReportRepository.FindAll(x => x.Closing_Date == reportTimeConvert).ToListAsync();
            var cdrNo = dataFGIN_ReportCompare.Select(k => k.Cdr_No.Trim()).ToList();
            var dataFRI_PO = await _fRI_PORepository.FindAll(x => cdrNo.Contains(x.PO.Trim())).ToListAsync();
            var data = dataFGIN_ReportCompare
                .Join(
                    dataFRI_PO,
                    w => w.Cdr_No.Trim(),
                    e => e.PO.Trim(),
                    (w, e) => new { CompareReport = w, FRI_PO = e })
                .Select(x => new WMSF_FG_CompareReportDto
                {
                    Status = x.CompareReport.Order_Status,
                    Cdr_No = x.CompareReport.Cdr_No,
                    Model_Name = x.FRI_PO.Model_Name,
                    Article = x.FRI_PO.Article,
                    Location_ID = x.CompareReport.Location_ID,
                    PO_Locat_Qty = x.CompareReport.PO_WMS_Qty,
                    PO_ERP_Qty = x.CompareReport.PO_ERP_Qty,
                    Balance = x.CompareReport.PO_WMS_Qty - x.CompareReport.PO_ERP_Qty,
                    Accuracy = (x.CompareReport.PO_WMS_Qty - x.CompareReport.PO_ERP_Qty) == 0 ? 1 : 0
                }).OrderByDescending(x => x.Balance).ToList();

            var result = PageListUtility<WMSF_FG_CompareReportDto>.PageList(data, pagination.PageNumber, pagination.PageSize);
            return result;
        }

        public async Task<List<FRI_PODto>> ExportExcelByRack(string reportTime)
        {
            DateTime reportTimeConvert = Convert.ToDateTime(reportTime);
            var dataFGIN_ReportCompare = await _wMSF_FG_CompareReportRepository.FindAll(x => x.Closing_Date == reportTimeConvert).ToListAsync();
            var cdrNo = dataFGIN_ReportCompare.Select(k => k.Cdr_No.Trim()).ToList();
            var dataFRI_PO = await _fRI_PORepository.FindAll(x => cdrNo.Contains(x.PO.Trim())).ToListAsync();
            var data = dataFGIN_ReportCompare
                .Join(
                    dataFRI_PO,
                    w => w.Cdr_No.Trim(),
                    e => e.PO.Trim(),
                    (w, e) => new { CompareReport = w, FRI_PO = e })
                .Select(x => new FRI_PODto
                {
                    Status = x.CompareReport.Order_Status,
                    Cdr_No = x.CompareReport.Cdr_No,
                    Model_Name = x.FRI_PO.Model_Name,
                    Article = x.FRI_PO.Article,
                    Location_ID = x.CompareReport.Location_ID.Trim() == "Other" ? "ZZZZZZ" : x.CompareReport.Location_ID,
                    PO_Locat_Qty = x.CompareReport.PO_ERP_Qty,
                    PO_ERP_Qty = x.CompareReport.PO_ERP_Qty,
                    Balance = x.CompareReport.PO_WMS_Qty - x.CompareReport.PO_ERP_Qty,
                    Accuracy = (x.CompareReport.PO_WMS_Qty - x.CompareReport.PO_ERP_Qty) == 0 ? 1 : 0
                }).OrderByDescending(x => x.Balance).ToList();

            data = data.OrderBy(x => x.Cdr_No).ThenBy(x => x.Location_ID).ToList();
            foreach (var item in data)
            {
                if (item.Location_ID == "ZZZZZZ")
                {
                    item.Location_ID = "Other";
                }
                if (item.Status == " ")
                {
                    item.Status = "Unship";
                }
                else if (item.Status == "Y")
                {
                    item.Status = "Close";
                }
                else if (item.Status == "P")
                {
                    item.Status = "Partial";
                }
                else if (item.Status == "D")
                {
                    item.Status = "Split";
                }
                else if (item.Status == "C")
                {
                    item.Status = "Cancel";
                }
            }
            return data;
        }

        public async Task<List<WMSF_FG_CompareReportDto>> ExportExcelByPO(string reportTime)
        {
            DateTime reportTimeConvert = Convert.ToDateTime(reportTime);
            var dataFGIN_ReportCompare = await _wMSF_FG_CompareReportRepository.FindAll(x => x.Closing_Date == reportTimeConvert).ToListAsync();
            var cdrNo = dataFGIN_ReportCompare.Select(k => k.Cdr_No.Trim()).ToList();
            var dataFRI_PO = await _fRI_PORepository.FindAll(x => cdrNo.Contains(x.PO.Trim())).ToListAsync();
            var data = dataFGIN_ReportCompare
                .Join(
                    dataFRI_PO,
                    w => w.Cdr_No.Trim(),
                    e => e.PO.Trim(),
                    (w, e) => new { CompareReport = w, FRI_PO = e })
                .Select(x => new WMSF_FG_CompareReportDto
                {
                    Status = x.CompareReport.Order_Status,
                    Cdr_No = x.CompareReport.Cdr_No,
                    Model_Name = x.FRI_PO.Model_Name,
                    Article = x.FRI_PO.Article,
                    PO_Locat_Qty = x.CompareReport.PO_ERP_Qty,
                    PO_ERP_Qty = x.CompareReport.PO_ERP_Qty,
                    Balance = x.CompareReport.PO_WMS_Qty - x.CompareReport.PO_ERP_Qty,
                }).OrderByDescending(x => x.Balance).ToList();
            foreach (var item in data)
            {
                if (item.Status == " ")
                {
                    item.Status = "Unship";
                }
                else if (item.Status == "Y")
                {
                    item.Status = "Close";
                }
                else if (item.Status == "P")
                {
                    item.Status = "Partial";
                }
                else if (item.Status == "D")
                {
                    item.Status = "Split";
                }
                else if (item.Status == "C")
                {
                    item.Status = "Cancel";
                }
            }
            return data;
        }
    }

}