import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { LoginComponent } from './account/login/login.component';
import { MaterialModule } from './material.module';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    CoreModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  exports: [LoginComponent]
})
export class SharedModule { }
