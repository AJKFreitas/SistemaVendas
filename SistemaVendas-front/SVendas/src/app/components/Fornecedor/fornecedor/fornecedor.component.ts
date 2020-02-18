import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormGroupDirective } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { FornecedorService } from '../services/fornecedor.service';
import { ToastrService } from 'ngx-toastr';

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
    private toastr: ToastrService
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
        this.toastr.success('Fonecedor incluido com Sucesso!', 'Sucesso!');
      }
      this.SpinnerService.hide();
    },
    err => {
      this.SpinnerService.hide();
      console.log('HTTP Error', err);
    },
   ()  => console.log('HTTP request completed.'));
  }


public submitForm(formData: any, formDirective: FormGroupDirective): void {
    formDirective.resetForm();
    this.fornecedorForm.reset();
}
}
/* successmsg(){
  this.toastr.success("Toastr Success message",'Success')
}
errorsmsg(){
  this.toastr.error("Toastr Error Notification",'Error')

}
infotoastr()
{
this.toastr.info("Important News", 'Information');
}
toastrwaring()
{  
this.toastr.warning("Battery about to Die", 'Warning');
}   */
