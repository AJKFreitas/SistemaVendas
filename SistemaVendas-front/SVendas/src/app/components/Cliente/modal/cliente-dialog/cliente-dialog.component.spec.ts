import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClienteDialogComponent } from './cliente-dialog.component';

describe('ClienteDialogComponent', () => {
  let component: ClienteDialogComponent;
  let fixture: ComponentFixture<ClienteDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClienteDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClienteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
