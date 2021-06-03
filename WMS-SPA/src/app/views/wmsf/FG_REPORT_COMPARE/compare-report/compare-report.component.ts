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
  report_time = new Date();
  reportTime = new Date();
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
    this.GetDateReport();
    this.getData();
    this.spinnerService.show();
    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinnerService.hide();
    }, 5000);
    console.log(this.report_time)
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
    this.fgReportCompareService.ExportExcelByRack(this.report_time.toDateString())
  }

  exportExcelByPO(report_time: string) {
    console.log(this.report_time);
    if (this.report_time == null) {
      this.customSnotifyService.error("Invalid Date", "");
    }
    this.fgReportCompareService.ExportExcelCompare(this.report_time.toDateString())
  }

  loadData() {
    this.spinnerService.show()
    this.fgReportCompareService.getAll(this.report_time.toDateString(), this.typeSort, this.pagination)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(res => {
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
      }, 120000);
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

  GetDateReport() {
    const today = new Date();
    if (today.getDate() != 1) {

      const date = new Date(today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + (today.getDate() - 1));
      this.report_time = date;
    }
    else if ((today.getMonth() + 1) == 1) {
      const date = new Date((today.getFullYear() - 1) + '/' + '12' + '/' + '31');
      this.report_time = date
    }
    else if (today.getMonth() == 1 || today.getMonth() == 3 || today.getMonth() == 5 || today.getMonth() == 7 || today.getMonth() == 8 || today.getMonth() == 10) {
      const date = new Date(today.getFullYear() + '/' + (today.getMonth()) + '/' + '31');
      this.report_time = date
    }
    else if (today.getMonth() == 2 && (today.getFullYear() % 4) == 0) {
      const date = new Date(today.getFullYear() + '/' + (today.getMonth()) + '/' + '29');
      this.report_time = date
    }
    else if (today.getMonth() == 2 && (today.getFullYear() % 4) != 0) {
      const date = new Date(today.getFullYear() + '/' + (today.getMonth()) + '/' + '28');
      this.report_time = date
    }
    else {
      const date = new Date(today.getFullYear() + '/' + (today.getMonth() + 1) + '/' + '30');
      this.report_time = date
    }
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
