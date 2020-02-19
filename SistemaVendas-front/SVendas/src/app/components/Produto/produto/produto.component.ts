import { Component, OnInit } from '@angular/core';
import { Form, FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {
  selectData;
  dropdownOptions;
  options = [{ id: 34, description: 'Adding new item' }];
 config = {
                displayKey: "description",
                search: true,
                height: 'auto',
                placeholder: 'Select',
                customComparator: () => { },
                limitTo: this.options.length,
                moreText: 'more',
                noResultsFound: 'No results found!',
                searchPlaceholder: 'Search',
                searchOnKey: 'name',
                clearOnSelection: false
                };

produtoForm: FormGroup;
constructor(public formBuilder: FormBuilder,
            public router: Router,
            private spinnerService: NgxSpinnerService,
            private toastSevice: ToastService
) {
  this.produtoForm = this.formBuilder.group({
    nome: [''],
    descricao: [''],
    valor: [''],
    fornecedor: ['']
  });
}

ngOnInit(): void {
}

selectionChanged($event: Event) { }
}
