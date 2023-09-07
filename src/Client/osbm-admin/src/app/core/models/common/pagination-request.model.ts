export interface PaginationRequest {
  searchTerm?: string,
  sortColumn?: string,
  sortOrder?: string,

  pageSize?: number
  pageIndex?: number
}
