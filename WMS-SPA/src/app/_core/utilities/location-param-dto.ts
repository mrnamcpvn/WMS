import { SortParams } from './sort-param';
import { SearchParam } from '../models/wmsf/FG_KanbanDetail-Rack/search-param.model';

export interface LocatinParamDTO {
  searchParam: SearchParam;
  sortParams: SortParams[];
}
