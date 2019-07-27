import { YesNoResolver } from './../pmtct/_services/yesno.resolver';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PrepEncounterformlistComponent } from './prep-encounterformlist/prep-encounterformlist.component';
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
    HivPartnerResolver, PrepDeclineResolver
} from './_services/resolvers/prepriskassessment.resolver';
import {
    PrepCareEndReasonResolver
} from './_services/resolvers/prepcareendreason.resolver';
import { PrepRiskassessmentComponent } from './prep-riskassessment/prep-riskassessment.component';
import { ReasonsPrepAppointmentNotGivenResolver } from './_services/reasons-prep-appointment-notgiven.resolver';
import { PrepEncounterTypeResolver } from './_services/prep-encounter-type.resolver';
import { PregnancyStatusResolver } from './_services/pregnancy-status.resolver';
import { ScreenedForSTIResolver } from './_services/screened-sti.resolver';
import { PrepCareendComponent } from './prep-careend/prep-careend.component';
import {
    PrepAdherenceResolver, AdherenceAssessmentReasonsResolver, RefillPrepStatusResolver,
    PrepDiscontinueReasonResolver, AdherenceCounsellingResolver, AppointmentGivenResolver, PrepAppointmentReasonResolver
} from './_services/resolvers/prepmonthlyrefillresolver';
import { PrepMonthlyrefillComponent } from './prep-monthlyrefill/prep-monthlyrefill.component';
import { HTSEncounterResolver } from './_services/resolvers/htsencounter.resolver';
import { PersonCurrentVitalsResolver } from './_services/resolvers/personvitals.resolver';
import { RiskEncounterResolver } from './_services/resolvers/riskencounter.resolver';
const routes: Routes = [
    {
        path: ':patientId/:personId/:serviceId',
        component: PrepEncounterHistoryComponent,
        pathMatch: 'full',
        resolve: {
            prepEncounterTypeOption: PrepEncounterTypeResolver,
            HTSEncounterArray: HTSEncounterResolver,
            PersonVitalsArray: PersonCurrentVitalsResolver,
            RiskAssessmentArray: RiskEncounterResolver
        }
    },

    {
        path: 'prepformslist',
        children: [
            {
                path: ':patientId/:personId/:serviceId',
                component: PrepEncounterformlistComponent,
                resolve: {
                    prepEncounterTypeOption: PrepEncounterTypeResolver
                }
            }
        ]
    },
    {
        path: 'prepcareend',
        children: [
            {
                path: ':patientId/:personId/:serviceId',
                component: PrepCareendComponent,
                pathMatch: 'full',
                resolve: {
                    careendreasonoptions: PrepCareEndReasonResolver,
                    EncounterTypeArray: EncounterTypeResolver
                }
            },
            {
                path: ':patientId/:personId/:serviceId/:patientMasterVisitId/:edit',
                component: PrepCareendComponent,
                pathMatch: 'full',
                resolve: {
                    careendreasonoptions: PrepCareEndReasonResolver,
                    EncounterTypeArray: EncounterTypeResolver
                }
            }




        ]

    },
    {
        path: 'encounter',
        children: [
            {
                path: ':patientId/:personId/:patientEncounterId/:patientMasterVisitId',
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
                path: ':patientId/:personId/:patientEncounterId/:patientMasterVisitId/:edit',
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
            }
        ]
    },
    {
        path: 'monthlyrefill',
        children: [
            {
                path: ':patientId/:personId/:serviceId',
                component: PrepMonthlyrefillComponent,
                resolve: {
                    clientsBehaviourRiskArray: ClientsBehaviourRiskResolver,
                    sexualPartnerHivStatusArray: SexualPartnetHivStatusProfileResolver,
                    PrepAdherenceArray: PrepAdherenceResolver,
                    AdherenceAssessmentReasonArray: AdherenceAssessmentReasonsResolver,
                    RefillPrepStatusArray: RefillPrepStatusResolver,
                    PrepDiscontinueReasonArray: PrepDiscontinueReasonResolver,
                    EncounterTypeArray: EncounterTypeResolver,
                    AdherenceCounsellingArray: AdherenceCounsellingResolver,
                    yesNoOptions: YesNoResolver,
                    AppointmentGivenArray: AppointmentGivenResolver,
                    PrepAppointmentReasonArray: PrepAppointmentReasonResolver
                }



            },
            {
                path: ':patientId/:personId/:serviceId/:patientMasterVisitId',
                component: PrepMonthlyrefillComponent,
                resolve: {
                    clientsBehaviourRiskArray: ClientsBehaviourRiskResolver,
                    sexualPartnerHivStatusArray: SexualPartnetHivStatusProfileResolver,
                    PrepAdherenceArray: PrepAdherenceResolver,
                    AdherenceAssessmentReasonArray: AdherenceAssessmentReasonsResolver,
                    RefillPrepStatusArray: RefillPrepStatusResolver,
                    PrepDiscontinueReasonArray: PrepDiscontinueReasonResolver,
                    EncounterTypeArray: EncounterTypeResolver,
                    AdherenceCounsellingArray: AdherenceCounsellingResolver,
                    yesNoOptions: YesNoResolver,
                    AppointmentGivenArray: AppointmentGivenResolver,
                    PrepAppointmentReasonArray: PrepAppointmentReasonResolver
                }
            }
        ]
    },
    {

        path: 'riskassessment',
        children: [
            {
                path: ':patientId/:personId/:serviceId',
                component: PrepRiskassessmentComponent,
                resolve: {
                    assessmentOutComeArray: AssessmentOutcomeResolver,
                    careendreasonoptions: PrepDeclineResolver,
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
                    careendreasonoptions: PrepDeclineResolver,
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
