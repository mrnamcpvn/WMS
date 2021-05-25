import { Component, OnInit } from "@angular/core";
import * as go from "gojs";
import { DataSyncService } from "gojs-angular";
import { WMSF_Carton_LocatService } from "../../../../_core/services/wmsf/FG_KanbanDetail-Rack/wmsf-carton-locat.service";
@Component({
  selector: "app-kanban-char",
  templateUrl: "./kanban-char.component.html",
  styleUrls: ["./kanban-char.component.scss"],
})
export class KanbanCharComponent implements OnInit {
  constructor(private _wMSF_Rack_AreaService: WMSF_Carton_LocatService) {}
  rack_AreaTree: any = [];
  rackFirst: any = [];
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
      console.log(this.rackFirst);
    });
  }
  itemClick(event) {
    debugger;
    let a = event;
  }
}
