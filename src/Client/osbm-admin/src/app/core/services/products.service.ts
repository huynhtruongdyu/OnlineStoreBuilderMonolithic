import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiResponse } from '../models/api-response.model';

export interface IProductsService {
  getAll(): Observable<ApiResponse>
}

@Injectable({
  providedIn: 'root'
})
export class ProductsService implements IProductsService {
  host: string = environment.apiURL;

  constructor(private http: HttpClient) { }

  getAll(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.host}/api/products`);
  }
}
