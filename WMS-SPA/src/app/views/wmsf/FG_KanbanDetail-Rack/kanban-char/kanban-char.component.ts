import { RackPairs } from './../../../../_core/models/wmsf/FG_KanbanDetail-Rack/rack-paris.model';
import { WMSF_Rack_AreaService } from './../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-rack-area.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { WMS_LocationService } from '../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wms-location.service';
import { WMSF_Carton_LocatService } from '../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-carton-locat.service';
@Component({
  selector: 'app-kanban-char',
  templateUrl: './kanban-char.component.html',
  styleUrls: ['./kanban-char.component.scss'],
})
export class KanbanCharComponent implements OnInit {
  @Output() sendObject = new EventEmitter<any>();
  constructor(
    private _wMSF_Carton_LocatService: WMSF_Carton_LocatService,
    private _wMSF_Rack_AreaService: WMSF_Rack_AreaService
  ) {}
  rack_AreaTree: any = [];
  rackFirst: any = [];
  listRackPairs: RackPairs[] = [];
  object: any = {
    wareHouseId: '',
    buildingId: '',
    floorId: '',
    areaId: '',
  };
  ngOnInit() {
    this.loadDataChar();
  }

  loadDataChar() {
    this._wMSF_Carton_LocatService.getListWareHouse().subscribe((res) => {
      this.rack_AreaTree = res;
    });
    this.getListRackPairs();

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

  getListRackPairs() {
    this._wMSF_Rack_AreaService.getListRackPairs().subscribe((res) => {
      this.listRackPairs = res;
      console.log(this.listRackPairs);
    });
  }


}
