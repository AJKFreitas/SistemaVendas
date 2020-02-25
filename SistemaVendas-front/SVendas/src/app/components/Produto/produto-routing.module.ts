import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProdutoComponent } from './produto/produto.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { GestaoProdutosComponent } from './gestao-produtos/gestao-produtos.component';


const routes: Routes = [
  { path: 'produto', component: ProdutoComponent, canActivate: [AuthGuard] },
  { path: 'gestao-produtos', component: GestaoProdutosComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutoRoutingModule { }
