import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepSTIScreeningTreatmentComponent } from './prep-sti-screening-treatment.component';

describe('PrepSTIScreeningTreatmentComponent', () => {
  let component: PrepSTIScreeningTreatmentComponent;
  let fixture: ComponentFixture<PrepSTIScreeningTreatmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepSTIScreeningTreatmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepSTIScreeningTreatmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
