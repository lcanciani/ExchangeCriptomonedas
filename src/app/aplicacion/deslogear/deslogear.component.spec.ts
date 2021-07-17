import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeslogearComponent } from './deslogear.component';

describe('DeslogearComponent', () => {
  let component: DeslogearComponent;
  let fixture: ComponentFixture<DeslogearComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeslogearComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeslogearComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
