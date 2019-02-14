import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AirRoutingModule } from './air-routing.module';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component';

@NgModule({
  declarations: [ActiveFormReportComponent],
  imports: [
    CommonModule,
    AirRoutingModule
  ]
})
export class AirModule { }
