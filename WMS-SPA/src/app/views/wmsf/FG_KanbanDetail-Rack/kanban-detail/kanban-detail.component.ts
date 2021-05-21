import { Component, OnInit } from "@angular/core";
import { WMS_LocationService } from "../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wms-location.service";
import { Select2OptionData } from "ng-select2";
import { BsDatepickerConfig, BsLocaleService } from "ngx-bootstrap/datepicker";
import { Pagination } from "../../../../_core/utilities/pagination";

@Component({
  selector: "app-kanban-detail",
  templateUrl: "./kanban-detail.component.html",
  styleUrls: ["./kanban-detail.component.scss"],
})
export class KanbanDetailComponent implements OnInit {
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
  listData: any;
  objectSearch: any = {
    wareHouseId: "",
    buildingId: "",
    floorId: "",
    areaId: "",
    rackNo: "",
    poNo: "",
    dateType: "",
    fromDate: "",
    toDate: "",
    sortBy: "",
    sortType: "",
    function: "",
  };
  dataFound: boolean = false;
  locale = "vi";
  colorTheme = "theme-green";

  bsConfig: Partial<BsDatepickerConfig>;
  constructor(
    private _wMS_LocationService: WMS_LocationService,
    private localeService: BsLocaleService
  ) {
    this.localeService.use(this.locale);
  }

  ngOnInit() {
    this.loadData();
    this.getListWarehouse();
    this.getListBuilding();
    this.getListFloor();
    this.getListArea();
  }
  ngOnAfterViewInit() {}
  loadData() {
    this._wMS_LocationService
      .searchData(this.pagination, this.objectSearch)
      .subscribe(
        (res) => {
          this.listData = res.result;
          if (res.result.length < 1) {
            this.dataFound = true;
          }
          this.pagination = res.pagination;
          console.log(this.listData);
        },
        (error) => {}
      );
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page === undefined ? 1 : event.page;
    this.loadData();
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
}
