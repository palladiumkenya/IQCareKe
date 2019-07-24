import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepRiskassessmentComponent } from './prep-riskassessment.component';

describe('PrepRiskassessmentComponent', () => {
  let component: PrepRiskassessmentComponent;
  let fixture: ComponentFixture<PrepRiskassessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepRiskassessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepRiskassessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
