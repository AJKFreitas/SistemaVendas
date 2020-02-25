import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from '../Auth/shared/guard/auth.guard';


const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent , canActivate: [AuthGuard], runGuardsAndResolvers: 'always'},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
