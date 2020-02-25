import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FornecedorComponent } from './fornecedor/fornecedor.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { ListarFornecedorComponent } from './listar-fornecedor/listar-fornecedor.component';


const routes: Routes = [
  { path: 'fornecedor', component: FornecedorComponent, canActivate: [AuthGuard] },
  { path: 'fornecedor-listar', component: ListarFornecedorComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FornecedorRoutingModule { }
