import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveFormReportComponent } from './active-form-report.component';

describe('ActiveFormReportComponent', () => {
  let component: ActiveFormReportComponent;
  let fixture: ComponentFixture<ActiveFormReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActiveFormReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActiveFormReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
