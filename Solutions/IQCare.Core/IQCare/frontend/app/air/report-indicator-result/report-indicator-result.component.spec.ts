import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportIndicatorResultComponent } from './report-indicator-result.component';

describe('ReportIndicatorResultComponent', () => {
  let component: ReportIndicatorResultComponent;
  let fixture: ComponentFixture<ReportIndicatorResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportIndicatorResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportIndicatorResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
