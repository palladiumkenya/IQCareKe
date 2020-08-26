import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OtzEnrollmentComponent } from './otz-enrollment.component';

describe('OtzEnrollmentComponent', () => {
  let component: OtzEnrollmentComponent;
  let fixture: ComponentFixture<OtzEnrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OtzEnrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OtzEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
