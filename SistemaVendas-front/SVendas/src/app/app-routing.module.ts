import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';
import { PedidoComponent } from './components/Pedido/pedido/pedido.component';
import { PerfilComponent } from './components/Perfil/perfil/perfil.component';


const routes: Routes = [
  { path: '', redirectTo: '/log-in', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'log-in', component: SigninComponent },
 
  { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard] },
  // { path: 'usuario', loadChildren: () => import('./components/Usuario/usuario.module').then(m => m.UsuarioModule) },
  // { path: 'gestao-usuarios', loadChildren: () => import('./components/Usuario/usuario.module').then(m => m.UsuarioModule) },
  //  { path: 'dashboard', loadChildren: () => import('./components/Dashboard/dashboard.module').then(dm => dm.DashboardModule) },
  //  { path: 'gestao-produtos', loadChildren: () => import('./components/Produto/produto.module').then(pm => pm.ProdutoModule) },
  //  { path: 'gestao-clientes', loadChildren: () => import('./components/Cliente/cliente.module').then(cm => cm.ClienteModule) },
  //  { path: 'gestao-fornecedores', loadChildren: () => import('./components/Fornecedor/fornecedor.module').then(fm => fm.FornecedorModule)},

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
