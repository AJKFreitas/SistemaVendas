import { Component, OnInit, Optional, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ModalComponent } from 'src/app/components/Usuario/modal/modal.component';
import { Cliente } from '../../model/Cliente';
import { ClienteService } from '../../service/cliente.service';

@Component({
  selector: 'app-cliente-dialog',
  templateUrl: './cliente-dialog.component.html',
  styleUrls: ['./cliente-dialog.component.css']
})
export class ClienteDialogComponent implements OnInit {
  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;
  clienteForm: FormGroup;
  isSubmitted = false;
    constructor(
      public dialogRef: MatDialogRef<ClienteDialogComponent>,
      @Optional()
      @Inject(MAT_DIALOG_DATA)
      public data: Cliente,
      public service: ClienteService) {
      this.local_data = { ...data };
      this.service.form.setValue({
        id: new FormControl(this.local_data.id).value,
        nome: new FormControl(this.local_data.nome).value,
        cpf: new FormControl(this.local_data.cpf).value,
        telefone: new FormControl(this.local_data.telefone).value,
        endereco: new FormControl(this.local_data.endereco).value
      });
      this.action = this.local_data.action;
    }

  ngOnInit(): void {
    this.clienteForm = this.service.form;
  }
  doAction() {
    this.dialogRef.close({ event: this.action, data: this.service.form });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
