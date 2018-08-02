import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile/profile.component';
import { PatientEducationExaminationComponent } from './patient-education-examination/patient-education-examination.component';
import { AntenatalProfileComponent } from './antenatal-profile/antenatal-profile.component';
import { HivStatusComponent } from './hiv-status/hiv-status.component';
import { ClientMonitoringComponent } from './client-monitoring/client-monitoring.component';
import { HaartProphylaxisComponent } from './haart-prophylaxis/haart-prophylaxis.component';
import { VisitDetailsComponent } from './visit-details/visit-details.component';
import { PmtctRoutingModule } from './pmtct-routing-module';
import {
    MatAutocompleteModule,
    MatButtonModule, MatButtonToggleModule, MatCardModule, MatCheckboxModule, MatChipsModule,
    MatDatepickerModule, MatDialogModule, MatDividerModule, MatExpansionModule, MatFormFieldModule,
    MatGridListModule, MatIconModule, MatInputModule, MatListModule, MatMenuModule,
    MatNativeDateModule, MatPaginatorModule, MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule,
    MatSelectModule, MatSidenavModule, MatSliderModule, MatSlideToggleModule, MatSnackBarModule,
    MatSortModule, MatStepperModule, MatTableModule, MatTabsModule, MatToolbarModule, MatTooltipModule, 
} from '@angular/material';
import { AncComponent } from './anc/anc.component';
import { PreventiveServicesComponent } from './preventive-services/preventive-services.component';
import { PartnerTestingComponent } from './partner-testing/partner-testing.component';
import { ReferralsComponent } from './referrals/referrals.component';
import { NextAppointmentComponent } from './next-appointment/next-appointment.component';


@NgModule({
    imports: [
        CommonModule,
        PmtctRoutingModule,
        CommonModule, HttpClientModule,  MatDatepickerModule, MatFormFieldModule,
        MatNativeDateModule, MatInputModule, MatDatepickerModule, MatFormFieldModule, MatNativeDateModule,
        MatTableModule, MatAutocompleteModule, MatButtonModule, MatButtonToggleModule, MatCardModule,
        MatCheckboxModule, MatChipsModule, MatDatepickerModule, MatDialogModule, MatDividerModule, MatExpansionModule,
        MatGridListModule, MatIconModule, MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule, MatSidenavModule, MatSliderModule,
        MatSlideToggleModule, MatSnackBarModule, MatSortModule, MatStepperModule, MatTableModule, MatTabsModule,
        MatToolbarModule, MatTooltipModule, ReactiveFormsModule, MatInputModule
    ],
    declarations: [
             ProfileComponent, 
             PatientEducationExaminationComponent,
             AntenatalProfileComponent,
             HivStatusComponent,
             ClientMonitoringComponent,
             HaartProphylaxisComponent,
             VisitDetailsComponent,
             AncComponent,
             PreventiveServicesComponent,
             PartnerTestingComponent,
             ReferralsComponent,
             NextAppointmentComponent
            ]
})
export class PmtctModule { }
