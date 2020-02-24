import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GestaoProdutosComponent } from './gestao-produtos.component';

describe('GestaoProdutosComponent', () => {
  let component: GestaoProdutosComponent;
  let fixture: ComponentFixture<GestaoProdutosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GestaoProdutosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GestaoProdutosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
