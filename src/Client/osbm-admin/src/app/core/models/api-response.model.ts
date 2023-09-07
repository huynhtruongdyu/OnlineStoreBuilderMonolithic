export interface ApiResponse {
  isSuccess: boolean,
  statusCode: number,
  messages?: string,
  data?: any | PaginationData
}

export interface PaginationData {
  totalItems: number,
  pageSize: number,
  pageCount: number,
  pageIndex: number,
  canGoNext: boolean,
  canGoPrevious: boolean,
  items?: any[]
}
