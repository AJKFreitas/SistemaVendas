import { NgModule } from '@angular/core';
import { FornecedorRoutingModule } from './fornecedor-routing.module';
import { FornecedorComponent } from './fornecedor/fornecedor.component';
import { FornecedorDialogComponent } from './modal/fornecedor-dialog/fornecedor-dialog.component';
import { ListarFornecedorComponent } from './listar-fornecedor/listar-fornecedor.component';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { CommonModule } from '@angular/common';


@NgModule({
    declarations: [
        FornecedorDialogComponent,
        FornecedorComponent,
        ListarFornecedorComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        FornecedorRoutingModule,
    ]
})
export class FornecedorModule { }
