import { RegistrationService } from './../registration/_services/registration.service';
import { ServicesResolver } from './services/services.resolver';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalComponent } from './portal/portal.component';
import { PersonHomeComponent } from './person-home/person-home.component';
import { NotificationService } from '../shared/_services/notification.service';
import { ServicesListComponent } from './services-list/services-list.component';
import {
    MatCardModule, MatDatepickerModule,
    MatNativeDateModule, MatFormFieldModule,
    MatInputModule, MatTableModule, MatPaginatorModule,
    MatSortModule, MatAutocompleteModule, MatButtonModule,
    MatButtonToggleModule, MatCheckboxModule, MatChipsModule,
    MatDialogModule, MatDividerModule, MatExpansionModule,
    MatGridListModule, MatIconModule, MatListModule, MatMenuModule,
    MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule,
    MatRippleModule, MatSelectModule, MatSidenavModule, MatSliderModule,
    MatSlideToggleModule, MatSnackBarModule, MatStepperModule,
    MatTabsModule, MatToolbarModule, MatTooltipModule
} from '@angular/material';
import { EnrollmentServicesComponent } from './enrollment/enrollment-services/enrollment-services.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { EnrollmentService } from '../registration/_services/enrollment.service';
import { ChronichIllnessComponent } from './chronich-illness/chronich-illness.component';
import { PatientRelationshipsComponent } from './patient-relationships/patient-relationships.component';
import { PatientAllergiesComponent } from './patient-allergies/patient-allergies.component';
import { PatientAppointmentComponent } from './patient-appointment/patient-appointment.component';

@NgModule({
    imports: [
        CommonModule,
        DashboardRoutingModule,
        MatCardModule,
        ReactiveFormsModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatFormFieldModule,
        MatInputModule,
        SharedModule,

        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatAutocompleteModule, MatButtonModule, MatButtonToggleModule,
        MatCardModule, MatCheckboxModule, MatChipsModule, MatDatepickerModule,
        MatDialogModule, MatDividerModule, MatExpansionModule,
        MatFormFieldModule, MatGridListModule, MatIconModule, MatInputModule,
        MatListModule, MatMenuModule, MatNativeDateModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
        MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule,
        MatSnackBarModule, MatStepperModule, MatTabsModule, MatToolbarModule, MatTooltipModule,
    ],
    declarations: [
        PortalComponent,
        PersonHomeComponent,
        ServicesListComponent,
        EnrollmentServicesComponent,
        ChronichIllnessComponent,
        PatientRelationshipsComponent,
        PatientAllergiesComponent,
        PatientAppointmentComponent
    ],
    providers: [
        NotificationService,
        ServicesResolver,
        RegistrationService,
        EnrollmentService
    ]
})
export class DashboardModule { }
