import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PedidoRoutingModule } from './pedido-routing.module';
import { PedidoComponent } from './pedido/pedido.component';
import { NgxSelectModule, INgxSelectOptions } from 'ngx-select-ex';

const CustomSelectOptions: INgxSelectOptions = {
  keepSelectedItems: true, // Check the interface for more options
  optionValueField: 'id',
  optionTextField: 'nome'
};
@NgModule({
  declarations: [
    PedidoComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PedidoRoutingModule,
    NgxSelectModule.forRoot(CustomSelectOptions)
  ]
})
export class PedidoModule { }
