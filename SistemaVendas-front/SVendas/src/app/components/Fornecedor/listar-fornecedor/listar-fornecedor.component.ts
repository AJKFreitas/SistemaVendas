import { Fornecedor } from './../model/Fornecedor';
import { Component, OnInit } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
    private SpinnerService: NgxSpinnerService
    )
      {
        this.fornecedores = new Array<any>();
        this.columns = [
          { id: 'Id' },
          { nome: 'Nome' },
          { telefone: 'Telefone' },
          { cnpj: 'CNPJ' }
        ];
      }


  ngOnInit(): void {
    this.inserirFornecedor();
  }

  inserirFornecedor() {
    this.SpinnerService.show();
    this.fornecedorService.listarFornecedores().subscribe((res) => {
      if (res.result) {
        this.fornecedores = res;
      }
      this.fornecedores = res;
      console.log(res);
      this.SpinnerService.hide();
    });
  }

}
