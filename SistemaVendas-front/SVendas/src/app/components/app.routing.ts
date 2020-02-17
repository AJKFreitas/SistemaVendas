import { Routes, RouterModule } from '@angular/router';
import { FornecedorComponent } from './Fornecedor/fornecedor/fornecedor.component';
import { ModuleWithProviders } from '@angular/core';
import { PedidoComponent } from './Pedido/pedido/pedido.component';
import { ClienteComponent } from './Cliente/cliente/cliente.component';
import { ProdutoComponent } from './Produto/produto/produto.component';
import { UsuarioComponent } from './Usuario/usuario/usuario.component';
import { PerfilComponent } from './Perfil/perfil/perfil.component';
import { RelatoriosComponent } from './Relatorios/relatorios/relatorios.component';
import { DashboardComponent } from './Dashboard/dashboard/dashboard.component';

const APP_ROUTES: Routes = [
    { path: 'fornecedor', component: FornecedorComponent },
    { path: 'pedido', component: PedidoComponent },
    { path: 'cliente', component: ClienteComponent },
    { path: 'produto', component: ProdutoComponent },
    { path: 'usuario', component: UsuarioComponent },
    { path: 'perfil', component: PerfilComponent },
    { path: 'relatorio', component: RelatoriosComponent },
    { path: 'dashboard', component: DashboardComponent },
];

export const routing: ModuleWithProviders = RouterModule.forRoot(APP_ROUTES);
