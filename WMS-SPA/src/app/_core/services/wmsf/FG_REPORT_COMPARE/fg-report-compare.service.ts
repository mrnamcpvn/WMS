import { Injectable } from "@angular/core";
import { environment } from "../../../../../environments/environment";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from "../../../utilities/pagination";
import { WMSF_FG_CompareReport } from "../../../models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport";
import { FunctionUtility } from "../../../utilities/fucntion-utility";

@Injectable({
    providedIn: 'root'
})
export class FgReportCompareService {
    baseUrl = environment.apiUrl;
    constructor(
        private http: HttpClient,
        private fu: FunctionUtility
    ) { }

    getAll(reportTime: string, typeSort: string, pagination: Pagination) {
        let params = new HttpParams()
            .set('pageNumber', pagination.currentPage.toString())
            .set('pageSize', pagination.pageSize.toString())
            .set('reportTime', reportTime)
            .set('typeSort', typeSort);
        return this.http.get<PaginationResult<WMSF_FG_CompareReport>>(`${this.baseUrl}FGReportCompare/GetCompareByRack`, { params });
    }

    ExportExcelByRack(reportTime: string,) {
        let params = new HttpParams();
        params = params.append('reportTime', reportTime);
        return this.http.get(`${this.baseUrl}FGReportCompare/ExportExcelByRack`, { responseType: 'blob', params })
            .subscribe((result: Blob) => {
                const blob = new Blob([result]);
                const url = window.URL.createObjectURL(blob);
                const link = document.createElement('a');
                const currentTime = new Date();
                let nameString = "Report_Compare_By_Rack"
                let filename = nameString + '-' + this.fu.getStringDate(currentTime) + '.xlsx';
                link.href = url;
                link.setAttribute('download', filename);
                document.body.appendChild(link);
                link.click();
            })
    }

    ExportExcelCompare(reportTime: string,) {
        let params = new HttpParams();
        params = params.append('reportTime', reportTime);
        return this.http.get(`${this.baseUrl}FGReportCompare/ExportExcelByPO`, { responseType: 'blob', params })
            .subscribe((result: Blob) => {
                const blob = new Blob([result]);
                const url = window.URL.createObjectURL(blob);
                const link = document.createElement('a');
                const currentTime = new Date();
                let nameString = "Report_Compare_By_PO"
                let filename = nameString + '-' + this.fu.getStringDate(currentTime) + '.xlsx';
                link.href = url;
                link.setAttribute('download', filename);
                document.body.appendChild(link);
                link.click();
            })
    }
}