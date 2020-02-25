import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteComponent } from './cliente/cliente.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { GestaoClienteComponent } from './gestao-cliente/gestao-cliente.component';


const routes: Routes = [
  { path: 'cliente', component: ClienteComponent, canActivate: [AuthGuard] },
  { path: 'gestao-cliente', component: GestaoClienteComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClienteRoutingModule { }
