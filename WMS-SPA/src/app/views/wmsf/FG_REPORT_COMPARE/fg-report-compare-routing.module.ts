import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FgReportCompareResolver } from "../../../_core/resolvers/wmsf/FG_REPORT_COMPARE/fg-report-compare.resolvers";
import { CompareReportComponent } from "./compare-report/compare-report.component";

const routes: Routes = [
    {
        path: '',
        data: { title: 'CompareReport' },
        resolve: {
            data: FgReportCompareResolver
        },
        component: CompareReportComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class FgReportCompareRoutingModule { }