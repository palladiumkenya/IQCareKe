import { YesNoResolver } from './../pmtct/_services/yesno.resolver';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';
import { STIScreeningTreatmentResolver } from './_services/STIScreeningTreatment.resolver';
import { YesNoUnknownResolver } from './_services/YesNoUnknown.resolver';
import {
    AssessmentOutcomeResolver, ClientsBehaviourRiskResolver, SexualPartnetHivStatusProfileResolver,
    RiskReductionEducationResolver, ReferralPreventionServicesResolver, ClientWillingTakePrepResolver
    , RiskEducationResolver, BehaviourRiskAssessmentResolver, EncounterTypeResolver
} from './_services/resolvers/prepriskassessment.resolver';
import { PrepRiskassessmentComponent } from './prep-riskassessment/prep-riskassessment.component';

const routes: Routes = [
    {
        path: '',
        component: PrepEncounterHistoryComponent,
        pathMatch: 'full'
    },
    {
        path: 'encounter',
        component: PrepEncounterComponent,
        resolve: {
            yesNoOptions: YesNoResolver,
            stiScreeningTreatmentOptions: STIScreeningTreatmentResolver,
            yesNoUnknownOptions: YesNoUnknownResolver
        }
    },
    {  
        
        path: 'riskassessment',
        children: [
        {
        path: ':patientId/:personId/:serviceId',
        component: PrepRiskassessmentComponent,
        resolve: {
            assessmentOutComeArray: AssessmentOutcomeResolver,
            clientsBehaviourRiskArray: ClientsBehaviourRiskResolver,
            sexualPartnerHivStatusArray: SexualPartnetHivStatusProfileResolver,
            clientWillingTakePrepArray: ClientWillingTakePrepResolver,
            riskEducationArray: RiskEducationResolver,
            behaviourRiskAssessmentArray: BehaviourRiskAssessmentResolver,
            ReferralPreventionArray: ReferralPreventionServicesResolver,
            RiskReductionEducationArray: RiskReductionEducationResolver,
            EncounterTypeArray: EncounterTypeResolver
            }
        },
        {
            path: ':patientId/:personId/:serviceId/:patientMasterVisitId',
            component: PrepRiskassessmentComponent,
            resolve: {
                assessmentOutComeArray: AssessmentOutcomeResolver,
                clientsBehaviourRiskArray: ClientsBehaviourRiskResolver,
                sexualPartnerHivStatusArray: SexualPartnetHivStatusProfileResolver,
                clientWillingTakePrepArray: ClientWillingTakePrepResolver,
                riskEducationArray: RiskEducationResolver,
                behaviourRiskAssessmentArray: BehaviourRiskAssessmentResolver,
                ReferralPreventionArray: ReferralPreventionServicesResolver,
                RiskReductionEducationArray: RiskReductionEducationResolver,
                EncounterTypeArray: EncounterTypeResolver
            },
        }
        ]
    }
     
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PrepRoutingModule {

}
