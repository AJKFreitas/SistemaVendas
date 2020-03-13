import { Usuario } from './components/Auth/shared/models/User';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from './components/Auth/shared/services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastContainerDirective, ToastrService } from 'ngx-toastr';
import { MensagemPopUPService } from './components/Shared/ToastService';

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
    private toasteSevice: MensagemPopUPService) {
    this.token = this.jwtHelper.decodeToken(this.authService.getToken());
    }
  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.authService.setProfileContext(JSON.parse(this.token.Data).Nome);
    } else {
      this.toasteSevice.Erro('Usuario e senha invalidos!');
      this.logout();
    }
    this.authService.mostrarMenuEmitter.subscribe(
      mostar => this.mostrarMenu = mostar
    );
  }

  logout() {
    this.authService.doLogout();
  }
}
