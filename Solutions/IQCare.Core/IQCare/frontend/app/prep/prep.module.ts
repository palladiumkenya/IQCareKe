import { PrepService } from './_services/prep.service';
import { STIScreeningTreatmentResolver } from './_services/STIScreeningTreatment.resolver';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepRoutingModule } from './prep-routing.module';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';
import {
    MatStepperModule, MatButtonModule,
    MatFormFieldModule, MatSelectModule,
    MatNativeDateModule, MatDatepickerModule,
    MatInputModule, MatAutocompleteModule, MatTableModule, MatDividerModule, MatDialogModule, MatIconModule,
    MatPaginatorModule,MatRadioModule
} from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { PrepSTIScreeningTreatmentComponent } from './encounter-components/prep-sti-screening-treatment/prep-sti-screening-treatment.component';
import { CircumcisionStatusComponent } from './encounter-components/circumcision-status/circumcision-status.component';
import { FertilityIntentionComponent } from './encounter-components/fertility-intention/fertility-intention.component';
import { PregnancyOutcomeComponent } from './encounter-components/pregnancy-outcome/pregnancy-outcome.component';
import { SharedModule } from '../shared/shared.module';
import { PrepStatusComponent } from './encounter-components/prep-status/prep-status.component';
import { YesNoResolver } from '../pmtct/_services/yesno.resolver';
import { YesNoUnknownResolver } from './_services/YesNoUnknown.resolver';
import { FamilyPlanningMethodResolver } from '../pmtct/_services/resolvers/family-planning-method.resolver';
import { PlanningPregnancyResolver } from './_services/planningPregnancy.resolver';
import { YesNoDontKnowResolver } from './_services/YesNoDont-Know.resolver';
import { PregnancyOutcomeResolver } from './_services/PregnancyOutcome.resolver';
import { PrepContraindicationsResolver } from './_services/prep-contraindications.resolver';
import { PrepStatusResolver } from './_services/prep-status.resolver';
import { HTSEncounterResolver } from './_services/resolvers/htsencounter.resolver';
import { PersonCurrentVitalsResolver } from './_services/resolvers/personvitals.resolver';
import {RiskEncounterResolver} from './_services/resolvers/riskencounter.resolver';
import {
    AssessmentOutcomeResolver, ClientsBehaviourRiskResolver, SexualPartnetHivStatusProfileResolver,
    RiskReductionEducationResolver, ReferralPreventionServicesResolver, ClientWillingTakePrepResolver
    , RiskEducationResolver, BehaviourRiskAssessmentResolver, EncounterTypeResolver, PartnerCCCEnrollmentResolver,
    PatientIdentifierResolver, ARTStartDateResolver, PartnerHIVStatusResolver, DurationResolver, SexWithoutCondomResolver,
    HivPartnerResolver,PrepDeclineResolver
} from './_services/resolvers/prepriskassessment.resolver';
import {
    PrepAdherenceResolver, AdherenceAssessmentReasonsResolver, RefillPrepStatusResolver,
    PrepDiscontinueReasonResolver, AdherenceCounsellingResolver,AppointmentGivenResolver,PrepAppointmentReasonResolver
} from './_services/resolvers/prepmonthlyrefillresolver';
import {
    PrepCareEndReasonResolver
} from './_services/resolvers/prepcareendreason.resolver';
import { PrepRiskassessmentComponent } from './prep-riskassessment/prep-riskassessment.component';
import { PrepAppointmentComponent } from './encounter-components/prep-appointment/prep-appointment.component';
import { ReasonsPrepAppointmentNotGivenResolver } from './_services/reasons-prep-appointment-notgiven.resolver';
import { PrepCheckinComponent } from './prep-checkin/prep-checkin.component';
import { PrepEncounterTypeResolver } from './_services/prep-encounter-type.resolver';
import { PregnancyStatusResolver } from './_services/pregnancy-status.resolver';
import { ScreenedForSTIResolver } from './_services/screened-sti.resolver';
import { PrepCareendComponent } from './prep-careend/prep-careend.component';
import { PrepMonthlyrefillComponent } from './prep-monthlyrefill/prep-monthlyrefill.component';
import { PrepLabsgridComponent } from './prep-labsgrid/prep-labsgrid.component';
import { PrepAppointmentgridComponent } from './prep-appointmentgrid/prep-appointmentgrid.component';
import { PrepHtsencountersgridComponent } from './prep-htsencountersgrid/prep-htsencountersgrid.component';
import { PrepEncounterformlistComponent } from './prep-encounterformlist/prep-encounterformlist.component';
import { PrepPatientvitalsinfoComponent } from './prep-patientvitalsinfo/prep-patientvitalsinfo.component';
import { PrepRiskassessmentgriddetailsComponent } from './prep-riskassessmentgriddetails/prep-riskassessmentgriddetails.component';

@NgModule({
    declarations: [
        PrepEncounterHistoryComponent,
        PrepEncounterComponent,
        PrepSTIScreeningTreatmentComponent,
        CircumcisionStatusComponent,
        FertilityIntentionComponent,
        PregnancyOutcomeComponent,
        PrepStatusComponent,
        PrepRiskassessmentComponent,
        PrepAppointmentComponent,
        PrepCheckinComponent,
        PrepCareendComponent,
        PrepMonthlyrefillComponent,
        PrepLabsgridComponent,
        PrepAppointmentgridComponent,
        PrepHtsencountersgridComponent,
        PrepEncounterformlistComponent,
        PrepPatientvitalsinfoComponent,
        PrepRiskassessmentgriddetailsComponent
    ],
    imports: [
        SharedModule, MatDatepickerModule, MatNativeDateModule,
        CommonModule, MatStepperModule, ReactiveFormsModule,
        MatButtonModule, MatFormFieldModule, MatSelectModule,
        MatInputModule, MatAutocompleteModule, MatTableModule,
        PrepRoutingModule, MatDividerModule, MatDialogModule,
        MatPaginatorModule, MatRadioModule,
        MatIconModule, ReactiveFormsModule, FormsModule
    ],
    providers: [
        YesNoResolver, YesNoUnknownResolver,
        STIScreeningTreatmentResolver, FamilyPlanningMethodResolver,
        PlanningPregnancyResolver, YesNoDontKnowResolver, PregnancyOutcomeResolver,
        PrepContraindicationsResolver, PrepStatusResolver, AssessmentOutcomeResolver,
        ClientsBehaviourRiskResolver, SexualPartnetHivStatusProfileResolver,
        RiskReductionEducationResolver, ReferralPreventionServicesResolver,
        ClientWillingTakePrepResolver, RiskEducationResolver, BehaviourRiskAssessmentResolver,
        EncounterTypeResolver, PrepService, PartnerCCCEnrollmentResolver,
        PatientIdentifierResolver, ARTStartDateResolver, PartnerHIVStatusResolver,
        DurationResolver, SexWithoutCondomResolver, HivPartnerResolver,
        ReasonsPrepAppointmentNotGivenResolver, PrepEncounterTypeResolver,
        PregnancyStatusResolver, ScreenedForSTIResolver,
        PrepCareEndReasonResolver, PrepAdherenceResolver, AdherenceAssessmentReasonsResolver, RefillPrepStatusResolver,
        PrepDiscontinueReasonResolver, AdherenceCounsellingResolver, AppointmentGivenResolver, PrepAppointmentReasonResolver,
        HTSEncounterResolver, PersonCurrentVitalsResolver, RiskEncounterResolver,PrepDeclineResolver
    ],
    entryComponents: [
        PrepCheckinComponent
    ]
})
export class PrepModule { }
