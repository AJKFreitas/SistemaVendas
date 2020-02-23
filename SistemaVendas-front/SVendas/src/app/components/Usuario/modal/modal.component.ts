import { Component, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { UsuarioService } from 'src/app/components/Usuario/services/usuario.service';
import { Usuario } from '../../Auth/shared/models/User';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;
  usuarioForm: FormGroup;
  isSubmitted  =  false;
  constructor(
              public dialogRef: MatDialogRef<ModalComponent>,
              @Optional()
              @Inject(MAT_DIALOG_DATA)
              public data: Usuario,

              public service: UsuarioService) {
                 this.local_data = { ...data };
                 this.service.form.setValue({
                            id: new FormControl(this.local_data.id).value,
                            email: new FormControl(this.local_data.email).value,
                            nome: new FormControl(this.local_data.nome).value,
                            senha: new FormControl(this.local_data.senha).value,
                            role: new FormControl(this.local_data.role).value,
                });
                 this.action = this.local_data.action;
                 }

  ngOnInit(): void {
    this.usuarioForm = this.service.form;
  }
   doAction() {
    this.dialogRef.close({ event: this.action, data: this.service.form});
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
