import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormGroupDirective } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { FornecedorService } from '../services/fornecedor.service';
import { MensagemPopUPService } from '../../Shared/ToastService';

@Component({
  selector: 'app-fornecedor',
  templateUrl: './fornecedor.component.html',
  styleUrls: ['./fornecedor.component.css']
})
export class FornecedorComponent implements OnInit {

  fornecedorForm: FormGroup;

  constructor(
    public fb: FormBuilder,
    public fornecedorService: FornecedorService,
    public router: Router,
    private SpinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService
  ) {
    this.fornecedorForm = this.fb.group({
      nome: [''],
      cnpj: [''],
      telefone: ['']
    })
  }

  ngOnInit() {
   }

  inserirFornecedor() {
    this.SpinnerService.show();
    this.fornecedorService.iserir(this.fornecedorForm.value).subscribe((res) => {
      if (res.result) {
        this.fornecedorForm.reset();
        this.fb = new  FormBuilder();
        this.toastSevice.Sucesso('Sucesso!', 'Fonecedor incluido com Sucesso!');
      }
      this.SpinnerService.hide();
    },
    err => {
      this.SpinnerService.hide();
      this.toastSevice.Erro('Erro ao tentar cadastar Fonecedor!');
    },
   ()  => console.log('HTTP request completed.'));
  }


public submitForm(formData: any, formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.fornecedorForm.reset();
}
}
