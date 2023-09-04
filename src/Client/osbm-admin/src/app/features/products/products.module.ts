import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ProductManagementComponent } from './components/product-management/product-management.component';



@NgModule({
  declarations: [
    ProductManagementComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  exports: [ProductManagementComponent]
})
export class ProductsModule { }
