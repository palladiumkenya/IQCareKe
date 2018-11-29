import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TriageComponent } from './triage/triage.component';

const routes: Routes = [
    {
        path: 'triage/:patientId',
        component: TriageComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClinicalRoutingModule { }
