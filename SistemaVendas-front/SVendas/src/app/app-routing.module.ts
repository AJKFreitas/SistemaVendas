import { ListarFornecedorComponent } from './components/Fornecedor/listar-fornecedor/listar-fornecedor.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { SignupComponent } from './components/Auth/signup/signup.component';
import { UserProfileComponent } from './components/Auth/user-profile/user-profile.component';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';
import { FornecedorComponent } from './components/Fornecedor/fornecedor/fornecedor.component';
import { PedidoComponent } from './components/Pedido/pedido/pedido.component';
import { ClienteComponent } from './components/Cliente/cliente/cliente.component';
import { ProdutoComponent } from './components/Produto/produto/produto.component';
import { UsuarioComponent } from './components/Usuario/usuario/usuario.component';
import { PerfilComponent } from './components/Perfil/perfil/perfil.component';
import { RelatoriosComponent } from './components/Relatorios/relatorios/relatorios.component';
import { DashboardComponent } from './components/Dashboard/dashboard/dashboard.component';


const routes: Routes = [
  { path: '', redirectTo: '/log-in', pathMatch: 'full' },
  { path: 'log-in', component: SigninComponent },
  { path: 'sign-up', component: SignupComponent },
  { path: 'user-profile/:id', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'user-profile', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'fornecedor', component: FornecedorComponent, canActivate: [AuthGuard] },
  { path: 'fornecedor-listar', component: ListarFornecedorComponent, canActivate: [AuthGuard] },
  { path: 'pedido', component: PedidoComponent, canActivate: [AuthGuard] },
  { path: 'cliente', component: ClienteComponent, canActivate: [AuthGuard] },
  { path: 'produto', component: ProdutoComponent, canActivate: [AuthGuard] },
  { path: 'usuario', component: UsuarioComponent , canActivate: [AuthGuard]},
  { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard] },
  { path: 'relatorio', component: RelatoriosComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent , canActivate: [AuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
