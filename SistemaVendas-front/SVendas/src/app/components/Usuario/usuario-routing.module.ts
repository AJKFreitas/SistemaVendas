import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsuarioComponent } from './usuario/usuario.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';
import { ListarUsuarioComponent } from './listar-usuario/listar-usuario.component';


const routes: Routes = [
  // {
  //   path: '',
  //   children: [
      { path: 'usuario', component: UsuarioComponent, canActivate: [AuthGuard] },
      { path: 'gestao-usuarios', component: ListarUsuarioComponent, canActivate: [AuthGuard] },
  //   ]
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsuarioRoutingModule { }
