import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReenrollmentComponent } from './reenrollment.component';

describe('ReenrollmentComponent', () => {
  let component: ReenrollmentComponent;
  let fixture: ComponentFixture<ReenrollmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReenrollmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReenrollmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
