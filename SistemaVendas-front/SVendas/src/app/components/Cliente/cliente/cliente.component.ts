import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../Shared/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
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
    public servicoDeLoading: NgxSpinnerService,
    public servicoDeMensagemPopUp: MensagemPopUPService,
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
    this.servicoDeLoading.show();
    this.clienteService.iserir(this.clienteForm.value).subscribe((res) => {
      if (res.result) {
        this.clienteForm.reset();
        this.formbuilder = new FormBuilder();
        this.servicoDeMensagemPopUp.Sucesso('Sucesso!', 'Cliente incluido com Sucesso!');
      }
      this.servicoDeLoading.hide();
    },
      err => {
        this.servicoDeLoading.hide();
        this.servicoDeMensagemPopUp.Erro('Erro ao tentar cadastar Cliente!');
      },
      () => console.log('HTTP request completed.'));
  }
  public submitForm(formData: any, formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.clienteForm.reset();
  }
}
