import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { FgReportCompareService } from '../../../../_core/services/wmsf/FG_REPORT_COMPARE/fg-report-compare.service';
import { Pagination } from '../../../../_core/utilities/pagination';
import { takeUntil } from 'rxjs/operators';
import { WMSF_FG_CompareReport } from '../../../../_core/models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport';
import { ActivatedRoute } from '@angular/router';
import { CustomSnotifyService } from '../../../../_core/services/wmsf/FG_REPORT_COMPARE/custom-snotify.service';
import { FunctionUtility } from '../../../../_core/utilities/fucntion-utility';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-compare-report',
  templateUrl: './compare-report.component.html',
  styleUrls: ['./compare-report.component.scss']
})
export class CompareReportComponent implements OnInit, OnDestroy {
  private readonly unsubscribe$: Subject<void> = new Subject();
  isAutoPage: boolean = true;
  date = new Date();
  report_time: Date = new Date(this.date.getFullYear() + '/' + (this.date.getMonth() + 1) + '/' + (this.date.getDate() - 1));
  reportTime = this.date.getFullYear() + '/' + (this.date.getMonth() + 1) + '/' + (this.date.getDate() - 1);
  wMSF_FG_CompareReport: WMSF_FG_CompareReport[]
  pagination: Pagination = {
    currentPage: 1,
    pageSize: 10
  } as Pagination;
  intervalReloadData = null;
  typeSort = "desc";
  sortClass = "fa fa-sort-amount-desc";
  constructor(
    private fgReportCompareService: FgReportCompareService,
    private route: ActivatedRoute,
    private customSnotifyService: CustomSnotifyService,
    private fu: FunctionUtility,
    private spinnerService: NgxSpinnerService,
  ) { }

  ngOnInit() {
    this.getData();
    this.spinnerService.show();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinnerService.hide();
    }, 5000);
  }

  getData() {
    this.route.data.pipe(takeUntil(this.unsubscribe$)).subscribe(res => {
      if (this.isAutoPage) {
        this.autoRefreshPage(true);
      }
      this.pagination = res.data.pagination;
      this.wMSF_FG_CompareReport = res.data.result;
    })
  }

  pageChanged(event: any) {
    this.spinnerService.show()
    this.pagination.currentPage = event.page;
    this.loadData();
  }

  exportExcelByrack(report_time: string) {
    console.log(this.report_time);

    if (this.report_time == null) {
      this.customSnotifyService.error("Invalid Date", "");
    }
    this.fgReportCompareService.ExportExcelByRack(this.fu.getStringDate(this.report_time))
  }

  exportExcelByPO(report_time: string) {
    if (this.report_time == null) {
      this.customSnotifyService.error("Invalid Date", "");
    }
    this.fgReportCompareService.ExportExcelCompare(this.fu.getStringDate(this.report_time))
  }

  loadData() {
    this.spinnerService.show()
    this.fgReportCompareService.getAll(this.fu.getStringDate(this.report_time), this.typeSort, this.pagination).subscribe(res => {
      this.spinnerService.hide();
      if (res === null) {
        this.customSnotifyService.error("Please select report time !!!", "Error  notice");
        this.wMSF_FG_CompareReport = null;
      } else {
        this.wMSF_FG_CompareReport = res.result;
        this.pagination = res.pagination;
      }
    })
  }

  searchData() {
    if (this.isAutoPage) {
      clearInterval(this.intervalReloadData)
    }
    this.pagination.currentPage = 1;
    this.spinnerService.show();
    this.loadData();
  }

  autoRefreshPage(val) {
    if (val) {
      this.intervalReloadData = setInterval(() => {
        this.pagination.currentPage = this.pagination.currentPage === this.pagination.totalPage ? 1 : this.pagination.currentPage + 1;
        this.loadData();
      }, 180000);
    } else {
      clearInterval(this.intervalReloadData);
    }
  }

  sort() {
    if (this.typeSort === 'desc') {
      this.typeSort = "asc";
      this.sortClass = "fa fa-sort-amount-asc";
    } else if (this.typeSort === 'asc') {
      this.typeSort = 'desc';
      this.sortClass = 'fa fa-sort-amount-desc';
    }
    this.searchData();
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
