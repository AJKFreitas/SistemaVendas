import { PedidoModule } from './components/Pedido/pedido.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './components/Auth/shared/config/authconfig.interceptor';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';
import { AuthService } from './components/Auth/shared/services/auth.service';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule, ToastContainerModule } from 'ngx-toastr';
import { ToastService } from './components/Shared/ToastService';
import { ErrorInterceptor } from './components/Auth/shared/config/authconfigerror.Interceptor';
import { registerLocaleData } from '@angular/common';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { UsuarioService } from './components/Usuario/services/usuario.service';
import { ClienteModule } from './components/Cliente/cliente.module';
import { DashboardModule } from './components/Dashboard/dashboard.module';
import { FornecedorModule } from './components/Fornecedor/fornecedor.module';
import { SharedModule } from './shared/modules/material/shared.module';
import { ProdutoModule } from './components/Produto/produto.module';
import { UsuarioModule } from './components/Usuario/usuario.module';
import { RelatoriosModule } from './components/Relatorios/relatorios.module';
import localeFr from '@angular/common/locales/br';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};

registerLocaleData(localeFr, 'pt-BR');
@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedModule,
    AppRoutingModule,
    HttpClientModule,
    JwtModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('access_token');
        }
      }
    }),
    NgxSpinnerModule,
    ToastrModule.forRoot(
      {
        timeOut: 10000,
        positionClass: 'top-right',
        closeButton: true,
      }),
    NgxMaskModule.forRoot(options),
    ToastContainerModule,
    ClienteModule,
    DashboardModule,
    FornecedorModule,
    ProdutoModule,
    UsuarioModule,
    RelatoriosModule,
    PedidoModule,
  ], exports: [


  ],
  entryComponents: [
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true, },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true, },
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'pt-BR' },

    AuthGuard,
    AuthService,
    ToastService,
    UsuarioService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
