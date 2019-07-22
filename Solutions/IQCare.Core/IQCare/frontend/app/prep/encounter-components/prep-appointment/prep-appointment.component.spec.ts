import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepAppointmentComponent } from './prep-appointment.component';

describe('PrepAppointmentComponent', () => {
  let component: PrepAppointmentComponent;
  let fixture: ComponentFixture<PrepAppointmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepAppointmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
