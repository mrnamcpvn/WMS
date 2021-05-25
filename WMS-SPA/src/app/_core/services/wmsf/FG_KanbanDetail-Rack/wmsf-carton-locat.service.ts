import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../../../environments/environment";
import { RackPairs } from "../../../models/wmsf/FG_KanbanDetail-Rack/rack-paris.model";
import { SelectOptions } from "../../../models/wmsf/FG_KanbanDetail-Rack/select-options.model";
import { Pagination, PaginationResult } from "../../../utilities/pagination";

const API = environment.apiUrl;
@Injectable({
  providedIn: "root",
})
export class WMSF_Carton_LocatService {
  constructor(private http: HttpClient) {}

  getListWareHouse() {
    return this.http.get<any[]>(
      `${API}WMSF_Carton_Locat/LoadDataChart`
    );
  }
 
}
