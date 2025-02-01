export interface PaginationParams {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginationResult<T> {
  results: T;
  paginationParams: PaginationParams | undefined;

  constructor(data: T, paginationParams: PaginationParams | undefined) {
    this.results = data;
    this.paginationParams = paginationParams;
  }
}

export class PaginationRequestParams {
  pageSize: number;
  pageNumber: number;

  constructor(pageSize: number, pageNumber: number) {
    this.pageSize = pageSize;
    this.pageNumber = pageNumber;
  }
}
