import { NgModule, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { UsuarioComponent } from '../usuario/usuario.component';
import { ListarUsuarioComponent } from '../listar-usuario/listar-usuario.component';
import { ModalComponent } from '../modal/modal.component';
import { UsuarioService } from '../services/usuario.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { MaterialModule } from 'src/app/shared/modules/material/material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { routing } from '../../app.routing';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastContainerModule, ToastrModule } from 'ngx-toastr';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SelectDropDownModule } from 'ngx-select-dropdown';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { options } from 'src/app/app.module';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from 'src/app/app.component';
import { SigninComponent } from '../../Auth/signin/signin.component';
import { SignupComponent } from '../../Auth/signup/signup.component';
import { UserProfileComponent } from '../../Auth/user-profile/user-profile.component';
import { FornecedorComponent } from '../../Fornecedor/fornecedor/fornecedor.component';
import { ProdutoComponent } from '../../Produto/produto/produto.component';
import { PedidoComponent } from '../../Pedido/pedido/pedido.component';
import { ClienteComponent } from '../../Cliente/cliente/cliente.component';
import { PerfilComponent } from '../../Perfil/perfil/perfil.component';
import { RelatoriosComponent } from '../../Relatorios/relatorios/relatorios.component';
import { DashboardComponent } from '../../Dashboard/dashboard/dashboard.component';
import { ListarFornecedorComponent } from '../../Fornecedor/listar-fornecedor/listar-fornecedor.component';
import { DialogBoxComponent } from '../../Shared/dialog-box/dialog-box.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { AuthInterceptor } from '../../Auth/shared/config/authconfig.interceptor';
import { ErrorInterceptor } from '../../Auth/shared/config/authconfigerror.Interceptor';
import { AuthGuard } from '../../Auth/shared/guard/auth.guard';
import { AuthService } from '../../Auth/shared/services/auth.service';
import { ToastService } from '../../Shared/ToastService';


@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    SignupComponent,
    UserProfileComponent,
    UsuarioComponent,
    PerfilComponent,
    ModalComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    FlexLayoutModule,
    MatPaginatorModule,
    MatSelectModule,
    MaterialModule,
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
    DialogBoxComponent,
    ModalComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true, },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true, },
    {provide: DEFAULT_CURRENCY_CODE, useValue: 'pt-BR'},

    AuthGuard,
    AuthService,
    ToastService,
    UsuarioService
  ],
})
export class UsuarioModule { }
