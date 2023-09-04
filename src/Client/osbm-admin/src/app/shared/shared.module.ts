import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import * as allComponents from './components';



@NgModule({
  declarations: [...allComponents.components],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [...allComponents.components, FormsModule]
})
export class SharedModule { }
