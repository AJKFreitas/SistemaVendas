import { ProdutoService } from './../services/produto.service';
import { Fornecedor } from './../../Fornecedor/model/Fornecedor';
import { Component, OnInit } from '@angular/core';
import { Form, FormGroup, FormBuilder, FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { FornecedorService } from '../../Fornecedor/services/fornecedor.service';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {
  config = {
    displayKey: 'nome',
    search: true,
    height: '150px',
    placeholder: 'Selecione o fornecedor',
    customComparator: () => {},
    moreText: 'more',
    noResultsFound: 'Nenhum Resultado encontrado!',
    searchPlaceholder: 'Buscar',
    searchOnKey: 'nome',
    clearOnSelection: false
  };

  produtoForm: FormGroup;
  fornecedores: Fornecedor[] = [];
  constructor(
    public formBuilder: FormBuilder,
    public router: Router,
    public fornecedorService: FornecedorService,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    private produtoService: ProdutoService
  ) {
    this.produtoForm = this.formBuilder.group({
      nome: [''],
      descricao: [''],
      valor: [''],
      fornecedor: ['']
    });
  }

  ngOnInit(): void {
    this.popularComboFornecedores();
  }

  selectionChanged($event: Event) {
    console.log(event);
  }

  popularComboFornecedores() {
    this.spinnerService.show();
    this.fornecedorService.listarFornecedores().subscribe(res => {
      if (res.result) {
        this.fornecedores = res;
      }
      this.fornecedores = res;
      this.spinnerService.hide();
    });
  }
iserirProduto(){
  this.spinnerService.show();
  this.produtoService.iserir(this.produtoForm.value).subscribe((res) => {
      if (res.result) {
        this.produtoForm.reset();
        this.formBuilder = new  FormBuilder();
        this.toastSevice.Success('Sucesso!', 'Produto incluido com Sucesso!');
      }
      this.spinnerService.hide();
    },
    err => {
      this.spinnerService.hide();
      this.toastSevice.Error('Erro ao tentar cadastar Produto!');
    },
   ()  => console.log('HTTP request completed.'));
}

public submitForm(formData: any, formDirective: FormGroupDirective): void {
  formDirective.resetForm();
  this.produtoForm.reset();
}
}
