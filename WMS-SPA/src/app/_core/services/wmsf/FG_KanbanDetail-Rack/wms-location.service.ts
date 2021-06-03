import { SearchParam } from './../../../models/wmsf/FG_KanbanDetail-Rack/search-param.model';
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
export class WMS_LocationService {
  constructor(private http: HttpClient) {}

  getListWarehouse() {
    return this.http.get<SelectOptions[]>(
      `${API}WMS_Location/GetListWarehouse`
    );
  }
  getListBuilding() {
    return this.http.get<SelectOptions[]>(`${API}WMS_Location/GetListBuilding`);
  }
  getListFloor() {
    return this.http.get<SelectOptions[]>(`${API}WMS_Location/GetListFloor`);
  }
  getListArea() {
    return this.http.get<SelectOptions[]>(`${API}WMS_Location/GetListArea`);
  }
  getListAreTotal() {
    return this.http.get<any[]>(
      `${API}WMSF_Rack_Area/GetListAreaTotal`
    );
  }
  searchData(pagination: Pagination, searchParam: SearchParam) {
    let params = new HttpParams();
    if (pagination.currentPage !== null && pagination.pageSize !== null) {
      params = params.append("pageNumber", pagination.currentPage.toString());
      params = params.append("pageSize", pagination.pageSize.toString());
    }
    return this.http.post<PaginationResult<any>>(
      `${API}WMS_Location/searchData`, searchParam, { params }
    );
  }

  searchDataNoPagintion(searchParam: SearchParam) {
    debugger
    return this.http.post<any>(`${API}WMS_Location/SearchDataNoPagintion`, searchParam);
  }
}
