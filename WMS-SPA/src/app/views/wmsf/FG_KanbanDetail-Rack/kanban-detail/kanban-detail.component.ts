import { AfterViewInit, Component, OnInit } from "@angular/core";
import { WMS_LocationService } from "../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wms-location.service";
import { Select2OptionData } from "ng-select2";
import { BsDatepickerConfig, BsLocaleService } from "ngx-bootstrap/datepicker";
import { Pagination } from "../../../../_core/utilities/pagination";
import { PageChangedEvent } from "ngx-bootstrap/pagination";

@Component({
  selector: "app-kanban-detail",
  templateUrl: "./kanban-detail.component.html",
  styleUrls: ["./kanban-detail.component.scss"],
})
export class KanbanDetailComponent implements AfterViewInit {
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
  listArea_CountTotalLoad: any = [];
  listArea_CountTotal: any = [];
  listDataAfterSearch: any = [];
  listDataAll: any;
  listData: any;
  isSearch: boolean = false;
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
  mindate: Date;
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
  setMinDate() {
    debugger;
    if (this.objectSearch.fromDate !== null) {
      this.mindate = new Date();
      this.mindate.setTime(this.objectSearch.fromDate.getTime());
    } else {
      this.mindate = undefined;
    }
  }
  search() {
    debugger;
    if (
      this.objectSearch.buildingId !== "" ||
      this.objectSearch.floorId !== "" ||
      this.objectSearch.areaId !== "" ||
      this.objectSearch.rackNo !== "" ||
      this.objectSearch.poNo !== "" ||
      this.objectSearch.dateType !== "" ||
      this.objectSearch.fromDate !== "" ||
      this.objectSearch.toDate !== ""
    ) {
      this.isSearch = true;
      let newList: any[] = this.listDataAll;
      newList = newList.filter((item) => {
        return (
          (item.warehouse_Id
            .toLowerCase()
            .includes(this.objectSearch.wareHouseId.toLowerCase()) ||
            this.objectSearch.wareHouseId == "") &&
          (item.building_Id
            .toLowerCase()
            .includes(this.objectSearch.buildingId.toLowerCase()) ||
            this.objectSearch.buildingId == "") &&
          (item.floor_Id
            .toLowerCase()
            .includes(this.objectSearch.floorId.toLowerCase()) ||
            this.objectSearch.floorId == "") &&
          (item.area_ID
            .toLowerCase()
            .includes(this.objectSearch.areaId.toLowerCase()) ||
            this.objectSearch.areaId == "") &&
          (item.location_ID
            .toLowerCase()
            .includes(this.objectSearch.rackNo.toLowerCase()) ||
            this.objectSearch.rackNo == "") &&
          (item.order_ID
            .toLowerCase()
            .includes(this.objectSearch.poNo.toLowerCase()) ||
            this.objectSearch.poNo == "")
        );
      });
      if (this.objectSearch.dateType !== "") {
        if (
          this.objectSearch.fromDate !== null &&
          this.objectSearch.fromDate !== ""
        ) {
          newList = newList.filter((item) => {
            return this.objectSearch.dateType == "cfm_date"
              ? +this.objectSearch.fromDate.getTime() <=
                  (item.comfirmed_Date !== null
                    ? +Date.parse(item.comfirmed_Date)
                    : 0)
              : this.objectSearch.dateType == "export_date"
              ? +this.objectSearch.fromDate.getTime() <=
                (item.plan_Ship_Date !== null
                  ? +Date.parse(item.plan_Ship_Date)
                  : 0)
              : +this.objectSearch.fromDate.getTime() <=
                (item.real_Finish_Date !== null
                  ? +Date.parse(item.real_Finish_Date)
                  : 0);
          });
        }
        if (
          this.objectSearch.toDate !== "" &&
          this.objectSearch.toDate !== null
        ) {
          newList = newList.filter((item) => {
            return this.objectSearch.dateType == "cfm_date"
              ? +this.objectSearch.toDate.getTime() >=
                  (item.comfirmed_Date !== null
                    ? +Date.parse(item.comfirmed_Date)
                    : 0)
              : this.objectSearch.dateType == "export_date"
              ? +this.objectSearch.toDate.getTime() >=
                (item.plan_Ship_Date !== null
                  ? +Date.parse(item.plan_Ship_Date)
                  : 0)
              : +this.objectSearch.toDate.getTime() >=
                (item.real_Finish_Date !== null
                  ? +Date.parse(item.real_Finish_Date)
                  : 0);
          });
        }
      }
      //var a =  +this.objectSearch.fromDate.getTime();
      // var b = this.objectSearch.toDate.getTime();
      // var c = Date.parse(this.listDataAll[0].comfirmed_Date);
      this.listDataAfterSearch = newList;
      console.log(this.listDataAfterSearch);

      const event: PageChangedEvent = {
        page: 1,
        itemsPerPage: this.pagination.pageSize,
      };
      this.pageChanged(event);
    } else {
      // tslint:disable-next-line: prefer-const
      let event: any = {
        page: 1,
        itemsPerPage: this.pagination.pageSize,
      };
      this.pagination.currentPage = 1;
      this.isSearch = false;
      this.pageChanged(event);
    }
  }
  area_CountTotal() {
    if (this.listArea_CountTotal.length > 0) {
      let areaReload = this.listArea_CountTotalLoad;
      this.listArea_CountTotal = [];
      this.listArea_CountTotal = areaReload;
      let subTotal: number = 0;
      this.listArea_CountTotal.map((i) => {
        let listAreaWithID = this.listData.filter((f) => f.area_ID == i.value);
        let total: number = 0;
        listAreaWithID.map((i) => {
          total += i.countCartonPairs;
        });
        i.totalCount = total;
        subTotal += total;
      });
      this.listArea_CountTotal.push({
        value: "Total",
        label: "Total",
        totalCount: subTotal,
      });

      console.log(this.listArea_CountTotalLoad);
    }
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.loadDataNoPagination();
      this.area_CountTotal();
    });
  }
  ngOnInit() {
    this.loadDataNoPagination();
    this.getListWarehouse();
    this.getListBuilding();
    this.getListFloor();
    this.getListArea();
    this.getListAreaTotal();
  }

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
        },
        (error) => {}
      );
  }

  loadDataNoPagination() {
    this._wMS_LocationService
      .searchDataNoPagintion(this.objectSearch)
      .subscribe(
        (res) => {
          this.listDataAll = res;
          if (res.length < 1) {
            this.dataFound = true;
          }
          const startItem =
            (this.pagination.currentPage - 1) * this.pagination.pageSize;
          const endItem =
            this.pagination.currentPage * this.pagination.pageSize;
          this.listData = res.slice(startItem, endItem);
        },
        (error) => {}
      );
  }
  pageChanged(event: any): void {
    debugger;
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
      this.listArea_CountTotal = res;
      this.listArea_CountTotalLoad = res;
    });
  }
}
