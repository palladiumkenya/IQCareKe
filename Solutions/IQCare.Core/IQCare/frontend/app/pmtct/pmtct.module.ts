import { HeiService } from './_services/hei.service';
import { PrimaryCareGiverResolver } from './_services/primarycaregiver.resolver';
import { MotherStateResolver } from './_services/motherstate.resolver';
import { ARVProphylaxisResolver } from './_services/arvprophylaxis.resolver';
import { DeliveryModeResolver } from './_services/deliverymode.resolver';
import { PlaceOfDeliveryResolver } from './_services/placeofdelivery.resolver';
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
import { SharedModule } from '../shared/shared.module';
import { HeiComponent } from './hei/hei.component';
import { HeiVisitDetailsComponent } from './hei/hei-visit-details/hei-visit-details.component';
import { ImmunizationHistoryComponent } from './hei/immunization-history/immunization-history.component';
import { MilestonesComponent } from './hei/milestones/milestones.component';
import { DeliveryComponent } from './hei/delivery/delivery.component';
import { MaternalhistoryComponent } from './hei/maternalhistory/maternalhistory.component';
import { MotherReceiveDrugsResolver } from './_services/motherreceivedrugs.resolver';
import { HeiMotherRegimenResolver } from './_services/heimotherregimen.resolver';
import { YesNoResolver } from './_services/yesno.resolver';
import { MotherDrugsAtInfantEnrollmentResolver } from './_services/motherdrugsatinfantenrollment.resolver';
import { HeiHivtestingComponent } from './hei/hei-hivtesting/hei-hivtesting.component';
import { InfantFeedingComponent } from './hei/infant-feeding/infant-feeding.component';
import { TbAssessmentComponent } from './hei/tb-assessment/tb-assessment.component';
import { InlineSearchComponent } from '../records/inline-search/inline-search.component';
import { RecordsModule } from '../records/records.module';
import { HeiOutcomeComponent } from './hei/hei-outcome/hei-outcome.component';
import { HeiOutcomeOptionsResolver } from './_services/hei-outcome-options.resolver.service';


@NgModule({
    imports: [
        CommonModule,
        PmtctRoutingModule,
        CommonModule, HttpClientModule, MatDatepickerModule, MatFormFieldModule,
        MatNativeDateModule, MatInputModule, MatDatepickerModule, MatFormFieldModule, MatNativeDateModule,
        MatTableModule, MatAutocompleteModule, MatButtonModule, MatButtonToggleModule, MatCardModule,
        MatCheckboxModule, MatChipsModule, MatDatepickerModule, MatDialogModule, MatDividerModule, MatExpansionModule,
        MatGridListModule, MatIconModule, MatListModule, MatMenuModule, MatNativeDateModule, MatPaginatorModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule, MatSidenavModule, MatSliderModule,
        MatSlideToggleModule, MatSnackBarModule, MatSortModule, MatStepperModule, MatTableModule, MatTabsModule,
        MatToolbarModule, MatTooltipModule, ReactiveFormsModule, MatInputModule, SharedModule, RecordsModule
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
        NextAppointmentComponent,
        HeiComponent,
        HeiVisitDetailsComponent,
        ImmunizationHistoryComponent,
        MilestonesComponent,
        DeliveryComponent,
        MaternalhistoryComponent,
        HeiHivtestingComponent,
        InfantFeedingComponent,
        TbAssessmentComponent,
        InfantFeedingComponent,
        HeiOutcomeComponent
    ],
    providers: [
        PlaceOfDeliveryResolver,
        DeliveryModeResolver,
        ARVProphylaxisResolver,
        MotherStateResolver,
        MotherReceiveDrugsResolver,
        HeiMotherRegimenResolver,
        YesNoResolver,
        MotherDrugsAtInfantEnrollmentResolver,
        PrimaryCareGiverResolver,
        HeiService
    ],
    entryComponents: [

    ]
})
export class PmtctModule { }
