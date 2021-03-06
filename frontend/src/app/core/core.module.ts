import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from '../app-routing.module';

import { MaterialModule } from '../shared/material.module';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { NavigationComponent } from './navigation/navigation.component';

@NgModule({
  declarations: [NavigationComponent],
  imports: [AppRoutingModule, CommonModule, MaterialModule, HttpClientModule],
  exports: [NavigationComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
})
export class CoreModule {}
