import { STIScreeningTreatmentResolver } from './_services/STIScreeningTreatment.resolver';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrepEncounterHistoryComponent } from './prep-encounter-history/prep-encounter-history.component';
import { PrepRoutingModule } from './prep-routing.module';
import { PrepEncounterComponent } from './prep-encounter/prep-encounter.component';
import {
    MatStepperModule, MatButtonModule,
    MatFormFieldModule, MatSelectModule,
    MatNativeDateModule, MatDatepickerModule,
    MatInputModule, MatAutocompleteModule
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

@NgModule({
    declarations: [
        PrepEncounterHistoryComponent,
        PrepEncounterComponent,
        PrepSTIScreeningTreatmentComponent,
        CircumcisionStatusComponent,
        FertilityIntentionComponent,
        PregnancyOutcomeComponent,
        PrepStatusComponent
    ],
    imports: [
        SharedModule, MatDatepickerModule, MatNativeDateModule,
        CommonModule, MatStepperModule, ReactiveFormsModule,
        MatButtonModule, MatFormFieldModule, MatSelectModule,
        MatInputModule, MatAutocompleteModule,
        PrepRoutingModule
    ],
    providers: [
        YesNoResolver, YesNoUnknownResolver,
        STIScreeningTreatmentResolver, FamilyPlanningMethodResolver,
        PlanningPregnancyResolver, YesNoDontKnowResolver, PregnancyOutcomeResolver
    ]
})
export class PrepModule { }
