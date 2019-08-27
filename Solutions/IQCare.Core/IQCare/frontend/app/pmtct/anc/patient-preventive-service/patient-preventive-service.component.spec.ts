import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientPreventiveServiceComponent } from './patient-preventive-service.component';

describe('PatientPreventiveServiceComponent', () => {
  let component: PatientPreventiveServiceComponent;
  let fixture: ComponentFixture<PatientPreventiveServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientPreventiveServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientPreventiveServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
