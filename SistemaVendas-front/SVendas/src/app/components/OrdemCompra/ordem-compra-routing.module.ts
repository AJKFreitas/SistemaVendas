import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { OrdemCompraComponent } from './ordem-compra/ordem-compra.component';
import { ListarOrdemCompraComponent } from './listar-ordem-compra/listar-ordem-compra.component';

const routes: Routes = [
  { path: 'ordem', component: OrdemCompraComponent, canActivate: [AuthGuard] },
  { path: 'listar-ordem-compra', component: ListarOrdemCompraComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrdemCompraRoutingModule { }
