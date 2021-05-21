import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelect2Module } from 'ng-select2';
import { CommonModule } from "@angular/common";
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AlertModule } from 'ngx-bootstrap/alert'
import { FgTrackingkanbanSortingkanbanComponent } from './fg-trackingkanban-sortingkanban/fg-trackingkanban-sortingkanban.component';
import { TrackingKanBanRoutingModule } from './fg-trackingkanban-sortingkanban-routing.module';

@NgModule({
    declarations: [
        FgTrackingkanbanSortingkanbanComponent
    ],
    imports: [
        CommonModule,
        BsDatepickerModule.forRoot(),
        NgSelect2Module,
        NgxSpinnerModule,
        PaginationModule,
        AlertModule.forRoot(),
        TrackingKanBanRoutingModule
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class TrackingKanBanModule { }
