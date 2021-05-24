import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { FgReportCompareRoutingModule } from "./fg-report-compare-routing.module";
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FgReportCompareResolver } from "../../../_core/resolvers/wmsf/FG_REPORT_COMPARE/fg-report-compare.resolvers";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { CompareReportComponent } from "./compare-report/compare-report.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SnotifyModule } from "ng-snotify";
import { NgxSpinnerModule } from "ngx-spinner";

@NgModule({
    declarations: [
        CompareReportComponent
    ],
    imports: [
        HttpClientModule,
        CommonModule,
        FormsModule,
        FgReportCompareRoutingModule,
        BsDatepickerModule.forRoot(),
        PaginationModule.forRoot(),
        SnotifyModule,
        NgxSpinnerModule

    ],
    providers: [
        FgReportCompareResolver
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class FgReportCompareModule { }
