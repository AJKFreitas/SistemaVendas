import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { OrdemCompra, OrdemCompraVM } from '../models/OrdemCompra';
import { ItemOrdemCompra, ItemOrdemCompraVM } from '../models/ItemOrdemCompra';
import { UntypedFormGroup, UntypedFormBuilder } from '@angular/forms';
import { Fornecedor } from '../../Fornecedor/model/Fornecedor';
import { ProdutoItemPedido, Produto } from '../../Produto/model/Produto';
import { FornecedorService } from '../../Fornecedor/services/fornecedor.service';
import { ProdutoService } from '../../Produto/services/produto.service';
import { OrdemCompraService } from '../service/ordem-compra.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { isNullOrUndefined } from 'util';
import { NgxSelectOption } from 'ngx-select-ex';

@Component({
  selector: 'app-ordem-compra',
  templateUrl: './ordem-compra.component.html',
  styleUrls: ['./ordem-compra.component.css']
})
export class OrdemCompraComponent implements OnInit {

  @ViewChild('input') input: ElementRef;

  ordemCompra: OrdemCompra = new OrdemCompra();
  itemsOrdemCompra: ItemOrdemCompra[] = [];
  ordemCompraForm: UntypedFormGroup;
  fornecedores: Fornecedor[] = [];
  config = {
    displayKey: 'nome',
    search: true,
    height: 'auto',
    placeholder: 'Select',
    limitTo: this.fornecedores.length,
    moreText: 'more',
    noResultsFound: 'Não ha dados!',
    searchPlaceholder: 'Buscar',
    searchOnKey: 'nome',
    clearOnSelection: false
  };
  produtos: ProdutoItemPedido[] = [];
  itemsOrdemCompraDeleted: Array<ItemOrdemCompra>;
  desconto = 0;
  constructor(
    public fb: UntypedFormBuilder,
    public fornecedoService: FornecedorService,
    public produtoService: ProdutoService,
    public ordemCompraService: OrdemCompraService,
    public router: Router,
    public spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    private route: Router
  ) {
    this.ordemCompra.fornecedor = new Fornecedor();
    this.ordemCompra.itemsOrdemCompra = new Array<ItemOrdemCompra>();
    this.itemsOrdemCompraDeleted = new Array<ItemOrdemCompra>();
    const nav = this.router.getCurrentNavigation();
    if (!isNullOrUndefined(nav.extras.state)) {
      this.ordemCompra = nav.extras.state.ordemCompra;
    }
  }



  ngOnInit(): void {
    this.popularComboFornecedores();
    this.popularComboProduto();
    if (isNullOrUndefined(this.ordemCompra.id)) {
      this.adicionarItemOrdemCompra();
    } else {
      this.carregarItemsOrdemCompra(this.ordemCompra.itemsOrdemCompra);
    }
  }

  selectionChanged(event: NgxSelectOption, index) {
    const produtoSelecionado = event[0].data;
    console.log(produtoSelecionado);
    this.produtoService.estoqueAtual(produtoSelecionado).subscribe(estoqueAtual => {
      if (!isNullOrUndefined(index)) {
        this.ordemCompra.itemsOrdemCompra[index].estoque = estoqueAtual;
        this.ordemCompra.itemsOrdemCompra[index].preco = produtoSelecionado.valor;
        this.ordemCompra.itemsOrdemCompra[index].produto = produtoSelecionado;
        const quantidade = this.iniciarQuantidade(this.ordemCompra.itemsOrdemCompra[index].quantidade);
        this.ordemCompra.itemsOrdemCompra[index].quantidade = quantidade;
        this.ordemCompra.itemsOrdemCompra[index].subTotal = this.calcularSubTotal(quantidade, produtoSelecionado.valor);
        this.calcularValorTotalDaVenda();
      }
    }, err => {
      this.spinnerService.hide();
    });
  }
  calcularSubTotal(quantidade: number, preco: number): number {
    return quantidade * preco;
  }
  calcularSubTotalDaLinha(index) {
    this.ordemCompra.itemsOrdemCompra[index].subTotal = this.calcularSubTotal(
      this.ordemCompra.itemsOrdemCompra[index].quantidade,
      this.ordemCompra.itemsOrdemCompra[index].preco);
  }
  calcularValorTotalDaVenda() {
    this.ordemCompra.valorTotal = this.ordemCompra.itemsOrdemCompra
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
  popularComboFornecedores() {
    this.spinnerService.show();
    this.fornecedoService.listarFornecedores()
      .subscribe(res => {
        if (res.result) {
          this.fornecedores = res;
          this.spinnerService.hide();
        }
        this.fornecedores = res;
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

  adicionarItemOrdemCompra() {
    this.ordemCompra.itemsOrdemCompra.push(new ItemOrdemCompra(null, null, null, 0, 0, 0, null, null));
  }
  carregarItemsOrdemCompra(itemsOrdemCompra: ItemOrdemCompra[]) {
    this.ordemCompra.itemsOrdemCompra = [...itemsOrdemCompra];
  }
  removeItemOrdemCompra(item: ItemOrdemCompra) {
    if (this.ordemCompra.itemsOrdemCompra.length <= 1) {
      return;
    } else {
      if (this.ordemCompra.itemsOrdemCompra.includes(item)) {
        if (item.id) {
          this.itemsOrdemCompraDeleted.push(item);
        }
        const ITEMPEDIDO = this.ordemCompra.itemsOrdemCompra.findIndex(
          resultado => resultado === item
        );
        this.ordemCompra.itemsOrdemCompra.splice(ITEMPEDIDO, 1);
        this.ordemCompra.itemsOrdemCompra = [... this.ordemCompra.itemsOrdemCompra];
      }
    }
  }
  cancelar() {
    this.ordemCompra = new OrdemCompra();
    this.route.navigate(['/listar-pedido-venda']);
  }
  lancarOrdemCompra() {

    this.calcularValorTotalDaVenda();
    if (this.ordemCompra.id) {
      this.editar(this.ordemCompra);
    } else {

      this.spinnerService.show();
      const itemPedidoVendaVM = this.ordemCompra.itemsOrdemCompra
        .map(x => new ItemOrdemCompraVM(
          x.idProduto,
          x.idOrdemCompra,
          x.preco,
          x.quantidade,
          x.subTotal,
        ));
      const ordemCompraVm = new OrdemCompraVM(
        this.ordemCompra.fornecedor.id,
        itemPedidoVendaVM,
        this.ordemCompra.valorTotal);

      this.ordemCompraService.iserir(ordemCompraVm).subscribe((res) => {
        if (res) {
          this.spinnerService.hide();
          this.ordemCompra = new OrdemCompra();
          this.route.navigate(['/listar-ordem-compra']);
          this.toastSevice.Sucesso('Sucesso!', 'Ordem de Compra lançada com sucesso!');
        }
        this.spinnerService.hide();
      },
        err => {
          this.spinnerService.hide();
          this.toastSevice.Erro('Erro ao tentar lançar Ordem de Compra!');
        }
      );
    }
  }
  editar(ordemCompra: OrdemCompra) {
    this.spinnerService.show();
    const itemOrdemCompraVM = ordemCompra.itemsOrdemCompra
      .map(x => new ItemOrdemCompraVM(
        x.idProduto,
        x.idOrdemCompra,
        x.preco,
        x.quantidade,
        x.subTotal,
        x.id));
    const ordemCompraVm = new OrdemCompraVM(
      this.ordemCompra.fornecedor.id,
      itemOrdemCompraVM,
      this.ordemCompra.valorTotal,
      null,
      this.ordemCompra.dataEntrada,
      this.ordemCompra.id);

    this.ordemCompraService.editar(ordemCompraVm).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Ordem de Compra alterada com sucesso!');
      this.route.navigate(['/listar-ordem-compra']);
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alter Ordem de Compra!');
      }
    );
  }

}
