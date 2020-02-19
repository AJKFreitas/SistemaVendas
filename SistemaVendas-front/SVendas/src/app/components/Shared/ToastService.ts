import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from './Message';

declare var toastr: any;

@Injectable()
export class ToastService {
    constructor() { }

    Success(title: string, meassage?: string) {
        toastr.success(title, meassage);
    }
    Warning(title: string, meassage?: string) {
        toastr.warning(title, meassage);
    }

    Error(title: string, meassage?: string) {
        toastr.error(title, meassage);
    }

    Info(meassage: string) {
        toastr.info(meassage);
    }


}