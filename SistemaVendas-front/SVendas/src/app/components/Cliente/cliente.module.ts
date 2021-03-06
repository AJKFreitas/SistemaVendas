import { NgModule } from '@angular/core';
import { ClienteRoutingModule } from './cliente-routing.module';
import { ClienteComponent } from './cliente/cliente.component';
import { GestaoClienteComponent } from './gestao-cliente/gestao-cliente.component';
import { ClienteDialogComponent } from './modal/cliente-dialog/cliente-dialog.component';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [
    ClienteDialogComponent,
    ClienteComponent,
    GestaoClienteComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ClienteRoutingModule,
  ],
  entryComponents: [
    ClienteDialogComponent,
  ],
})
export class ClienteModule { }
