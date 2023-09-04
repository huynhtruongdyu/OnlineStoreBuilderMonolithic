import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/core/models/products/product.model';
import { ProductsService } from 'src/app/core/services/products.service';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.scss']
})
export class ProductManagementComponent implements OnInit, OnDestroy {
  products: Product[] = [];

  private getAllProductSubscription?: Subscription;

  constructor(private productService: ProductsService) { }

  ngOnDestroy(): void {
    this.getAllProductSubscription?.unsubscribe();
  }

  ngOnInit(): void {
    this.getAllProductSubscription = this.productService.getAll()
      .subscribe({
        next: (resp) => {
          this.products = resp.data as Product[]
        }
      })
  }
}
