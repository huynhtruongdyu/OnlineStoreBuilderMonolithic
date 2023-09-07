import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { objectToQueryString } from 'src/app/shared/utils/http.helper';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { PaginationRequest } from '../models/common/pagination-request.model';
import { ProductCreateRequest } from '../models/products/@index';

export interface IProductsReadService {
  getAll(): Observable<ApiResponse>
}

export interface IProductWriteService {
  create(product: ProductCreateRequest): Observable<ApiResponse>
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService implements IProductsReadService, IProductWriteService {
  host: string = environment.apiURL;

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.host}/api/products`);
  }

  getAllV2(filter?: PaginationRequest): Observable<ApiResponse> {
    let query = objectToQueryString(filter);
    const requestUrl = `${this.host}/api/products?` + query;
    console.log('requestUrl', requestUrl);

    let headers = new HttpHeaders();
    headers = headers.set('x-api-version', '2');
    return this.http.get<ApiResponse>(requestUrl, {
      headers: headers
    });
  }

  create(product: any): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.host}/api/products`, product);

  }
}
