import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AirRoutingModule } from './air-routing.module';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { MatTableModule, MatPaginatorModule } from '@angular/material';
import { IndicatorService } from './_services/indicator.service';

@NgModule({
  declarations: [
    ActiveFormReportComponent,
    ReportIndicatorResultComponent,
    IndicatorReportingPeriodComponent
  ],
  imports: [
    CommonModule,
    AirRoutingModule,
    MatTableModule,
    MatPaginatorModule

  ],
  providers :[
    IndicatorService
  ]
})
export class AirModule { }
