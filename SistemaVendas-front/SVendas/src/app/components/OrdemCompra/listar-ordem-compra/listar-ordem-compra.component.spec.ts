import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarOrdemCompraComponent } from './listar-ordem-compra.component';

describe('ListarOrdemCompraComponent', () => {
  let component: ListarOrdemCompraComponent;
  let fixture: ComponentFixture<ListarOrdemCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListarOrdemCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListarOrdemCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
