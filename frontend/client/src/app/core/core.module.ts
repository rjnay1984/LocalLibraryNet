import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { MaterialModule } from '../shared/material.module';
import { NavigationComponent } from './navigation/navigation.component';



@NgModule({
  declarations: [NavigationComponent],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule
  ],
  exports: [NavigationComponent]
})
export class CoreModule { }
