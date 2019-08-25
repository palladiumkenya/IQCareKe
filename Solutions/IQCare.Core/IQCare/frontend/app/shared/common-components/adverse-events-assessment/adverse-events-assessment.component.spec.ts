import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdverseEventsAssessmentComponent } from './adverse-events-assessment.component';

describe('AdverseEventsAssessmentComponent', () => {
  let component: AdverseEventsAssessmentComponent;
  let fixture: ComponentFixture<AdverseEventsAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdverseEventsAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdverseEventsAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
