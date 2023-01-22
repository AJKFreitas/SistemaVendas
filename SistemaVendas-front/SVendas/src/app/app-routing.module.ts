import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';


const routes: Routes = [
  {path: '',   redirectTo: '/log-in', pathMatch: 'full'},

  { path: 'log-in', component: SigninComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
