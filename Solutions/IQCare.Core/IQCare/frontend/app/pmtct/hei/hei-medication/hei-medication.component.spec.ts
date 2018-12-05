import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HeiMedicationComponent } from './hei-medication.component';

describe('HeiMedicationComponent', () => {
  let component: HeiMedicationComponent;
  let fixture: ComponentFixture<HeiMedicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HeiMedicationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeiMedicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
