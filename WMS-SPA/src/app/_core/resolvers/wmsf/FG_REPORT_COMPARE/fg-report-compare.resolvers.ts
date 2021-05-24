import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, Router } from "@angular/router";
import { NgxSpinnerService } from "ngx-spinner";
import { of } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { WMSF_FG_CompareReport } from "../../../models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport";
import { FgReportCompareService } from "../../../services/wmsf/FG_REPORT_COMPARE/fg-report-compare.service";
import { Pagination, PaginationResult } from "../../../utilities/pagination";

@Injectable()
export class FgReportCompareResolver implements Resolve<PaginationResult<WMSF_FG_CompareReport>> {
    typeSort: string;
    date = new Date();
    reportTime = this.date.getFullYear() + '/' + (this.date.getMonth() + 1) + '/' + (this.date.getDate() - 1);
    pagination: Pagination = {
        currentPage: 1,
        pageSize: 10,
    } as Pagination;

    constructor(
        private fgReportCompareService: FgReportCompareService,
        private spinnerService: NgxSpinnerService,
        //private router: Router,
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        this.spinnerService.show();
        return this.fgReportCompareService.getAll(this.reportTime, this.typeSort, this.pagination).pipe(
            tap(() => this.spinnerService.hide()),
            catchError(() => {
                return of(null);
            })
        );
    }
}