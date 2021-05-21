import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { FgReportCompareService } from '../../../../_core/services/wmsf/FG_REPORT_COMPARE/fg-report-compare.service';
import { Pagination } from '../../../../_core/utilities/pagination';
import { switchMap, takeUntil } from 'rxjs/operators';
import { WMSF_FG_CompareReport } from '../../../../_core/models/wmsf/FG_REPORT_COMPARE/WMSF_FG_CompareReport';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-compare-report',
  templateUrl: './compare-report.component.html',
  styleUrls: ['./compare-report.component.scss']
})
export class CompareReportComponent implements OnInit, OnDestroy {
  private readonly unsubscribe$: Subject<void> = new Subject();
  reportTime = "2021-05-20";
  wMSF_FG_CompareReport: WMSF_FG_CompareReport[]
  pagination: Pagination;
  constructor(
    public fgReportCompareService: FgReportCompareService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.route.data.pipe(takeUntil(this.unsubscribe$)).subscribe(res => {
      this.pagination = res.data.pagination;
      this.wMSF_FG_CompareReport = res.data.result;
      console.log(this.wMSF_FG_CompareReport);

    })
  }

  loadData() {
    this.fgReportCompareService.getAll(this.reportTime, this.pagination)
      .pipe(takeUntil(this.unsubscribe$)).subscribe(res => {
        console.log(res);
        this.pagination = res.pagination
        this.wMSF_FG_CompareReport = res.result
      });
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
