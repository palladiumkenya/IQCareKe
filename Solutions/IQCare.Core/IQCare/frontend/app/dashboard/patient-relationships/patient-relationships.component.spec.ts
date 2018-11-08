import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientRelationshipsComponent } from './patient-relationships.component';

describe('PatientRelationshipsComponent', () => {
  let component: PatientRelationshipsComponent;
  let fixture: ComponentFixture<PatientRelationshipsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientRelationshipsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientRelationshipsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
