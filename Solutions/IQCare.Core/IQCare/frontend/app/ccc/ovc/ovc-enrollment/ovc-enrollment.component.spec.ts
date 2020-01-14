import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OvcEnrollmentComponent } from './ovc-enrollment.component';

describe('OvcEnrollmentComponent', () => {
  let component: OvcEnrollmentComponent;
  let fixture: ComponentFixture<OvcEnrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OvcEnrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OvcEnrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
