import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { HomeComponent } from 'src/app/features/home/home.component';
import { DefaultLayoutComponent } from './default-layout.component';



@NgModule({
  declarations: [
    DefaultLayoutComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    CoreModule
  ]
})
export class DefaultLayoutModule { }
