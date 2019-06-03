import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { LeftnavComponent } from './leftnav/leftnav.component';
import { ClientbriefComponent } from './clientbrief/clientbrief.component';
import { ClientService } from './_services/client.service';
import { AlertComponent } from './alert/alert.component';
import { PnstracingService } from '../hts/_services/pnstracing.service';
import { PersonbriefComponent } from './personbrief/personbrief.component';
import {
    DateAdapter, MAT_DATE_FORMATS,
    MatCardModule, MatTableModule,
    MatIconModule, MatPaginatorModule,
    MAT_DATE_LOCALE,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule
} from '@angular/material';
import { AppDateAdapter } from './dateadapter/momentDateAdapter';
import { NotificationService } from './_services/notification.service';
import { AppLoadService } from './_services/appload.service';
import { AppStateService } from './_services/appstate.service';
import { ErrorHandlerService } from './_services/errorhandler.service';
import { PatientEncounterComponent } from './patient-encounter/patient-encounter.component';
import { CustomFormComponent } from './custom-form/custom-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AdverseEventsAssessmentComponent } from './common-components/adverse-events-assessment/adverse-events-assessment.component';
import { PatientChronicIllnessesComponent } from './common-components/patient-chronic-illnesses/patient-chronic-illnesses.component';
import { AllergiesComponent } from './common-components/allergies/allergies.component';
import { ChronicIllnessesTableComponent } from './common-components/chronic-illnesses-table/chronic-illnesses-table.component';
import { AllergiesTableComponent } from './common-components/allergies-table/allergies-table.component';
import { AdverseEventsTableComponent } from './common-components/adverse-events-table/adverse-events-table.component';

@NgModule({
    imports: [
        CommonModule, MatAutocompleteModule,
        SharedRoutingModule, MatDatepickerModule,
        MatNativeDateModule, MatCardModule,
        MatTableModule, MatIconModule,
        MatPaginatorModule, ReactiveFormsModule,
        MatSelectModule, MatFormFieldModule,
        MatInputModule, MatButtonModule
    ],
    declarations: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent,
        PersonbriefComponent,
        PatientEncounterComponent,
        CustomFormComponent,
        AdverseEventsAssessmentComponent,
        PatientChronicIllnessesComponent,
        AllergiesComponent,
        ChronicIllnessesTableComponent,
        AllergiesTableComponent,
        AdverseEventsTableComponent
    ],
    exports: [
        LeftnavComponent, AdverseEventsAssessmentComponent,
        ClientbriefComponent, AllergiesComponent,
        AlertComponent, PatientChronicIllnessesComponent,
        PersonbriefComponent, ChronicIllnessesTableComponent,
        PatientEncounterComponent, AllergiesTableComponent,
        CustomFormComponent, AdverseEventsTableComponent
    ],
    providers: [
        ClientService,
        NotificationService,
        PnstracingService,
        AppLoadService,
        AppStateService,
        ErrorHandlerService,
        { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
        { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
        { provide: DateAdapter, useClass: AppDateAdapter },
    ],
    entryComponents: [
        AllergiesComponent, PatientChronicIllnessesComponent,
        AdverseEventsAssessmentComponent
    ]
})
export class SharedModule { }
