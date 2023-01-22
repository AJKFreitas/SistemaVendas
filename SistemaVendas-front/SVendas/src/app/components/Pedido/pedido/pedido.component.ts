import { Cliente } from './../../Cliente/model/Cliente';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { ClienteService } from '../../Cliente/service/cliente.service';
import { ItemPedidoVenda, ItemPedidoVendaVM } from '../models/ItemPedidoVenda';
import { ProdutoService } from '../../Produto/services/produto.service';
import { Produto, ProdutoItemPedido } from '../../Produto/model/Produto';
import { PedidoVenda, PedidoVendaVM } from '../models/PedidoVenda';
import { NgxSelectOption } from 'ngx-select-ex';
import { PedidoVendaService } from '../service/pedido-venda.service';
import { isNullOrUndefined } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.css']
})
export class PedidoComponent implements OnInit {


  @ViewChild('input') input: ElementRef;

  pedidoVenda: PedidoVenda = new PedidoVenda();
  itemsPedido: ItemPedidoVenda[] = [];
  pedidoForm: UntypedFormGroup;
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
  itemsPedidoDeleted: Array<ItemPedidoVenda>;
  desconto = 0;
  constructor(
    public fb: UntypedFormBuilder,
    public clienteService: ClienteService,
    public produtoService: ProdutoService,
    public pedidoVendaService: PedidoVendaService,
    public router: Router,
    public spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    private route: Router
  ) {
    this.pedidoVenda.cliente = new Cliente();
    this.pedidoVenda.itemPedidos = new Array<ItemPedidoVenda>();
    this.itemsPedidoDeleted = new Array<ItemPedidoVenda>();
    const nav = this.router.getCurrentNavigation();
    if (!isNullOrUndefined(nav.extras.state)) {
      this.pedidoVenda = nav.extras.state.pedidoVenda;
    }
  }

  ngOnInit(): void {
    this.popularComboClientes();
    this.popularComboProduto();
    if (isNullOrUndefined(this.pedidoVenda.id)) {
      this.adicionarItemPedido();
    } else {
      this.carregarItemsPedido(this.pedidoVenda.itemPedidos);
    }
  }

  selectionChanged(event: NgxSelectOption, index) {
    const produtoSelecionado = event[0].data;
    this.produtoService.estoqueAtual(produtoSelecionado).subscribe(res => {
      this.spinnerService.hide();
      if (!isNullOrUndefined(index)) {
        this.pedidoVenda.itemPedidos[index].estoque = res;
        this.pedidoVenda.itemPedidos[index].preco = produtoSelecionado.valor;
        this.pedidoVenda.itemPedidos[index].precoCompra = produtoSelecionado.valor;
        this.pedidoVenda.itemPedidos[index].produto = produtoSelecionado;
        const quantidade = this.iniciarQuantidade(this.pedidoVenda.itemPedidos[index].quantidade);
        this.pedidoVenda.itemPedidos[index].quantidade = quantidade;
        this.pedidoVenda.itemPedidos[index].subTotal = this.calcularSubTotal(quantidade, produtoSelecionado.valor);
        this.calcularValorTotalDaVenda();
      }
  }, err => {
    this.spinnerService.hide();
    return 0;
  });
  }
  calcularSubTotal(quantidade: number, preco: number): number {
    return quantidade * preco;
  }
  calcularSubTotalDaLinha(index) {
    this.pedidoVenda.itemPedidos[index].subTotal = this.calcularSubTotal(
      this.pedidoVenda.itemPedidos[index].quantidade,
      this.pedidoVenda.itemPedidos[index].preco);
  }
  calcularValorTotalDaVenda() {
    this.pedidoVenda.valorTotal = this.pedidoVenda.itemPedidos
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
  estoqueAtual(produto: Produto) {
    this.spinnerService.show();
    let resEstoqueAtual = 0;
    this.produtoService.estoqueAtual(produto).subscribe(res => {
        this.spinnerService.hide();
        resEstoqueAtual = res;
        return resEstoqueAtual;
    }, err => {
      this.spinnerService.hide();
      return 0;
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
    this.pedidoVenda.itemPedidos.push(new ItemPedidoVenda(null, 0, 0, 0, null, null, 0));
  }
  carregarItemsPedido(itemsPedido: ItemPedidoVenda[]) {
    this.pedidoVenda.itemPedidos = [...itemsPedido];
  }
  removeItemPedido(item: ItemPedidoVenda) {
    debugger;
    if (this.pedidoVenda.itemPedidos.length <= 1) {
      return;
    } else {
      if (this.pedidoVenda.itemPedidos.includes(item)) {
        if (item.id) {
          this.itemsPedidoDeleted.push(item);
        }
        const ITEMPEDIDO = this.pedidoVenda.itemPedidos.findIndex(
          resultado => resultado === item
        );
        this.pedidoVenda.itemPedidos.splice(ITEMPEDIDO, 1);
        this.pedidoVenda.itemPedidos = [... this.pedidoVenda.itemPedidos];
      }
    }
  }
  cancelar() {
    this.pedidoVenda = new PedidoVenda();
    this.route.navigate(['/listar-pedido-venda']);
  }
  lancarPedidoVenda() {

    this.calcularValorTotalDaVenda();
    if (this.pedidoVenda.id) {
      this.editarPedidoVenda(this.pedidoVenda);
    } else {

      this.spinnerService.show();
      const itemPedidoVendaVM = this.pedidoVenda.itemPedidos
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
          this.route.navigate(['/listar-pedido-venda']);
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
  editarPedidoVenda(pedidoVenda: PedidoVenda) {
    this.spinnerService.show();
    debugger;
    const itemPedidoVendaVM = pedidoVenda.itemPedidos
      .map(x => new ItemPedidoVendaVM(
        x.quantidade,
        x.preco,
        x.subTotal,
        x.idProduto,
        x.idPedido,
        x.id));
    const pedidoVendaVm = new PedidoVendaVM(
      this.pedidoVenda.cliente.id,
      itemPedidoVendaVM,
      this.pedidoVenda.valorTotal,
      this.pedidoVenda.dataVenda,
      this.pedidoVenda.id);

    this.pedidoVendaService.editar(pedidoVendaVm).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Pedido alterado com sucesso!');
      this.route.navigate(['/listar-pedido-venda']);
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alterado Pedido!');
      }
    );
  }
}
