import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BlankLayoutModule } from './layouts/blank-layout/blank-layout.module';
import { DefaultLayoutModule } from './layouts/default-layout/default-layout.module';
import { SharedModule } from './shared/shared.module';
import { ProductsManagementComponent } from './features/products/products-management/products-management.component';
import { CollectionsManagementComponent } from './features/products/collections-management/collections-management.component';
import { OrdersManagementComponent } from './features/orders/orders-management/orders-management.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductsManagementComponent,
    CollectionsManagementComponent,
    OrdersManagementComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    DefaultLayoutModule,
    BlankLayoutModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
