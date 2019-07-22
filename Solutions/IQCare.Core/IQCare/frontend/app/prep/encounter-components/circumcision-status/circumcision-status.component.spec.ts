import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CircumcisionStatusComponent } from './circumcision-status.component';

describe('CircumcisionStatusComponent', () => {
  let component: CircumcisionStatusComponent;
  let fixture: ComponentFixture<CircumcisionStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CircumcisionStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CircumcisionStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
