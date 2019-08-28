import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientChronicillnessComponent } from './patient-chronicillness.component';

describe('PatientChronicillnessComponent', () => {
  let component: PatientChronicillnessComponent;
  let fixture: ComponentFixture<PatientChronicillnessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientChronicillnessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientChronicillnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
