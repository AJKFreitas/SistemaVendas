import {ReactiveFormsModule, FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, DEFAULT_CURRENCY_CODE } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './components/Auth/shared/config/authconfig.interceptor';
import { SigninComponent } from './components/Auth/signin/signin.component';
import { SignupComponent } from './components/Auth/signup/signup.component';
import { UserProfileComponent } from './components/Auth/user-profile/user-profile.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AuthGuard } from './components/Auth/shared/guard/auth.guard';
import { AuthService } from './components/Auth/shared/services/auth.service';
import { ProdutoComponent } from './components/Produto/produto/produto.component';
import { PedidoComponent } from './components/Pedido/pedido/pedido.component';
import { ClienteComponent } from './components/Cliente/cliente/cliente.component';
import { UsuarioComponent } from './components/Usuario/usuario/usuario.component';
import { routing } from './components/app.routing';
import { FornecedorComponent } from './components/Fornecedor/fornecedor/fornecedor.component';
import { PerfilComponent } from './components/Perfil/perfil/perfil.component';
import { RelatoriosComponent } from './components/Relatorios/relatorios/relatorios.component';
import { DashboardComponent } from './components/Dashboard/dashboard/dashboard.component';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastContainerModule } from 'ngx-toastr';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ListarFornecedorComponent } from './components/Fornecedor/listar-fornecedor/listar-fornecedor.component';
import { ToastService } from './components/Shared/ToastService';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { ErrorInterceptor } from './components/Auth/shared/config/authconfigerror.Interceptor';
import { MatRadioModule } from '@angular/material/radio';
import { ListarUsuarioComponent } from './components/Usuario/listar-usuario/listar-usuario.component';
import { MatTableModule} from '@angular/material/table';
import { MatDialogModule} from '@angular/material/dialog';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule} from '@angular/material/input';
import { MatButtonModule} from '@angular/material/button';
import { DialogBoxComponent } from './components/Shared/dialog-box/dialog-box.component';
import {MatPaginatorModule} from '@angular/material/paginator';
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/br';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import {MatSelectModule} from '@angular/material/select';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};

registerLocaleData(localeFr, 'pt-BR');
@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    SignupComponent,
    UserProfileComponent,
    FornecedorComponent,
    ProdutoComponent,
    PedidoComponent,
    ClienteComponent,
    UsuarioComponent,
    PerfilComponent,
    RelatoriosComponent,
    DashboardComponent,
    ListarFornecedorComponent,
    ListarUsuarioComponent,
    DialogBoxComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatRadioModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FlexLayoutModule,
    MatPaginatorModule,
    MatSelectModule,
    routing,
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
    } ),
    ToastContainerModule,
    NgxDatatableModule,
    SelectDropDownModule,
    NgxMaskModule.forRoot(options)
  ],
  entryComponents: [
    DialogBoxComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true, },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true, },
    {provide: DEFAULT_CURRENCY_CODE, useValue: 'pt-BR'},

    AuthGuard,
    AuthService,
    ToastService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
