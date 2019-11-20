import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ActivityFormComponent} from './otz/activity-form/activity-form.component';
import {OtzEnrollmentComponent} from './otz/otz-enrollment/otz-enrollment.component';
import {EncounterHistoryComponent} from './otz/encounter-history/encounter-history.component';
import {ViewOtzFormComponent} from './otz/view-otz-form/view-otz-form.component';
import {OtzCareendingComponent} from './otz/otz-careending/otz-careending.component';

const routes: Routes = [
    {
        path: 'encounterHistory/:patientId/:personId/:serviceId',
        component: EncounterHistoryComponent,
        pathMatch: 'full'
    },
    {
        path: 'activityForm/:patientId/:personId/:serviceId',
        component: ActivityFormComponent,
        pathMatch: 'full'
    },
    {
        path: 'otzEnrollment/:personId/:patientId/:serviceId/:serviceCode',
        component: OtzEnrollmentComponent,
        pathMatch: 'full'
    },
    {
        path: 'viewActivityForm/:id/:patientId',
        component: ViewOtzFormComponent,
        pathMatch: 'full'
    },
    {
        path: 'activityForm/update/:patientId/:personId/:serviceId/:id',
        component: ActivityFormComponent,
        pathMatch: 'full'
    },
    {
        path: 'careEnding/:patientId/:personId/:serviceId',
        component: OtzCareendingComponent,
        pathMatch: 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CccRoutingModule {
}
