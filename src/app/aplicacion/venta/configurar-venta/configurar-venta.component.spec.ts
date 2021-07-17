import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigurarVentaComponent } from './configurar-venta.component';

describe('ConfigurarVentaComponent', () => {
  let component: ConfigurarVentaComponent;
  let fixture: ComponentFixture<ConfigurarVentaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfigurarVentaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigurarVentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
