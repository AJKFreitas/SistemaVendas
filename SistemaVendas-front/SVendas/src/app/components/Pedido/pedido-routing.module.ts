import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PedidoComponent } from './pedido/pedido.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { ListarPedidoVendaComponent } from './listar-pedido-venda/listar-pedido-venda.component';


const routes: Routes = [
  { path: 'pedido', component: PedidoComponent, canActivate: [AuthGuard] },
  { path: 'listar-pedido-venda', component: ListarPedidoVendaComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PedidoRoutingModule { }
