import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from './ToastService';
import { FormBuilder } from '@angular/forms';


export abstract class BaseComponent  {

    constructor(
        public SpinnerService: NgxSpinnerService,
        public toastSevice: MensagemPopUPService
    ) { }
}