import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';
import { PedidoComponent } from './components/Pedido/pedido/pedido.component';
import { PerfilComponent } from './components/Perfil/perfil/perfil.component';


const routes: Routes = [
  { path: '', redirectTo: '/log-in', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'log-in', component: SigninComponent },
  { path: 'pedido', component: PedidoComponent, canActivate: [AuthGuard] },
  { path: 'perfil', component: PerfilComponent, canActivate: [AuthGuard] },
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
