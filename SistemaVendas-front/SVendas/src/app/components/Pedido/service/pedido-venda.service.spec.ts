import { TestBed } from '@angular/core/testing';

import { PedidoVendaService } from './pedido-venda.service';

describe('PedidoVendaService', () => {
  let service: PedidoVendaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PedidoVendaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
