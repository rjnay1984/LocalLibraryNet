import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { MaterialModule } from '../shared/material.module';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { NavigationComponent } from './navigation/navigation.component';



@NgModule({
  declarations: [NavigationComponent],
  imports: [
    CommonModule,
    MaterialModule,
    HttpClientModule
  ],
  exports: [NavigationComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ]
})
export class CoreModule { }
