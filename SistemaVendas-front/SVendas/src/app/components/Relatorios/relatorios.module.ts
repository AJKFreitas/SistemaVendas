import { NgModule } from '@angular/core';
import { RelatoriosRoutingModule } from './relatorios-routing.module';
import { RelatoriosComponent } from './relatorios/relatorios.component';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    RelatoriosComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RelatoriosRoutingModule
  ]
})
export class RelatoriosModule { }
