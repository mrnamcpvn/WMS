import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelect2Module } from 'ng-select2';
import { CommonModule } from "@angular/common";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AlertModule } from 'ngx-bootstrap/alert'
import { FgTrackingkanbanSortingkanbanComponent } from './fg-trackingkanban-sortingkanban/fg-trackingkanban-sortingkanban.component';
import { TrackingKanBanRoutingModule } from './fg-trackingkanban-sortingkanban-routing.module';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from "ngx-spinner";
@NgModule({
    declarations: [
        FgTrackingkanbanSortingkanbanComponent
    ],
    imports: [
        CommonModule,
        BsDatepickerModule.forRoot(),
        NgSelect2Module,
        PaginationModule,
        AlertModule.forRoot(),
        TrackingKanBanRoutingModule,
        FormsModule,
        NgxSpinnerModule
    ],
    // schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class TrackingKanBanModule { }
