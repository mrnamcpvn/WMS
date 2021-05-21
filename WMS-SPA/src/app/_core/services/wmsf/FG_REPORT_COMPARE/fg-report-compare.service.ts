import { Injectable } from "@angular/core";
import { environment } from "../../../../../environments/environment";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from "../../../utilities/pagination";
import { WMSF_FG_CompareReport } from "../../../models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport";
import { map } from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})
export class FgReportCompareService {
    baseUrl = environment.apiUrl;
    constructor(
        private http: HttpClient,
    ) { }

    getAll(reportTime: string, pagination: Pagination) {
        let params = new HttpParams()
            .set('pageNumber', pagination.currentPage.toString())
            .set('pageSize', pagination.pageSize.toString())
            .set('reportTime', reportTime);
        return this.http.get<PaginationResult<WMSF_FG_CompareReport>>(`${this.baseUrl}FGReportCompare/GetCompareByRack`, { params });

    }
}