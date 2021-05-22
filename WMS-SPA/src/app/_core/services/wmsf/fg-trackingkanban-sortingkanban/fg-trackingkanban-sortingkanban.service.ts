import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { SearchParams } from '../../../models/wmsf/fg-trackingkanban-sortingkanban/SearchParams';
import { Pagination } from '../../../utilities/pagination';
import { FunctionUtility } from '../../../../_core/utilities/fucntion-utility';
@Injectable({ providedIn: 'root' })
export class FgTrackingKanbanSortingkanbanService {
    baseUrl = environment.apiUrl;
    constructor(private httpClient: HttpClient, private fuctionUtility: FunctionUtility) { }
    debugger;
    getAllDepartment() {
        return this.httpClient.get<any>(this.baseUrl + "FG_TrackingKanban_SortingKanban/getAllDepartment");
    }

    search(searchParams: SearchParams, paginationParams: Pagination) {
        let receivedTime = this.fuctionUtility.getStringDate(new Date(searchParams.receivedTime));
        let params = new HttpParams();
        params = params.append('deptId', searchParams.deptId);
        params = params.append('receivedTime', receivedTime);
        params = params.append('sortBy', searchParams.sortBy);
        params = params.append('sortType', searchParams.sortType);
        params = params.append('optionData', searchParams.optionData);
        params = params.append('pageNumber', paginationParams.currentPage.toString());
        params = params.append('pageSize', paginationParams.pageSize.toString());
        return this.httpClient.get<any>(this.baseUrl + 'FG_TrackingKanban_SortingKanban/search', { params });
    }

    exportExcel(searchParams: SearchParams) {
        let receivedTime = this.fuctionUtility.getStringDate(new Date(searchParams.receivedTime));
        searchParams.receivedTime = receivedTime;
        return this.httpClient.post(this.baseUrl + 'FG_TrackingKanban_SortingKanban/exportExcel', searchParams, { responseType: 'blob' })
            .subscribe((result: Blob) => {
                if (result.type !== 'application/xlsx') {
                    alert(result.type);
                }
                const blob = new Blob([result]);
                const url = window.URL.createObjectURL(blob);
                const link = document.createElement('a');
                const currentTime = new Date();
                const filename = 'ExportData-' + currentTime.getFullYear().toString() +
                    (currentTime.getMonth() + 1) + currentTime.getDate() + "_" +
                    currentTime.toLocaleTimeString().replace(/[ ]|[,]|[:]/g, '').trim() + '.xlsx';
                link.href = url;
                link.setAttribute('download', filename);
                document.body.appendChild(link);
                link.click();
            });
    }
}