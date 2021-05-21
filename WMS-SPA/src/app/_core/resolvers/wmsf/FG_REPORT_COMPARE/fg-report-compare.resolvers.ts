import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { of } from "rxjs";
import { catchError } from "rxjs/operators";
import { WMSF_FG_CompareReport } from "../../../models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport";
import { FgReportCompareService } from "../../../services/wmsf/FG_REPORT_COMPARE/fg-report-compare.service";
import { Pagination, PaginationResult } from "../../../utilities/pagination";

@Injectable()
export class FgReportCompareResolver implements Resolve<PaginationResult<WMSF_FG_CompareReport>> {
    reportTime = "2021-05-20";

    pagination: Pagination = {
        currentPage: 1,
        pageSize: 10,
    } as Pagination;

    constructor(
        private fgReportCompareService: FgReportCompareService,
        //private router: Router,
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.fgReportCompareService.getAll(this.reportTime, this.pagination).pipe(
            catchError(() => {
                return of(null);
            })
        );
    }
}