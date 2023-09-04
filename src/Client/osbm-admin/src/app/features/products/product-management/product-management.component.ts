import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiResponse } from 'src/app/core/models/api-response.model';
import { Product } from 'src/app/core/models/products/product.model';
import { ProductsService } from 'src/app/core/services/products.service';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.scss']
})
export class ProductManagementComponent implements OnInit, OnDestroy {

  //#region fields
  products: Product[] = [];
  getAllProductSubscription?: Subscription;
  //#endregion

  constructor(private productService: ProductsService) {

  }

  ngOnInit(): void {
    this.getAllProductSubscription = this.productService.getAll()
      .subscribe({
        next: (resp: ApiResponse) => {
          if (resp.isSuccess) {
            this.products = resp.data as Product[];
          }
        },
        error: (error: any) => {
          console.error(error);
        }
      });
  }
  ngOnDestroy(): void {
    this.getAllProductSubscription?.unsubscribe();
  }

}
