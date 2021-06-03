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
    report_time = " ";
    receivedDateTime = "";
    pagination: Pagination = {
        currentPage: 1,
        pageSize: 10,
    } as Pagination;

    constructor(
        private fgReportCompareService: FgReportCompareService,
        private spinnerService: NgxSpinnerService,
        //private router: Router,
    ) { }

    GetDateReport() {
        const today = new Date();
        if (today.getDate() != 1) {
            const date = today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + (today.getDate() - 1);
            this.report_time = date;
        }
        else if ((today.getMonth() + 1) == 1) {
            const date = (today.getFullYear() - 1) + '/' + '12' + '/' + '31';
            this.report_time = date
        }
        else if (today.getMonth() == 1 || today.getMonth() == 3 || today.getMonth() == 5 || today.getMonth() == 7 || today.getMonth() == 8 || today.getMonth() == 10) {
            const date = today.getFullYear() + '/' + (today.getMonth()) + '/' + '31';
            this.report_time = date
        }
        else if (today.getMonth() == 2 && (today.getFullYear() % 4) == 0) {
            const date = today.getFullYear() + '/' + (today.getMonth()) + '/' + '29';
            this.report_time = date
        }
        else if (today.getMonth() == 2 && (today.getFullYear() % 4) != 0) {
            const date = today.getFullYear() + '/' + (today.getMonth()) + '/' + '28';
            this.report_time = date
        }
        else {
            const date = today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + '30';
            this.report_time = date
        }
    }

    resolve(route: ActivatedRouteSnapshot) {
        this.GetDateReport();
        this.spinnerService.show();
        console.log(this.report_time)
        return this.fgReportCompareService.getAll(this.report_time, this.typeSort, this.pagination).pipe(
            tap(() => this.spinnerService.hide()),
            catchError(() => {
                return of(null);
            })
        );
    }
}