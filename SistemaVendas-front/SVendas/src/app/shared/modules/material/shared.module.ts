import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { MaterialModule } from './material.module';
import { HasRoleDirective } from '../../utils/has-role.directive';

@NgModule({
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    FlexLayoutModule,
    NgxDatatableModule,
    SelectDropDownModule,
  ],
  declarations: [
    HasRoleDirective,
  ],
  exports: [
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    FlexLayoutModule,
    NgxSpinnerModule,
    NgxDatatableModule,
    SelectDropDownModule,
    HasRoleDirective,
  ]
})
export class SharedModule { }
