import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportSectionSettingComponent } from './report-section-setting.component';

describe('ReportSectionSettingComponent', () => {
  let component: ReportSectionSettingComponent;
  let fixture: ComponentFixture<ReportSectionSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportSectionSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportSectionSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
