import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component'
import { FormDetailResolver } from './_services/customformdetails.resolver';
const routes: Routes = [
    {
      path: '',
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
        },
        
        
      ],
      

    },
   
];



@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AirRoutingModule { }

