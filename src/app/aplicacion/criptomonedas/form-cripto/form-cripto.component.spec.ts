import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormCriptoComponent } from './form-cripto.component';

describe('FormCriptoComponent', () => {
  let component: FormCriptoComponent;
  let fixture: ComponentFixture<FormCriptoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormCriptoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormCriptoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
