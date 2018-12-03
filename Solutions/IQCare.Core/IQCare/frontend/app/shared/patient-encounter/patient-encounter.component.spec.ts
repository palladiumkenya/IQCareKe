import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientEncounterComponent } from './patient-encounter.component';

describe('PatientEncounterComponent', () => {
  let component: PatientEncounterComponent;
  let fixture: ComponentFixture<PatientEncounterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientEncounterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientEncounterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
