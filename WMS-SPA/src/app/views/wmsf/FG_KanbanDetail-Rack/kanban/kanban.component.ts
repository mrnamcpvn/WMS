import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Component, OnInit, ViewChild } from '@angular/core';
import { RackPairs } from '../../../../_core/models/wmsf/FG_KanbanDetail-Rack/rack-paris.model';
import { WMSF_Rack_AreaService } from '../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-rack-area.service';
import { KanbanCharComponent } from '../kanban-char/kanban-char.component';
import { KanbanDetailComponent } from '../kanban-detail/kanban-detail.component';

@UntilDestroy()
@Component({
  selector: 'app-kanban',
  templateUrl: './kanban.component.html',
  styleUrls: ['./kanban.component.scss'],
})
export class KanbanComponent implements OnInit {
  @ViewChild('kanbanChar', { static: false }) kanbanChar: KanbanCharComponent;
  @ViewChild('kanbanDetail', { static: false })
  kanbanDetail: KanbanDetailComponent;
  object: any = {
    wareHouseId: '',
    buildingId: '',
    floorId: '',
    areaId: '',
  };
  listRackPairs: RackPairs[] = [];
  isVisible: number = 0;
  constructor(private _wMSF_Rack_AreaService: WMSF_Rack_AreaService) {}

  ngOnInit() {
    this.getListRackPairs();
  }

  ngAfterViewInit() {}
  getListRackPairs() {
    this._wMSF_Rack_AreaService.getListRackPairs().pipe(untilDestroyed(this)).subscribe((res) => {
      this.listRackPairs = res;
    });
  }
  setVisible(event) {
    this.isVisible = event;
  }

  getObjectFromChar(event) {
    this.object.wareHouseId = event.wareHouseId;
    this.object.buildingId = event.buildingId;
    this.object.floorId = event.floorId;
    this.object.areaId = event.areaId;
    this.isVisible = 1;
  }
}
