import { Cliente } from './../../Cliente/model/Cliente';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FornecedorService } from '../../Fornecedor/services/fornecedor.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { ClienteService } from '../../Cliente/service/cliente.service';
import { ItemPedidoVenda } from '../models/ItemPedidoVenda';
import { ProdutoService } from '../../Produto/services/produto.service';
import { Produto, ProdutoItemPedido } from '../../Produto/model/Produto';
import { PedidoVenda } from '../models/PedidoVenda';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {
  pedidoVenda: PedidoVenda = new PedidoVenda();
  itemsPedido: ItemPedidoVenda[] = [];
  pedidoForm: FormGroup;
  clientes: Cliente[] = [];
  config = {
    displayKey: 'nome',
    search: true,
    height: 'auto',
    placeholder: 'Select',
    limitTo: this.clientes.length,
    moreText: 'more',
    noResultsFound: 'Não ha dados!',
    searchPlaceholder: 'Buscar',
    searchOnKey: 'nome',
    clearOnSelection: false
  };
  produtos: ProdutoItemPedido[] = [];
  itemsPedidoDeleted: any;

  constructor(
    public fb: FormBuilder,
    public clienteService: ClienteService,
    public produtoService: ProdutoService,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService
  ) {
    this.pedidoVenda.cliente = new Cliente();
    this.pedidoVenda.itemsPedido = new Array<ItemPedidoVenda>();
  }

  ngOnInit(): void {
    this.popularComboClientes();
    this.popularComboProduto();
    this.adicionarItemPedido();
  }

  selectionChanged($event, index) {
    if (!isNullOrUndefined(index)) {
      this.itemsPedido[index].estoque = this.estoqueAtual($event.value);
    }
  }

  estoqueAtual(produto: Produto): any {
    this.spinnerService.show();
    this.produtoService.estoqueAtual(produto).subscribe(res => {
      if (res) {
        this.spinnerService.hide();
        return res.estoque;
      }
    }, err => {
      this.spinnerService.hide();
    });
  }
  popularComboClientes() {
    this.spinnerService.show();
    this.clienteService.listarClientes()
      .subscribe(res => {
        if (res.result) {
          this.clientes = res;
        }
        this.clientes = res;
        this.spinnerService.hide();
      }, err => {
        this.spinnerService.hide();
      });
  }
  popularComboProduto() {
    this.spinnerService.show();
    this.produtoService.listarProdutoss()
      .subscribe(res => {
        if (res.result) {
          this.produtos = res;
        }
        this.produtos = res;
        this.spinnerService.hide();
      }, err => {
        this.spinnerService.hide();
      });
  }

  adicionarItemPedido() {
    this.pedidoVenda.itemsPedido.push(new ItemPedidoVenda(null, 0, 0, 0, null, null));
  }
  removeItemPedido(item: ItemPedidoVenda) {
    if ( this.pedidoVenda.itemsPedido.length <= 1) {
      return;
    } else {
      if (  this.pedidoVenda.itemsPedido.includes(item)) {
        if (item.id) {
          this.itemsPedidoDeleted.push(item);
        }
        const ITEMPEDIDO =  this.pedidoVenda.itemsPedido.findIndex(
          resultado => resultado === item
        );
        this.pedidoVenda.itemsPedido.splice(ITEMPEDIDO, 1);
        this.pedidoVenda.itemsPedido = [... this.pedidoVenda.itemsPedido];
      }
    }
  }
}
