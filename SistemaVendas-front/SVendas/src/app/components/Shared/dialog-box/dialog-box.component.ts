import { Usuario } from './../../Auth/shared/models/User';
import { Component, OnInit, Optional, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})

export class DialogBoxComponent implements OnInit {
  action: string;
  // tslint:disable-next-line:variable-name
  local_data: any;

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Optional()
    @Inject(MAT_DIALOG_DATA)
     public data: Usuario
  ) {
    console.log(data);
    this.local_data = { ...data };
    this.action = this.local_data.action;
  }
  ngOnInit(): void {}

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }
}
