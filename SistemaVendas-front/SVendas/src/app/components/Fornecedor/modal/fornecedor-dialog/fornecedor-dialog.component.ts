import { Component, OnInit, Optional, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Fornecedor } from '../../model/Fornecedor';
import { FornecedorService } from '../../services/fornecedor.service';

@Component({
  selector: 'app-fornecedor-dialog',
  templateUrl: './fornecedor-dialog.component.html',
  styleUrls: ['./fornecedor-dialog.component.css']
})
export class FornecedorDialogComponent implements OnInit {

  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;
  fornecedorForm: FormGroup;
  isSubmitted  =  false;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;
  
  constructor(
              public dialogRef: MatDialogRef<FornecedorDialogComponent>,
              @Optional()
              @Inject(MAT_DIALOG_DATA)
              public data: Fornecedor,

              public service: FornecedorService) {
                 this.local_data = { ...data };
                 this.service.form.setValue({
                            id: new FormControl(this.local_data.id).value,
                            nome: new FormControl(this.local_data.nome).value,
                            cnpj: new FormControl(this.local_data.cnpj).value,
                            telefone: new FormControl(this.local_data.telefone).value
                });
                 this.action = this.local_data.action;
                 }

  ngOnInit(): void {
    this.fornecedorForm = this.service.form;
  }
   doAction() {
    this.dialogRef.close({ event: this.action, data: this.service.form});
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }

}
