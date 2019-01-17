import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TriageComponent } from './triage/triage.component';
import { LabOrderComponent } from './lab/lab-order/lab-order.component';
import { LabTestReasonsResolver } from './_services/labtestreasons.resolver';
import { LabTestsResolver } from './_services/labtests.resolver';
import { CompleteLabOrderComponent } from './lab/complete-lab-order/complete-lab-order.component';

const routes: Routes = [
    {
        path: 'triage/:patientId/:personId',
        component: TriageComponent
    },
    {
        path:'laborder/:patientId/:personId',
        component : LabOrderComponent ,
        pathMatch: 'full',
        resolve :{
            labTestReasonOptions : LabTestReasonsResolver ,
            configuredLabTests : LabTestsResolver
        }
    },
    {
      path:'completeorder/:patientId/:personId',
      component : CompleteLabOrderComponent,
      pathMatch : 'full'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ClinicalRoutingModule { }
