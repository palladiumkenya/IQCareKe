import { MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WaitingListService } from './_services/waiting.service';
import { DialogService } from './_services/dialog.service';
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
    MatAutocompleteModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule,
    MatButtonModule
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
import { ClickNoneEventsDirectiveDirective } from './_directives/click-none-events-directive.directive';
import { AddWaitingListComponent } from './add-waiting-list/add-waiting-list.component';
import { MatconfirmdialogComponent } from './matconfirmdialog/matconfirmdialog.component';

@NgModule({
    imports: [
        CommonModule, MatAutocompleteModule,
        SharedRoutingModule, MatDatepickerModule,
        MatNativeDateModule, MatCardModule,
        MatTableModule, MatIconModule,
        MatPaginatorModule, ReactiveFormsModule,
        MatSelectModule, MatFormFieldModule,
        MatInputModule, MatButtonModule, MatDialogModule,
        CommonModule,
        SharedRoutingModule,
        MatCardModule,
        MatTableModule,
        MatIconModule,
        MatPaginatorModule,
        ReactiveFormsModule,
        MatSelectModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatDialogModule
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
        AdverseEventsTableComponent,
        ClickNoneEventsDirectiveDirective,
        CustomFormComponent,
        AddWaitingListComponent,
        MatconfirmdialogComponent
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
        WaitingListService,
        DialogService,
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
        AdverseEventsAssessmentComponent, MatconfirmdialogComponent
    ]
})
export class SharedModule { }
