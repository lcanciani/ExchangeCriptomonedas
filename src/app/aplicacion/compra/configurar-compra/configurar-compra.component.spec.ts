import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurarCompraComponent } from './configurar-compra.component';

describe('ConfigurarCompraComponent', () => {
  let component: ConfigurarCompraComponent;
  let fixture: ComponentFixture<ConfigurarCompraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurarCompraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurarCompraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
