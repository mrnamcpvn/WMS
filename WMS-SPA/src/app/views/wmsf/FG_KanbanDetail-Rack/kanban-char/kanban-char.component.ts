import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import * as go from "gojs";
import { DataSyncService } from "gojs-angular";
import { WMSF_Carton_LocatService } from "../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-carton-locat.service";
@Component({
  selector: "app-kanban-char",
  templateUrl: "./kanban-char.component.html",
  styleUrls: ["./kanban-char.component.scss"],
})
export class KanbanCharComponent implements OnInit {
  @Output() sendObject = new EventEmitter<any>();
  constructor(private _wMSF_Rack_AreaService: WMSF_Carton_LocatService) {}
  rack_AreaTree: any = [];
  rackFirst: any = [];
  object: any = {
    wareHouseId: "",
    buildingId: "",
    floorId: "",
    areaId: "",
  };
  ngOnInit() {
    // this.loadDataChar();
  }
  ngAfterViewInit() {
    this.loadDataChar();
  }
  groupByKey(data, key) {
    return data.reduce(function (rv, x) {
      (rv[x[key]] = rv[x[key]] || []).push(x);
      return rv;
    }, {});
  }
  loadDataChar() {
    this._wMSF_Rack_AreaService.getListWareHouse().subscribe((res) => {
      this.rack_AreaTree = res;
      this.rackFirst.push(res[0]);
      console.log(this.rack_AreaTree);
    });
  }
  itemClick(wareHouseId, buildingId, floorId, areaId) {
    this.object.wareHouseId = wareHouseId;
    this.object.buildingId = buildingId;
    this.object.floorId = floorId;
    this.object.areaId = areaId;
    this.sendObject.emit(this.object);
  }
  onVisible(item) {
    item.visible = !item.visible;
  }
}
