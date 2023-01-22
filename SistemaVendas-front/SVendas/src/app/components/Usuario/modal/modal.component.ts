import { Component, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { UsuarioService } from 'src/app/components/Usuario/services/usuario.service';
import { Usuario } from '../../Auth/shared/models/User';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';


@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;
  usuarioForm: UntypedFormGroup;
  isSubmitted  =  false;
  constructor(
              public dialogRef: MatDialogRef<ModalComponent>,
              @Optional()
              @Inject(MAT_DIALOG_DATA)
              public data: Usuario,

              public service: UsuarioService) {
                 this.local_data = { ...data };
                 this.service.form.setValue({
                            id: new UntypedFormControl(this.local_data.id).value,
                            email: new UntypedFormControl(this.local_data.email).value,
                            nome: new UntypedFormControl(this.local_data.nome).value,
                            senha: new UntypedFormControl(this.local_data.senha).value,
                            role: new UntypedFormControl(this.local_data.role).value,
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
