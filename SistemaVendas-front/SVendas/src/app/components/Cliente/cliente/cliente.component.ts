import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../Shared/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { FormGroup, FormBuilder, FormGroupDirective } from '@angular/forms';
import { ClienteService } from '../service/cliente.service';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})


export class ClienteComponent implements OnInit {

  clienteForm: FormGroup;

  constructor(
    public formbuilder: FormBuilder,
    public spinnerService: NgxSpinnerService,
    public toastSevice: ToastService,
    public clienteService: ClienteService
  ) {
    this.clienteForm = this.formbuilder.group({
      nome: [''],
      cpf: [''],
      endereco: [''],
      telefone: ['']
    });
  }


  ngOnInit(): void {
  }

  inserirCliente() {
    this.spinnerService.show();
    this.clienteService.iserir(this.clienteForm.value).subscribe((res) => {
      if (res.result) {
        this.clienteForm.reset();
        this.formbuilder = new FormBuilder();
        this.toastSevice.Success('Sucesso!', 'Cliente incluido com Sucesso!');
      }
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Cliente!');
      },
      () => console.log('HTTP request completed.'));
  }
  public submitForm(formData: any, formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.clienteForm.reset();
  }
}
