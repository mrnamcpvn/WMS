import { Component, OnInit, ViewChild } from "@angular/core";
import { RackPairs } from "../../../../_core/models/wmsf/FG_KanbanDetail-Rack/rack-paris.model";
import { WMSF_Rack_AreaService } from "../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-rack-area.service";
import { KanbanCharComponent } from "../kanban-char/kanban-char.component";
import { KanbanDetailComponent } from "../kanban-detail/kanban-detail.component";

@Component({
  selector: "app-kanban",
  templateUrl: "./kanban.component.html",
  styleUrls: ["./kanban.component.scss"],
})
export class KanbanComponent implements OnInit {
  @ViewChild("kanbanChar", { static: false }) kanbanChar: KanbanCharComponent;
  @ViewChild("kanbanDetail", { static: false })
  kanbanDetail: KanbanDetailComponent;
  listRackPairs: RackPairs[] = [];
  isVisible: number = 1;
  constructor(private _wMSF_Rack_AreaService: WMSF_Rack_AreaService) {}

  ngOnInit() {
    this.getListRackPairs();
  }

  ngAfterViewInit() {}
  getListRackPairs() {
    this._wMSF_Rack_AreaService.getListRackPairs().subscribe((res) => {
      this.listRackPairs = res;
    });
  }

  setVisible(event) {
    this.isVisible = event;
    console.log(this.isVisible);
    
  }
}
