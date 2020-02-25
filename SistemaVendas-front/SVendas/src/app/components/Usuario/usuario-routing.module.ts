import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsuarioComponent } from './usuario/usuario.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { ListarUsuarioComponent } from './listar-usuario/listar-usuario.component';
import { SigninComponent } from '../Auth/signin/signin.component';


const routes: Routes = [
  { path: 'usuario/:id', component: UsuarioComponent , canActivate: [AuthGuard]},
  { path: 'listar-usuario', component: ListarUsuarioComponent , canActivate: [AuthGuard]},
  { path: 'log-in', component: SigninComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule { }
