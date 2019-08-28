import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientCounsellingComponent } from './patient-counselling.component';

describe('PatientCounsellingComponent', () => {
  let component: PatientCounsellingComponent;
  let fixture: ComponentFixture<PatientCounsellingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientCounsellingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientCounsellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
