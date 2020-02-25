import { NgModule } from '@angular/core';
import { RelatoriosRoutingModule } from './relatorios-routing.module';
import { RelatoriosComponent } from './relatorios/relatorios.component';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';


@NgModule({
  declarations: [
    RelatoriosComponent
  ],
  imports: [
    SharedModule,
    RelatoriosRoutingModule
  ]
})
export class RelatoriosModule { }
