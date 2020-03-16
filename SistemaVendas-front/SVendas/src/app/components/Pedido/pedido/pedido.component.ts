import { Cliente } from './../../Cliente/model/Cliente';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FornecedorService } from '../../Fornecedor/services/fornecedor.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { ClienteService } from '../../Cliente/service/cliente.service';
import { ItemPedidoVenda, ItemPedidoVendaVM } from '../models/ItemPedidoVenda';
import { ProdutoService } from '../../Produto/services/produto.service';
import { Produto, ProdutoItemPedido } from '../../Produto/model/Produto';
import { PedidoVenda, PedidoVendaVM } from '../models/PedidoVenda';
import { isNullOrUndefined } from 'util';
import { NgxSelectOption } from 'ngx-select-ex';
import { PedidoVendaService } from '../service/pedido-venda.service';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {


  @ViewChild('input') input: ElementRef;

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
  desconto = 0;
  constructor(
    public fb: FormBuilder,
    public clienteService: ClienteService,
    public produtoService: ProdutoService,
    public pedidoVendaService: PedidoVendaService,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService
  ) {
    this.pedidoVenda.cliente = new Cliente();
    this.pedidoVenda.itemsPedido = new Array<ItemPedidoVenda>();
  }

  ngOnInit(): void {
    this.popularComboClientes();
    this.popularComboProduto();
    this.adicionarItemPedido();
  }

  selectionChanged(event: NgxSelectOption, index) {
    console.log(event[0]);
    console.log(index);
    const produtoSelecionado = event[0].data;
    if (!isNullOrUndefined(index)) {
      this.pedidoVenda.itemsPedido[index].estoque = this.estoqueAtual(produtoSelecionado);
      this.pedidoVenda.itemsPedido[index].preco = produtoSelecionado.valor;
      this.pedidoVenda.itemsPedido[index].precoCompra = produtoSelecionado.valor;
      this.pedidoVenda.itemsPedido[index].produto = produtoSelecionado;
      const quantidade = this.iniciarQuantidade(this.pedidoVenda.itemsPedido[index].quantidade);
      this.pedidoVenda.itemsPedido[index].quantidade = quantidade;
      this.pedidoVenda.itemsPedido[index].subTotal = this.calcularSubTotal(quantidade, produtoSelecionado.valor);
      this.calcularValorTotalDaVenda();
    }
  }
  calcularSubTotal(quantidade: number, preco: number): number {
    return quantidade * preco;
  }
  calcularSubTotalDaLinha(index) {
    this.pedidoVenda.itemsPedido[index].subTotal = this.calcularSubTotal(
      this.pedidoVenda.itemsPedido[index].quantidade,
      this.pedidoVenda.itemsPedido[index].preco);
  }
  calcularValorTotalDaVenda() {
    this.pedidoVenda.valorTotal = this.pedidoVenda.itemsPedido
      .map(p => p.subTotal)
      .reduce((subTotal1, subTotal2) => subTotal1 + subTotal2) - this.desconto;
  }

  iniciarQuantidade(quantidade: number): number {
    if (quantidade < 1) {
      return 1;
    } else {
      return quantidade;
    }
  }
  estoqueAtual(produto: Produto): any {
    this.spinnerService.show();
    this.produtoService.estoqueAtual(produto).subscribe(res => {
      if (res) {
        this.spinnerService.hide();
        return res.estoque || 0;
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
          this.spinnerService.hide();
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
    this.pedidoVenda.itemsPedido.push(new ItemPedidoVenda(null, 0, 0, 0, null, null, 0));
  }
  removeItemPedido(item: ItemPedidoVenda) {
    if (this.pedidoVenda.itemsPedido.length <= 1) {
      return;
    } else {
      if (this.pedidoVenda.itemsPedido.includes(item)) {
        if (item.id) {
          this.itemsPedidoDeleted.push(item);
        }
        const ITEMPEDIDO = this.pedidoVenda.itemsPedido.findIndex(
          resultado => resultado === item
        );
        this.pedidoVenda.itemsPedido.splice(ITEMPEDIDO, 1);
        this.pedidoVenda.itemsPedido = [... this.pedidoVenda.itemsPedido];
      }
    }
  }

  lancarPedidoVenda() {
    this.calcularValorTotalDaVenda();
    this.spinnerService.show();
    const itemPedidoVendaVM = this.pedidoVenda.itemsPedido
      .map(x => new ItemPedidoVendaVM(
        x.quantidade,
        x.preco,
        x.subTotal,
        x.idProduto,
        x.idPedido));
    const pedidoVendaVm = new PedidoVendaVM(
      this.pedidoVenda.cliente.id,
      itemPedidoVendaVM,
      this.pedidoVenda.valorTotal);
    this.pedidoVendaService.iserir(pedidoVendaVm).subscribe((res) => {
      if (res) {
        this.spinnerService.hide();
        this.pedidoVenda = new PedidoVenda();
        this.toastSevice.Sucesso('Sucesso!', 'Pedido lançado com sucesso!');
      }
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar lançar Pedido!');
      }
    );
  }
}
