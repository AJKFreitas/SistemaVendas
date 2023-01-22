import { NgModule } from '@angular/core';
import { UsuarioRoutingModule } from './usuario-routing.module';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { UsuarioComponent } from './usuario/usuario.component';
import { ModalComponent } from './modal/modal.component';
import { ListarUsuarioComponent } from './listar-usuario/listar-usuario.component';
import { CommonModule } from '@angular/common';


@NgModule({
    declarations: [
        ModalComponent,
        UsuarioComponent,
        ListarUsuarioComponent,
    ],
    imports: [
        CommonModule,
        UsuarioRoutingModule,
        SharedModule,
    ]
})
export class UsuarioModule { }
