import { NgModule } from "@angular/core";
import { FgReportCompareRoutingModule } from "./fg-report-compare-routing.module";
import { FormsModule } from '@angular/forms';

import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FgReportCompareResolver } from "../../../_core/resolvers/wmsf/FG_REPORT_COMPARE/fg-report-compare.resolvers";
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [

    ],
    imports: [
        CommonModule,
        FormsModule,
        FgReportCompareRoutingModule,
        BsDatepickerModule.forRoot()
    ],
    providers: [
        FgReportCompareResolver
    ]
})
export class FgReportCompareModule { }
