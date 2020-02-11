import { Component } from '@angular/core';
import { AuthService } from './components/Auth/shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SVendas';
  constructor(public authService: AuthService) { }

  logout() {
    this.authService.doLogout();
  }
}
