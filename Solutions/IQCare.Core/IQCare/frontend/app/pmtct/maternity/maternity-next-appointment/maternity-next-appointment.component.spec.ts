import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaternityNextAppointmentComponent } from './maternity-next-appointment.component';

describe('MaternityNextAppointmentComponent', () => {
  let component: MaternityNextAppointmentComponent;
  let fixture: ComponentFixture<MaternityNextAppointmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaternityNextAppointmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaternityNextAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
