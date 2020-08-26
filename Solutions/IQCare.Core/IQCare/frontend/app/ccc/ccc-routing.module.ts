import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { ActivityFormComponent } from './otz/activity-form/activity-form.component';
import { OtzEnrollmentComponent } from './otz/otz-enrollment/otz-enrollment.component';
import { EncounterHistoryComponent } from './otz/encounter-history/encounter-history.component';
import { ViewOtzFormComponent } from './otz/view-otz-form/view-otz-form.component';
import { OtzCareendingComponent } from './otz/otz-careending/otz-careending.component';
import { OvcEnrollmentComponent } from './ovc/ovc-enrollment/ovc-enrollment.component';
import { FamilySearchComponent } from './ovc/family/family-search/family-search.component';
import { RegisterFamilyComponent } from './ovc/family/register/registerfamily.component';
import { OvcCareendingComponent } from './ovc/ovc-careending/ovc-careending.component';
import { OvcEncounterComponent } from './ovc/ovc-encounter/ovc-encounter.component';

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
        path: 'ovcEnrollment/:personId/:patientId/:serviceId/:serviceCode',
        component: OvcEnrollmentComponent,
        pathMatch: 'full'
    },
    {
        path: 'ovcEnrollment/:personId/:patientId/:serviceId/:serviceCode/:edit',
        component: OvcEnrollmentComponent,
        pathMatch: 'full'

    },
    {

        path: 'ovcnewEncounter/:personId/:patientId/:serviceId',
        component: OvcEnrollmentComponent,
        pathMatch: 'full'

    },

    {

        path: 'ovccareend',
        children: [
            {
                path: ':patientId/:personId/:serviceId',
                component: OvcCareendingComponent,
                pathMatch: 'full',

            },
            {
                path: ':patientId/:personId/:serviceId/:patientMasterVisitId/:edit',
                component: OvcCareendingComponent,
                pathMatch: 'full',

            }




        ]


    },
    {
        path: 'ovcFormList/:patientId/:personId/:serviceId',
        component: OvcEncounterComponent,
        pathMatch: 'full'
    },
    {
        path: 'familysearch/:personId/:patientId/:serviceId/:serviceCode',
        component: FamilySearchComponent,
        pathMatch: 'full'
    },
    {
        path: 'registernew/:personId/:patientId/:serviceId/:serviceCode',
        component: RegisterFamilyComponent,
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
