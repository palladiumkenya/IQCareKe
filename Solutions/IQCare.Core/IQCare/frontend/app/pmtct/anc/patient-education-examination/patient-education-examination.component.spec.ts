import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientEducationExaminationComponent } from './patient-education-examination.component';

describe('PatientEducationExaminationComponent', () => {
  let component: PatientEducationExaminationComponent;
  let fixture: ComponentFixture<PatientEducationExaminationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientEducationExaminationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientEducationExaminationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
