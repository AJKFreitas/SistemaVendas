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
  acaoModal: string;
  dadosLocais: any;
  clienteForm: FormGroup;
  isSubmitted = false;
    constructor(
      public dialogRef: MatDialogRef<ClienteDialogComponent>,
      @Optional()
      @Inject(MAT_DIALOG_DATA)
      public cliente: Cliente,
      public service: ClienteService) {
      this.dadosLocais = { ...cliente };
      this.service.clienteFormGroup.setValue({
        id: new FormControl(this.dadosLocais.id).value,
        nome: new FormControl(this.dadosLocais.nome).value,
        cpf: new FormControl(this.dadosLocais.cpf).value,
        telefone: new FormControl(this.dadosLocais.telefone).value,
        endereco: new FormControl(this.dadosLocais.endereco).value
      });
      this.acaoModal = this.dadosLocais.action;
    }

  ngOnInit(): void {
    this.clienteForm = this.service.clienteFormGroup;
  }
  doAction() {
    this.dialogRef.close({ event: this.acaoModal, data: this.service.clienteFormGroup });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
