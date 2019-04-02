import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReportSectionSettingComponent } from './report-section-setting/report-section-setting.component';
import { IndicatorReportingPeriodComponent } from './indicator-reporting-period/indicator-reporting-period.component';
import { ReportIndicatorResultComponent } from './report-indicator-result/report-indicator-result.component';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component'
import { FormDetailResolver } from './_services/customformdetails.resolver';
import { ReportsComponent } from './reports/reports.component';
const routes: Routes = [
    {
        path: 'report/:reportingFormId',
        component: IndicatorReportingPeriodComponent,
        pathMatch: 'full'
    },
    {
        path: 'indicator/result/:reportingFormId/:reportingPeriodId',
        component: ReportIndicatorResultComponent,
        pathMatch: 'full'
    },
    {
        path: 'formdetails/:reportingFormId',
        component: ActiveFormReportComponent,
        resolve: {
            FormDetails: FormDetailResolver
        },
        data: { isEdit: false }
    },
    {
        path: 'formdetails/edit/:reportingFormId/:reportingPeriodId',
        component: ActiveFormReportComponent,
        resolve: {
            FormDetails: FormDetailResolver
        },
        data: { isEdit: true }
    },
    {
        path: 'sections/:reportingFormId',
        component: ReportSectionSettingComponent,
        pathMatch: 'full'

    },
    {
        path: 'report',
        component: ReportsComponent
    }
];



@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AirRoutingModule { }

