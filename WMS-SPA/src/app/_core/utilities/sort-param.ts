export interface SortParams {
  sortColumn: string;
  sortBy: SortBy;
  sortClass: SortClass;
}

export enum SortBy {
  Asc = 'ASC',
  Desc = 'DESC'
}

export enum SortClass {
  Asc = 'fa fa-lg fa-sort-amount-asc',
  Desc = 'fa fa-lg fa-sort-amount-desc'
}
