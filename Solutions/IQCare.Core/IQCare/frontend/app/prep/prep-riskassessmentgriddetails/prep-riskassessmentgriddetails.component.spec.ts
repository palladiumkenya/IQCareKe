import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepRiskassessmentgriddetailsComponent } from './prep-riskassessmentgriddetails.component';

describe('PrepRiskassessmentgriddetailsComponent', () => {
  let component: PrepRiskassessmentgriddetailsComponent;
  let fixture: ComponentFixture<PrepRiskassessmentgriddetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepRiskassessmentgriddetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepRiskassessmentgriddetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
