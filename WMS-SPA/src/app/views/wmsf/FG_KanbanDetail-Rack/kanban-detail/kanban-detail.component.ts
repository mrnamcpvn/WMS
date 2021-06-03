import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { WMS_LocationService } from '../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wms-location.service';
import { Select2OptionData } from 'ng-select2';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Pagination } from '../../../../_core/utilities/pagination';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { WMSF_Rack_AreaService } from '../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-rack-area.service';
import { SearchParam } from '../../../../_core/models/wmsf/FG_KanbanDetail-Rack/search-param.model';

@Component({
  selector: 'app-kanban-detail',
  templateUrl: './kanban-detail.component.html',
  styleUrls: ['./kanban-detail.component.scss'],
})
export class KanbanDetailComponent implements AfterViewInit {
  @Output() visible = new EventEmitter<any>();
  @Input() object: any;
  pagination: Pagination = {
    currentPage: 1,
    pageSize: 10,
    totalCount: 0,
    totalPage: 0,
  };
  listWareHouses: Array<Select2OptionData>;
  listBuildings: Array<Select2OptionData>;
  listFloors: Array<Select2OptionData>;
  listAreas: Array<Select2OptionData>;
  listArea_CountTotal: any = [];
  listDataAfterSearch: any = [];
  listDataAll = [];
  listData: any;
  isSearch: boolean = false;
  isAutoRefresh: boolean = false;
  searchParam: SearchParam = {
    wareHouseId: '',
    buildingId: '',
    floorId: '',
    areaId: '',
    dateType: '',
    rackNo: '',
    poNo: '',
    sortBy: '',
    sortType: '',
    function: ''
  } as SearchParam;
  mindate: Date;
  dataFound: boolean = false;
  locale = 'vi';
  colorTheme = 'theme-green';

  bsConfig: Partial<BsDatepickerConfig>;
  constructor(
    private _wMS_LocationService: WMS_LocationService,
    private _wMSF_Rack_AreaService: WMSF_Rack_AreaService,
    private localeService: BsLocaleService
  ) {
    this.localeService.use(this.locale);
  }
  ngAfterViewInit() {
    setTimeout(() => {
      this.loadDataNoPagination();
      this.area_CountTotal();
    });
  }
  ngOnInit() {
    this.getListWarehouse();
    this.getListBuilding();
    this.getListFloor();
    this.getListArea();
  }
  getObjectFromParent(obj) {
    this.searchParam.wareHouseId = obj.wareHouseId;
    this.searchParam.buildingId = obj.buildingId;
    this.searchParam.floorId = obj.floorId;
    this.searchParam.areaId = obj.areaId;
    if (
      obj.wareHouseId !== '' ||
      obj.buildingId !== '' ||
      obj.floorId !== '' ||
      obj.areaId !== ''
    ) {
      this.isSearch = true;
    }
    this.search();
  }
  setFromDate() {
    if (this.searchParam.fromDate !== null) {
      this.searchParam.fromDate.setHours(0, 0, 0, 0);
      this.mindate = new Date();
      this.mindate.setTime(this.searchParam.fromDate.getTime());
    } else {
      this.mindate = undefined;
    }
    if (this.isAutoRefresh) {
      this.search();
    }
  }
  setToDate() {
    if (this.searchParam.toDate !== null) {
      this.searchParam.toDate.setHours(23, 59, 0, 0);
    }
    if (this.isAutoRefresh) {
      this.search();
    }
  }

  autoRefresh() {
    if (this.isAutoRefresh) {
      this.search();
    }
  }
  search() {
    if (
      this.searchParam.wareHouseId !== '' ||
      this.searchParam.buildingId !== '' ||
      this.searchParam.floorId !== '' ||
      this.searchParam.areaId !== '' ||
      this.searchParam.rackNo !== '' ||
      this.searchParam.poNo !== '' ||
      this.searchParam.dateType !== '' ||
      this.searchParam.fromDate ||
      this.searchParam.toDate
    ) {
      this.isSearch = true;
      let newList: any[] = this.listDataAll;
      newList = newList.filter((item) => {
        return (
          (item.warehouse_Id.toLowerCase().includes(this.searchParam.wareHouseId.toLowerCase()) ||
            this.searchParam.wareHouseId == '') &&
          (item.building_Id.toLowerCase().includes(this.searchParam.buildingId.toLowerCase()) ||
            this.searchParam.buildingId == '') &&
          (item.floor_Id.toLowerCase().includes(this.searchParam.floorId.toLowerCase()) ||
            this.searchParam.floorId == '') &&
          (item.area_ID.toLowerCase().includes(this.searchParam.areaId.toLowerCase()) ||
            this.searchParam.areaId == '') &&
          (item.location_ID.toLowerCase().includes(this.searchParam.rackNo.toLowerCase()) ||
            this.searchParam.rackNo == '') &&
          (item.order_ID.toLowerCase().includes(this.searchParam.poNo.toLowerCase()) ||
            this.searchParam.poNo == '')
        );
      });
      if (this.searchParam.dateType !== '') {
        if (this.searchParam.fromDate !== null) {
          newList = newList.filter((item) => {
            return this.searchParam.dateType == 'cfm_date'
              ? +this.searchParam.fromDate.getTime() <=
                  (item.comfirmed_Date !== null
                    ? +Date.parse(item.comfirmed_Date)
                    : 0)
              : this.searchParam.dateType == 'export_date'
              ? +this.searchParam.fromDate.getTime() <=
                (item.plan_Ship_Date !== null
                  ? +Date.parse(item.plan_Ship_Date)
                  : 0)
              : +this.searchParam.fromDate.getTime() <=
                (item.real_Finish_Date !== null
                  ? +Date.parse(item.real_Finish_Date)
                  : 0);
          });
        }
        if (this.searchParam.toDate !== null) {
          newList = newList.filter((item) => {
            return this.searchParam.dateType == 'cfm_date'
              ? +this.searchParam.toDate.getTime() >=
                  (item.comfirmed_Date !== null
                    ? +Date.parse(item.comfirmed_Date)
                    : 0)
              : this.searchParam.dateType == 'export_date'
              ? +this.searchParam.toDate.getTime() >=
                (item.plan_Ship_Date !== null
                  ? +Date.parse(item.plan_Ship_Date)
                  : 0)
              : +this.searchParam.toDate.getTime() >=
                (item.real_Finish_Date !== null
                  ? +Date.parse(item.real_Finish_Date)
                  : 0);
          });
        }
      }
      this.listDataAfterSearch = newList;
      const event: PageChangedEvent = {
        page: 1,
        itemsPerPage: this.pagination.pageSize,
      };
      this.pageChanged(event);
    } else {
      const event: any = {
        page: 1,
        itemsPerPage: this.pagination.pageSize,
      };
      this.pagination.currentPage = 1;
      this.isSearch = false;
      this.pageChanged(event);
    }
  }
  area_CountTotal() {
    setTimeout(() => {
      if (this.listArea_CountTotal.length > 0) {
        let subTotal = 0;
        this.listArea_CountTotal
          .filter((f) => f.value !== 'Total')
          .map((i) => {
            const listAreaWithID = this.listDataAfterSearch.filter(
              (f) => f.area_ID == i.value
            );
            let total = 0;
            listAreaWithID.map((x) => {
              total += x.countCartonPairs;
            });
            i.totalCount = total;
            subTotal += total;
          });
        const total = this.listArea_CountTotal.find((f) => f.value === 'Total');
        if (total !== null) {
          total.totalCount = subTotal;
        }
      }
    });
  }

  setVisible() {
    this.visible.emit(5);
  }

  loadDataNoPagination() {
    console.log('search param: ', this.searchParam);
    this._wMS_LocationService.searchDataNoPagintion(this.searchParam).subscribe(
      (res) => {
        debugger
        this.listDataAll = res;
        this.listDataAfterSearch = res;
        if (res.length < 1) {
          this.dataFound = true;
        }
        const startItem =
          (this.pagination.currentPage - 1) * this.pagination.pageSize;
        const endItem = this.pagination.currentPage * this.pagination.pageSize;
        this.listData = res.slice(startItem, endItem);
        this.getListAreaTotal();
        this.getObjectFromParent(this.object);
      },
      (error) => {}
    );
  }
  exportExcel() {
    this._wMSF_Rack_AreaService.exportExcel(this.listDataAfterSearch);
  }
  pageChanged(event: any): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    if (this.isSearch) {
      this.pagination.currentPage = 1;
      this.listData = this.listDataAfterSearch.slice(startItem, endItem);
    } else {
      this.listData = this.listDataAll.slice(startItem, endItem);
    }
    if (this.listData.length < 1) {
      this.dataFound = true;
    } else {
      this.dataFound = false;
    }
    this.area_CountTotal();
  }

  getListWarehouse() {
    this._wMS_LocationService.getListWarehouse().subscribe((res) => {
      this.listWareHouses = res.map((item) => {
        return {
          id: item.value.toString(),
          text: item.label,
        };
      });
    });
  }
  getListBuilding() {
    this._wMS_LocationService.getListBuilding().subscribe((res) => {
      this.listBuildings = res.map((item) => {
        return {
          id: item.value.toString(),
          text: item.label,
        };
      });
    });
  }
  getListFloor() {
    this._wMS_LocationService.getListFloor().subscribe((res) => {
      this.listFloors = res.map((item) => {
        return {
          id: item.value.toString(),
          text: item.label,
        };
      });
    });
  }
  getListArea() {
    this._wMS_LocationService.getListArea().subscribe((res) => {
      this.listAreas = res.map((item) => {
        return {
          id: item.value.toString(),
          text: item.label,
        };
      });
    });
  }
  getListAreaTotal() {
    this._wMS_LocationService.getListAreTotal().subscribe((res) => {
      res.push({
        value: 'Total',
        label: 'Total',
        totalCount: 0,
        visible: true,
      });
      this.listArea_CountTotal = res;
      this.area_CountTotal();
    });
  }
}
