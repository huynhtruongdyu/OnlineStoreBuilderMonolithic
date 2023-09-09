import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthComponent } from 'src/app/features/auth/auth.component';
import { BlankLayoutComponent } from './blank-layout.component';



@NgModule({
  declarations: [
    BlankLayoutComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
  ]
})
export class BlankLayoutModule { }
