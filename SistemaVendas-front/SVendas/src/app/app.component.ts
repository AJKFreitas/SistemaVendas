import { Component, OnInit } from '@angular/core';
import { AuthService } from './components/Auth/shared/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'SVendas';
  mostrarMenu = false;
  constructor(public authService: AuthService) {
  }
  ngOnInit(): void {
    this.authService.mostrarMenuEmitter.subscribe(
      mostar => this.mostrarMenu = mostar
    );
  }

  logout() {
    this.authService.doLogout();
  }
}
