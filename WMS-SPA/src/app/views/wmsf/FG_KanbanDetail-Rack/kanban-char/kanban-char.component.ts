import { Component, EventEmitter, OnInit, Output } from "@angular/core";
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
  }
  ngAfterViewInit() {
    this.loadDataChar();
  }
  loadDataChar() {
    this._wMSF_Rack_AreaService.getListWareHouse().subscribe((res) => {
      this.rack_AreaTree = res;
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
