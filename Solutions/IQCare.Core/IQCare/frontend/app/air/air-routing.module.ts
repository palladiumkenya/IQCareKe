import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActiveFormReportComponent } from './active-form-report/active-form-report.component'
import { FormDetailResolver } from './_services/customformdetails.resolver';
const routes: Routes = [
    {
        path: 'formdetails/:id',
        component: ActiveFormReportComponent,
        resolve: {
            FormDetails: FormDetailResolver
        }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AirRoutingModule { }
