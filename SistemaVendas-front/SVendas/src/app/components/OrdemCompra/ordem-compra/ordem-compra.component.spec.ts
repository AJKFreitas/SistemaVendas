import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdemCompraComponent } from './ordem-compra.component';

describe('OrdemCompraComponent', () => {
  let component: OrdemCompraComponent;
  let fixture: ComponentFixture<OrdemCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrdemCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdemCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
