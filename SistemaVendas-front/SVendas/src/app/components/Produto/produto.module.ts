import { NgModule } from '@angular/core';

import { ProdutoRoutingModule } from './produto-routing.module';
import { SharedModule } from 'src/app/shared/modules/material/shared.module';
import { ProdutoComponent } from './produto/produto.component';
import { ProdutoDialogComponent } from './modal/produto-dialog/produto-dialog.component';
import { GestaoProdutosComponent } from './gestao-produtos/gestao-produtos.component';
import { CommonModule } from '@angular/common';


@NgModule({
    declarations: [
        ProdutoDialogComponent,
        GestaoProdutosComponent,
        ProdutoComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        ProdutoRoutingModule
    ]
})
export class ProdutoModule { }
