import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RelatoriosComponent } from './relatorios/relatorios.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';


const routes: Routes = [
  { path: 'relatorio', component: RelatoriosComponent, canActivate: [AuthGuard] },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RelatoriosRoutingModule { }
