import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { CollectionManagementComponent } from './products/collection-management/collection-management.component';
import { ProductManagementComponent } from './products/product-management/product-management.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    ProductManagementComponent,
    CollectionManagementComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class FeaturesModule { }
