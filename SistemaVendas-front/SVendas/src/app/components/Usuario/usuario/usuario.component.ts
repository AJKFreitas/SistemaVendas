import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormGroupDirective } from '@angular/forms';
import { AuthService } from '../../Auth/shared/services/auth.service';
import { Router } from '@angular/router';
import { error } from '@angular/compiler/src/util';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { UsuarioService } from '../services/usuario.service';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

interface DialogData {
  email: string;
}
@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

  signupForm: FormGroup;
  role: string;
  roles: string[] = ['Admin', 'Funcionario', 'Fornecedor', 'Vendedor'];
 
  constructor(
    public fb: FormBuilder,
    public service: UsuarioService,
    public router: Router,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService
  ) {
    this.signupForm = this.fb.group({
      nome: [''],
      email: [''],
      senha: [''],
      role: ['']
    });
  }

  ngOnInit() { }

  registerUser(formData: any, formDirective: FormGroupDirective) {
    this.spinnerService.show();
    this.service.iserir(this.signupForm.value).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Success('Sucesso!', 'Usuario cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Usuario cadastrado com sucesso!');
      this.submitForm(formData, formDirective);
      this.spinnerService.hide();
    },
    err => {
      this.spinnerService.hide();
      this.toastSevice.Error('Erro ao tentar cadastar Usuario!');
    }
    );
  }
  public submitForm(formData: any, formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.signupForm.reset();
}


onClear() {
    let $key = this.service.form.get('$key').value;
    this.service.form.reset();
    this.service.initializeFormGroup();
    this.service.form.patchValue({ $key });
  }
}
