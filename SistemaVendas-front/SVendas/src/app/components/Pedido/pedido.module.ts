import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PedidoRoutingModule } from './pedido-routing.module';
import { PedidoComponent } from './pedido/pedido.component';

@NgModule({
  declarations: [
    PedidoComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    PedidoRoutingModule,
  ]
})
export class PedidoModule { }
