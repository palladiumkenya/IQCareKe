import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';

const routes: Routes = [
     {
      path: 'air',
      pathMatch : 'full',
      children : [
        {
           path: 'report',
           pathMatch : 'full',
           component : IndicatorReportingPeriodComponent
        },
        {
          path: 'indicator/result/:reportingPeriodId',
          pathMatch :'full',
          component : ReportIndicatorResultComponent
        }
      ]
     }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AirRoutingModule { }

