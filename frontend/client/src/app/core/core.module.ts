import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './navigation/navigation.component';
import { MaterialModule } from '../shared/material.module';



@NgModule({
  declarations: [NavigationComponent],
  imports: [
    CommonModule,
    MaterialModule,
  ],
  exports: [NavigationComponent]
})
export class CoreModule { }
