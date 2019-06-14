import { YesNoResolver } from './../pmtct/_services/yesno.resolver';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';
import { STIScreeningTreatmentResolver } from './_services/STIScreeningTreatment.resolver';
import { YesNoUnknownResolver } from './_services/YesNoUnknown.resolver';
import { FamilyPlanningMethodResolver } from '../pmtct/_services/resolvers/family-planning-method.resolver';
import { PlanningPregnancyResolver } from './_services/planningPregnancy.resolver';
import { YesNoDontKnowResolver } from './_services/YesNoDont-Know.resolver';
import { PregnancyOutcomeResolver } from './_services/PregnancyOutcome.resolver';
import { PrepStatusResolver } from './_services/prep-status.resolver';
import { PrepContraindicationsResolver } from './_services/prep-contraindications.resolver';
import {
    AssessmentOutcomeResolver, ClientsBehaviourRiskResolver, SexualPartnetHivStatusProfileResolver,
    RiskReductionEducationResolver, ReferralPreventionServicesResolver, ClientWillingTakePrepResolver
    , RiskEducationResolver, BehaviourRiskAssessmentResolver, EncounterTypeResolver, PartnerCCCEnrollmentResolver,
    PatientIdentifierResolver, ARTStartDateResolver, PartnerHIVStatusResolver, DurationResolver, SexWithoutCondomResolver,
    HivPartnerResolver
} from './_services/resolvers/prepriskassessment.resolver';
import { PrepRiskassessmentComponent } from './prep-riskassessment/prep-riskassessment.component';
import { ReasonsPrepAppointmentNotGivenResolver } from './_services/reasons-prep-appointment-notgiven.resolver';
import { PrepEncounterTypeResolver } from './_services/prep-encounter-type.resolver';
import { PregnancyStatusResolver } from './_services/pregnancy-status.resolver';
import { ScreenedForSTIResolver } from './_services/screened-sti.resolver';

const routes: Routes = [
    {
        path: ':patientId/:personId/:serviceId',
        component: PrepEncounterHistoryComponent,
        pathMatch: 'full',
        resolve: {
            prepEncounterTypeOption: PrepEncounterTypeResolver
        }
    },
    {
        path: 'encounter/:patientId/:personId/:patientEncounterId/:patientMasterVisitId',
        component: PrepEncounterComponent,
        resolve: {
            yesNoOptions: YesNoResolver,
            stiScreeningTreatmentOptions: STIScreeningTreatmentResolver,
            yesNoUnknownOptions: YesNoUnknownResolver,
            familyPlanningMethodsOptions: FamilyPlanningMethodResolver,
            planningPregnancyOptions: PlanningPregnancyResolver,
            yesNoDontKnowOptions: YesNoDontKnowResolver,
            pregnancyOutcomeOptions: PregnancyOutcomeResolver,
            prepStatusOptions: PrepStatusResolver,
            prepContraindicationsOptions: PrepContraindicationsResolver,
            reasonsPrepAppointmentNotGivenOptions: ReasonsPrepAppointmentNotGivenResolver,
            pregnancyStatusOptions: PregnancyStatusResolver,
            screenedForSTIOptions: ScreenedForSTIResolver
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
                    EncounterTypeArray: EncounterTypeResolver,
                    PartnerCCCEnrollmentArray: PartnerCCCEnrollmentResolver,
                    PatientIdentifierArray: PatientIdentifierResolver,
                    ARTStartDateArray: ARTStartDateResolver,
                    PartnerHIVStatusArray: PartnerHIVStatusResolver,
                    DurationArray: DurationResolver,
                    SexWithoutCondomArray: SexWithoutCondomResolver,
                    HivPartnerArray: HivPartnerResolver


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
                    EncounterTypeArray: EncounterTypeResolver,
                    PartnerCCCEnrollmentArray: PartnerCCCEnrollmentResolver,
                    PatientIdentifierArray: PatientIdentifierResolver,
                    ARTStartDateArray: ARTStartDateResolver,
                    PartnerHIVStatusArray: PartnerHIVStatusResolver,
                    DurationArray: DurationResolver,
                    SexWithoutCondomArray: SexWithoutCondomResolver,
                    HivPartnerArray: HivPartnerResolver
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
