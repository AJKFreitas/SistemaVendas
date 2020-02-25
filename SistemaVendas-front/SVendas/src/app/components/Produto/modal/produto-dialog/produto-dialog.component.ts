import { Component, OnInit, Optional, Inject } from '@angular/core';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ProdutoService } from '../../services/produto.service';
import { Produto } from '../../model/Produto';

@Component({
  selector: 'app-produto-dialog',
  templateUrl: './produto-dialog.component.html',
  styleUrls: ['./produto-dialog.component.css']
})
export class ProdutoDialogComponent implements OnInit {

  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;
  produtoForm: FormGroup;
  isSubmitted = false;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;

  constructor(
    public dialogRef: MatDialogRef<ProdutoDialogComponent>,
    @Optional()
    @Inject(MAT_DIALOG_DATA)
    public data: Produto,

    public service: ProdutoService) {
       this.local_data = { ...data };
       this.service.form.setValue({
                  id: new FormControl(this.local_data.id).value,
                  nome: new FormControl(this.local_data.nome).value,
                  descricao: new FormControl(this.local_data.descricao).value,
                  valor: new FormControl(this.local_data.valor).value,
      });
       this.action = this.local_data.action;
       }


    ngOnInit(): void {
      this.produtoForm = this.service.form;
    }
    doAction() {
      this.dialogRef.close({ event: this.action, data: this.service.form});
    }
    closeDialog() {
      this.dialogRef.close({ event: 'Cancel' });
    }

  }
