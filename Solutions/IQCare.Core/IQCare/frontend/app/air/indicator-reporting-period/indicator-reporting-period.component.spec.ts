import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IndicatorReportingPeriodComponent } from './indicator-reporting-period.component';

describe('IndicatorReportingPeriodComponent', () => {
  let component: IndicatorReportingPeriodComponent;
  let fixture: ComponentFixture<IndicatorReportingPeriodComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndicatorReportingPeriodComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndicatorReportingPeriodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
