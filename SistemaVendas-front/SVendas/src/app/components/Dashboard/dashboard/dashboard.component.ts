import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { NavigationEnd, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { Cliente } from '../../Cliente/model/Cliente';
import { ItemPedidoVenda } from '../../Pedido/models/ItemPedidoVenda';
import { isNullOrUndefined } from 'util';
import { Produto } from '../../Produto/model/Produto';
import { DashboardService } from '../service/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  title = 'Os 10 produtos mais vendidos do mês';
  type = 'PieChart';
  data: Array<any>;
  columnNames = ['Produto', '%'];
  options = {
  };
  width = 850;
  height = 400;
  constructor(
    private dashboardService: DashboardService,
    public router: Router,
    public spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    private route: Router
  ) {
    const nav = this.router.getCurrentNavigation();
    this.data = new Array<any>();
  }
  ngOnInit(): void {
    this.CarregarProdutosMaisVendidos();
  }

  CarregarProdutosMaisVendidos() {
    this.spinnerService.show();
    this.dashboardService.ProdutosMaisVendidos().subscribe(produtos => {
        this.spinnerService.hide();
        this.data = produtos;
        console.log(this.data);
    }, err => {
      this.spinnerService.hide();
    });
  }
}
