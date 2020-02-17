import { Usuario } from './components/Auth/shared/models/User';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from './components/Auth/shared/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @ViewChild(ToastContainerDirective, {static: true}) toastContainer: ToastContainerDirective;

  title = 'SVendas';
  token: any;
  user: Usuario;
  mostrarMenu = false;
  constructor(
    public authService: AuthService,
    public jwtHelper: JwtHelperService,
    private toastrService: ToastrService) {
    this.token = this.jwtHelper.decodeToken(this.authService.getToken());
    }
  ngOnInit(): void {
    this.toastrService.overlayContainer = this.toastContainer;
    console.log(JSON.parse(this.token.Data));
    this.authService.setProfileContext(JSON.parse(this.token.Data).Nome);
    this.authService.mostrarMenuEmitter.subscribe(
      mostar => this.mostrarMenu = mostar
    );
  }

  logout() {
    this.authService.doLogout();
  }
}
