import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../../environments/environment";
import { RackPairs } from "../../../models/wmsf/FG_KanbanDetail-Rack/rack-paris.model";

const API = environment.apiUrl;
@Injectable({
  providedIn: "root",
})
export class WMSF_Rack_AreaService {
  constructor(private http: HttpClient) {}

  getListRackPairs() {
    return this.http.get<RackPairs[]>(
      `${API}WMSF_Rack_Area/GetListRackPairs`
    );
  }
}
