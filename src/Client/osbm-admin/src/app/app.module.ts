import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BlankLayoutModule } from './layouts/blank-layout/blank-layout.module';
import { DefaultLayoutModule } from './layouts/default-layout/default-layout.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
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
