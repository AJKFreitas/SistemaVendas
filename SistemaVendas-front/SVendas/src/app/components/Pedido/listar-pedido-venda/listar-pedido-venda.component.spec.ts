import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarPedidoVendaComponent } from './listar-pedido-venda.component';

describe('ListarPedidoVendaComponent', () => {
  let component: ListarPedidoVendaComponent;
  let fixture: ComponentFixture<ListarPedidoVendaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarPedidoVendaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarPedidoVendaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
