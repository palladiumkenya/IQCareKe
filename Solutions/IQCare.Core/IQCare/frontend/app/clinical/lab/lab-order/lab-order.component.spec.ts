import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LabOrderComponent } from './lab-order.component';

describe('LabOrderComponent', () => {
  let component: LabOrderComponent;
  let fixture: ComponentFixture<LabOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LabOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LabOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
