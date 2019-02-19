import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';

const routes: Routes = [
     {
      path: 'air/submittedreports',
      component: IndicatorReportingPeriodComponent,
      pathMatch: 'full',
     }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AirRoutingModule { }

