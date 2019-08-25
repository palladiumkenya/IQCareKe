import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientHtsComponent } from './patient-hts.component';

describe('PatientHtsComponent', () => {
  let component: PatientHtsComponent;
  let fixture: ComponentFixture<PatientHtsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientHtsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientHtsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
