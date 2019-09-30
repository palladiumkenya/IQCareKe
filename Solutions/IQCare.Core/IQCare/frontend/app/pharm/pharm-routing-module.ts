import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PharmOrderformComponent } from './pharm-orderform/pharm-orderform.component';
import { AdultRegimenClassificationResolver, PaedsClassificationResolver } from './services/regimenclassifications.resolver';
import { FrequencyTypeResolver } from './services/frequencytype.resolver';
import { ActiveModulesResolver } from './services/activemodules.resolver';
import { TreatmentStartedResolver } from './services/TreatmentStarted.resolver';
import { PersonCurrentVitalsResolver } from './services/Vitals.resolver';
import { PharmMainpageComponent } from './pharm-mainpage/pharm-mainpage.component'


const routes: Routes = [
    {
        path: 'mainpage/:patientId/:personId',
        component: PharmMainpageComponent
    },
    {

        path: ':patientId/:personId',
        pathMatch: 'full',
        component: PharmOrderformComponent,
        resolve: {
            AdultArray: AdultRegimenClassificationResolver,
            PaedsArray: PaedsClassificationResolver,
            FrequencyArray: FrequencyTypeResolver,
            ActiveModulesArray: ActiveModulesResolver,
            TreatmentStartArray: TreatmentStartedResolver,
            PersonVitalsArray: PersonCurrentVitalsResolver
        }
    },
    {

        path: ':patientId/:personId/:patientMasterVisitId/:edit',
        pathMatch: 'full',
        component: PharmOrderformComponent,
        resolve: {
            AdultArray: AdultRegimenClassificationResolver,
            PaedsArray: PaedsClassificationResolver,
            FrequencyArray: FrequencyTypeResolver,
            ActiveModulesArray: ActiveModulesResolver,
            TreatmentStartArray: TreatmentStartedResolver,
            PersonVitalsArray: PersonCurrentVitalsResolver
        }
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PharmRoutingModule {

}

