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
    return this.http.get<RackPairs[]>(`${API}WMSF_Rack_Area/GetListRackPairs`);
  }
  exportExcel(data: any) {
    return this.http
      .post(`${API}WMSF_Rack_Area/exportExcel`, data, { responseType: "blob" })
      .subscribe((result: Blob) => {
        if (result.type !== "application/xlsx") {
          alert(result.type);
        }
        if (result.size == 0) {
          alert("No data");
          return;
        }
        const blob = new Blob([result]);
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement("a");
        const currentTime = new Date();
        const filename =
          "DataKanBan" +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate() +
          currentTime
            .toLocaleTimeString()
            .replace(/[ ]|[,]|[:]/g, "")
            .trim() +
          ".xlsx";
        link.href = url;
        link.setAttribute("download", filename);
        document.body.appendChild(link);
        link.click();
      });
  }
}
