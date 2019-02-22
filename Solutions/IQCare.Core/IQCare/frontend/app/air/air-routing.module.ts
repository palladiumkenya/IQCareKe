import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component'
import { FormDetailResolver } from './_services/customformdetails.resolver';
const routes: Routes = [
     {
      path: '',
      component : IndicatorReportingPeriodComponent,
      children : [
        {
           path: 'report',
           component : IndicatorReportingPeriodComponent,
           pathMatch : 'full'
        }
      ],
     },
     {
      path: 'indicator/result/:reportingPeriodId',
      component : ReportIndicatorResultComponent,
      pathMatch :'full'
    },
    {
        path: 'formdetails/:id/:reportingformid',
       component: ActiveFormReportComponent,
      resolve: {
          FormDetails: FormDetailResolver
         }
     }
];



@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AirRoutingModule { }

