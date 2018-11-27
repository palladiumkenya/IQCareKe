import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        loadChildren: '../dashboard/dashboard.module#DashboardModule'
    },
    {
        path: 'hts',
        loadChildren: '../hts/hts.module#HtsModule'
    },
    {
        path: 'registration',
        loadChildren: '../registration/registration.module#RegistrationModule'
    },
    {
        path: 'record',
        loadChildren: '../records/records.module#RecordsModule'
    },
    {
        path: 'pmtct',
        loadChildren: '../pmtct/pmtct.module#PmtctModule'
    },
{
    path:'clinical',
    loadChildren:'../clinical/clinical.module#ClinicalModule'
},
    {
        path: '**',
        component: NotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class CoreRoutingModule { }
