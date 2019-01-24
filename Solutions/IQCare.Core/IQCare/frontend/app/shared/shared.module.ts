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

@NgModule({
    imports: [
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
        MatButtonModule
    ],
    declarations: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent,
        PersonbriefComponent,
        PatientEncounterComponent,
        CustomFormComponent
    ],
    exports: [
        LeftnavComponent,
        ClientbriefComponent,
        AlertComponent,
        PersonbriefComponent,
        PatientEncounterComponent,
        CustomFormComponent
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
    ]
})
export class SharedModule { }
