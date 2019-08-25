import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepAppointmentgridComponent } from './prep-appointmentgrid.component';

describe('PrepAppointmentgridComponent', () => {
  let component: PrepAppointmentgridComponent;
  let fixture: ComponentFixture<PrepAppointmentgridComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepAppointmentgridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepAppointmentgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
