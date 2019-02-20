import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';

const routes: Routes = [
     {
      path: 'indicatorreportingperiod',
      component: IndicatorReportingPeriodComponent,
     }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AirRoutingModule { }

