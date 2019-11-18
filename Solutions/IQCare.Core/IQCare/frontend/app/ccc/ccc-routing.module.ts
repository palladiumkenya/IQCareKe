import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {ActivityFormComponent} from './otz/activity-form/activity-form.component';
import {OtzEnrollmentComponent} from './otz/otz-enrollment/otz-enrollment.component';
import {EncounterHistoryComponent} from './otz/encounter-history/encounter-history.component';

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
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CccRoutingModule {
}
