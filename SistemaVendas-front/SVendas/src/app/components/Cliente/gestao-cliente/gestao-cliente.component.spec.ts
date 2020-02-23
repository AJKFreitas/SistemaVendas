import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GestaoClienteComponent } from './gestao-cliente.component';

describe('GestaoClienteComponent', () => {
  let component: GestaoClienteComponent;
  let fixture: ComponentFixture<GestaoClienteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GestaoClienteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GestaoClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
