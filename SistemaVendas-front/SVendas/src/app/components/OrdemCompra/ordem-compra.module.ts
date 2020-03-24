import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { OrdemCompraRoutingModule } from './ordem-compra-routing.module';
import { NgxSelectModule, INgxSelectOptions } from 'ngx-select-ex';
import { OrdemCompraComponent } from './ordem-compra/ordem-compra.component';
import { ListarOrdemCompraComponent } from './listar-ordem-compra/listar-ordem-compra.component';

const CustomSelectOptions: INgxSelectOptions = {
  keepSelectedItems: true,
  optionValueField: 'id',
  optionTextField: 'nome'
};

@NgModule({
  declarations: [
    OrdemCompraComponent,
    ListarOrdemCompraComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    OrdemCompraRoutingModule,
    NgxSelectModule.forRoot(CustomSelectOptions)
  ]
})
export class OrdemCompraModule { }
