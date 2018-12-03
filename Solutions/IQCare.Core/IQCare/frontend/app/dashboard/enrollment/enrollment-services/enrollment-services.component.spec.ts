import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrollmentServicesComponent } from './enrollment-services.component';

describe('EnrollmentServicesComponent', () => {
  let component: EnrollmentServicesComponent;
  let fixture: ComponentFixture<EnrollmentServicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrollmentServicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrollmentServicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
