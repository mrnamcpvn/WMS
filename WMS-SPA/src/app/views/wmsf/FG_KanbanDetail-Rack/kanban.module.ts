import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgSelect2Module } from "ng-select2";
import { ModalModule } from "ngx-bootstrap/modal";
import { PaginationModule } from "ngx-bootstrap/pagination";
import { NgxOrgChartModule } from "ngx-org-chart";
import { KanbanCharComponent } from "./kanban-char/kanban-char.component";
import { KanbanDetailComponent } from "./kanban-detail/kanban-detail.component";
import { KanbanRoutingModule } from "./kanban.routing";
import { KanbanComponent } from "./kanban/kanban.component";
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale, viLocale } from 'ngx-bootstrap/chronos';
defineLocale('vi', viLocale);
@NgModule({
  imports: [
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    KanbanRoutingModule,
    NgxOrgChartModule,
    FormsModule,
    CommonModule,
    NgSelect2Module,
    BsDatepickerModule.forRoot(),
    //OrgchartModule
  ],
  declarations: [KanbanCharComponent, KanbanDetailComponent, KanbanComponent],
  providers: [],
})
export class KanbanModule {}
