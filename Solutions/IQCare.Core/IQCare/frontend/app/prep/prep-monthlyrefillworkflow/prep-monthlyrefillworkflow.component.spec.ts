import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepMonthlyrefillworkflowComponent } from './prep-monthlyrefillworkflow.component';

describe('PrepMonthlyrefillworkflowComponent', () => {
  let component: PrepMonthlyrefillworkflowComponent;
  let fixture: ComponentFixture<PrepMonthlyrefillworkflowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepMonthlyrefillworkflowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepMonthlyrefillworkflowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
