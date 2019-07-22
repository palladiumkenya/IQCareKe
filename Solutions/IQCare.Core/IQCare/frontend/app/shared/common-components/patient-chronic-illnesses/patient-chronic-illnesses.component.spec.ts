import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientChronicIllnessesComponent } from './patient-chronic-illnesses.component';

describe('PatientChronicIllnessesComponent', () => {
  let component: PatientChronicIllnessesComponent;
  let fixture: ComponentFixture<PatientChronicIllnessesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientChronicIllnessesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientChronicIllnessesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
