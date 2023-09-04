export interface ApiResponse {
  isSuccess: boolean,
  statusCode: number,
  messages?: string,
  data?: any
}
