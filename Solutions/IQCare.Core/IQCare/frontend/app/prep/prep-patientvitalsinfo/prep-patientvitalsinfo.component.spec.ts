import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepPatientvitalsinfoComponent } from './prep-patientvitalsinfo.component';

describe('PrepPatientvitalsinfoComponent', () => {
  let component: PrepPatientvitalsinfoComponent;
  let fixture: ComponentFixture<PrepPatientvitalsinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepPatientvitalsinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepPatientvitalsinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
