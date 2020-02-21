import { Fornecedor } from './../model/Fornecedor';
import { Component, OnInit } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';

@Component({
  selector: 'app-listar-fornecedor',
  templateUrl: './listar-fornecedor.component.html',
  styleUrls: ['./listar-fornecedor.component.css']
})
export class ListarFornecedorComponent implements OnInit {
  fornecedores = [];
  columns = [];
  constructor(
    public fornecedorService: FornecedorService,
    private SpinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    )
      {
        this.fornecedores = new Array<any>();
        this.columns = [
          { prop: 'nome' },
          { name: 'Telefone' },
          { name: 'CNPJ' }
        ];
      }


  ngOnInit(): void {
    this.listarFornecedores();
  }

  listarFornecedores() {
    this.SpinnerService.show();
    this.fornecedorService.listarFornecedores().subscribe((res) => {
      if (res.result) {
        this.fornecedores = res;
      }
      this.fornecedores = res;
      this.SpinnerService.hide();
    });
  }

}
