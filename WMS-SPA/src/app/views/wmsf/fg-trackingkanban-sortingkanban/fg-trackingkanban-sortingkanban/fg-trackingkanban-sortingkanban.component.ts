import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Select2OptionData } from "ng-select2";
import { FgTrackingKanbanSortingkanbanService } from '../../../../_core/services/wmsf/fg-trackingkanban-sortingkanban/fg-trackingkanban-sortingkanban.service';
import { Pagination } from '../../../../_core/utilities/pagination';
import { SearchParams } from '../../../../_core/models/wmsf/fg-trackingkanban-sortingkanban/SearchParams';
import { VW_FGIN_LOCAT_LIST } from '../../../../_core/models/wmsf/fg-trackingkanban-sortingkanban/cb-wms/VW_FGIN_LOCAT_LIST';
@Component({
  selector: 'app-fg-trackingkanban-sortingkanban',
  templateUrl: './fg-trackingkanban-sortingkanban.component.html',
  styleUrls: ['./fg-trackingkanban-sortingkanban.component.scss']
})
export class FgTrackingkanbanSortingkanbanComponent implements OnInit {
  vw_fgin_locat_list: VW_FGIN_LOCAT_LIST[] = [];
  lines: Array<Select2OptionData>;
  Cartons = 0;
  Pairs = 0;
  CBM = 0;
  intervalData;
  checkInterval;
  isAutoRefreshAll: boolean = true;
  isAutoRefreshPage: boolean = true;
  pagination: Pagination = {
    currentPage: 1,
    pageSize: 10,
    totalCount: 0,
    totalPage: 0
  };
  searchParams: SearchParams = {
    deptId: ' ',
    receivedTime: new Date(),
    sortBy: '',
    sortType: '',
    optionData: 'nocompleted'
  };
  constructor(
    private fgTrackingKanban: FgTrackingKanbanSortingkanbanService,
    private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.show();
    this.getAllDepartments();
    this.search();
    this.spinner.hide();
  }

  search() {
    this.spinner.show();
    if (this.isAutoRefreshAll) {
      clearInterval(this.checkInterval);
    }
    if (this.isAutoRefreshPage) {
      clearInterval(this.intervalData);
    }
    this.fgTrackingKanban.search(this.searchParams, this.pagination).subscribe((res) => {
      this.Cartons = res.sumCartons;
      this.Pairs = res.sumPairs;
      this.CBM = res.sumCBM
      this.pagination = res.dtos.pagination;
      this.vw_fgin_locat_list = res.dtos.result;
      //xử lý hiển thị date
    });
    this.spinner.hide();
    if (this.isAutoRefreshAll) {
      this.autoRefreshAll(true);
    }
    if (this.isAutoRefreshPage) {
      this.autoRefreshPage(true);
    }
  }

  exportExcel() {
    this.fgTrackingKanban.exportExcel(this.searchParams);
  }

  getAllDepartments() {
    this.fgTrackingKanban.getAllDepartment().subscribe((res) => {
      this.lines = res.map((item) => ({ id: item.dept_ID, text: item.dept_ID + ' - ' + item.line_Desc }));
      this.lines.unshift({ id: " ", text: "All" });
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }

  reset() {
    this.pagination.currentPage = 1;
    this.searchParams = {
      deptId: '',
      receivedTime: new Date(),
      sortBy: '',
      sortType: '',
      optionData: 'nocompleted'
    };
    this.search();
  }

  autoRefreshPage(e) {
    if (e === true) {
      this.intervalData = setInterval(() => {
        if (this.pagination.currentPage === this.pagination.totalPage)
          this.pagination.currentPage = 1;
        else
          this.pagination.currentPage += 1;
        this.search();
      }, 10000)
    } else if (e === false && this.isAutoRefreshAll === true) {
      clearInterval(this.intervalData);
      this.autoRefreshAll(true);
    } else {
      clearInterval(this.intervalData);
    }
  }
  autoRefreshAll(e) {
    if (e === true && this.isAutoRefreshPage === false) {
      this.checkInterval = setInterval((page) => {
        this.search();
      }, 10000)
    } else {
      clearInterval(this.checkInterval);
    }
  }
}
