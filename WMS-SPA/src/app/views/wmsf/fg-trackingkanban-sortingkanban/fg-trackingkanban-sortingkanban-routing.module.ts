import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { FgTrackingkanbanSortingkanbanComponent } from './fg-trackingkanban-sortingkanban/fg-trackingkanban-sortingkanban.component';

export const routes: Routes = [
    {
        path: '',
        data: {
            title: 'FG Tracking Kanban - Sorting Kanban'
        },
        component: FgTrackingkanbanSortingkanbanComponent,
    }
]



@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class TrackingKanBanRoutingModule { }
