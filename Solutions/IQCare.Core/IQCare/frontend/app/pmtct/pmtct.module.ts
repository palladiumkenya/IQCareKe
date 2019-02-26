import { RecordsService } from './../records/_services/records.service';
///<reference path="maternity/maternity.component.ts"/>
import { PncService } from './_services/pnc.service';
import { CervicalCancerScreeningResultsResolver } from './_services/resolvers/cervical-cancer-screening-results.resolver';
import { FamilyPlanningMethodResolver } from './_services/resolvers/family-planning-method.resolver';
import { CervicalCancerScreeningMethodResolver } from './_services/resolvers/cervical-cancer-screening-method.resolver';
import { FinalPartnerHivResultResolver } from './_services/resolvers/final-partner-hivresult.resolver';
import { InfantDrugsStartContinueResolver } from './_services/resolvers/infant-drugs-start-continue.resolver';
import { InfantPncDrugResolver } from './_services/resolvers/infant-pnc-drug.resolver';
import { YesNoNaResolver } from './_services/resolvers/yes-no-na.resolver';
import { BabyConditionResolver } from './_services/resolvers/baby-condition.resolver';
import { FistulaScreeningResolver } from './_services/resolvers/fistula-screening.resolver';
import { CSectionSiteResolver } from './_services/resolvers/c-section-site.resolver';
import { EpisiotomyResolver } from './_services/resolvers/episiotomy.resolver';
import { PostPartumHaemorrhage } from './_services/resolvers/post-partum-haemorrhage.resolver';
import { LochiaResolver } from './_services/resolvers/lochia.resolver';
import { UterusResolver } from './_services/resolvers/uterus.resolver';
import { BreastResolver } from './_services/resolvers/breast.resolver';
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
import { PatientEducationExaminationComponent } from './anc/patient-education-examination/patient-education-examination.component';
import { AntenatalProfileComponent } from './anc/antenatal-profile/antenatal-profile.component';
import { HivStatusComponent } from './anc/hiv-status/hiv-status.component';
import { ClientMonitoringComponent } from './anc/client-monitoring/client-monitoring.component';
import { HaartProphylaxisComponent } from './anc/haart-prophylaxis/haart-prophylaxis.component';
import { VisitDetailsComponent } from './anc/visit-details/visit-details.component';
import { PmtctRoutingModule } from './pmtct-routing-module';
import { MaternityEncounterComponent } from './maternity/maternity-encounter/maternity-encounter.component';
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
import { PreventiveServicesComponent } from './anc/preventive-services/preventive-services.component';
import { PartnerTestingComponent } from './anc/partner-testing/partner-testing.component';
import { ReferralsComponent } from './anc/referrals/referrals.component';
import { NextAppointmentComponent } from './anc/next-appointment/next-appointment.component';
import { SharedModule } from '../shared/shared.module';
import { HeiComponent } from './hei/hei.component';
import { HeiVisitDetailsComponent } from './hei/hei-visit-details/hei-visit-details.component';
import { ImmunizationHistoryComponent } from './hei/immunization-history/immunization-history.component';
import { MilestonesComponent } from './hei/milestones/milestones.component';
import { MaternalhistoryComponent } from './hei/maternalhistory/maternalhistory.component';
import { MotherReceiveDrugsResolver } from './_services/motherreceivedrugs.resolver';
import { HeiMotherRegimenResolver } from './_services/heimotherregimen.resolver';
import { YesNoResolver } from './_services/yesno.resolver';
import { MotherDrugsAtInfantEnrollmentResolver } from './_services/motherdrugsatinfantenrollment.resolver';
import { HeiHivtestingComponent } from './hei/hei-hivtesting/hei-hivtesting.component';
import { InfantFeedingComponent } from './hei/infant-feeding/infant-feeding.component';
import { TbAssessmentComponent } from './hei/tb-assessment/tb-assessment.component';
import { RecordsModule } from '../records/records.module';
import { HeiOutcomeComponent } from './hei/hei-outcome/hei-outcome.component';
import { IptClientWorkupComponent } from './hei/ipt-client-workup/ipt-client-workup.component';
import { IptFollowUpComponent } from './hei/ipt-follow-up/ipt-follow-up.component';
import { IptOutcomeComponent } from './hei/ipt-outcome/ipt-outcome.component';
import { HeiHivTestTypesResolver } from './_services/resolvers/hei-hiv-testtypes.resolver';
import { HeiHivTestResultsResolver } from './_services/resolvers/hei-hiv-test-results.resolver';
import { HivtestingmodalComponent } from './hei/hei-hivtesting/hivtestingmodal/hivtestingmodal.component';
import { PncComponent } from './pnc/pnc.component';
import { PncMaternalhistoryComponent } from './pnc/pnc-maternalhistory/pnc-maternalhistory.component';
import { PncPostnatalexamComponent } from './pnc/pnc-postnatalexam/pnc-postnatalexam.component';
import { PncBabyexaminationComponent } from './pnc/pnc-babyexamination/pnc-babyexamination.component';
import { PncDrugadministrationComponent } from './pnc/pnc-drugadministration/pnc-drugadministration.component';
import { PncPartnertestingComponent } from './pnc/pnc-partnertesting/pnc-partnertesting.component';
import { PncPatienteducationComponent } from './pnc/pnc-patienteducation/pnc-patienteducation.component';
import { PncCervicalcancerscreeningComponent } from './pnc/pnc-cervicalcancerscreening/pnc-cervicalcancerscreening.component';
import { PncContraceptivehistoryComponent } from './pnc/pnc-contraceptivehistory/pnc-contraceptivehistory.component';
import { PncHivtestingComponent } from './pnc/pnc-hivtesting/pnc-hivtesting.component';
import { MaternityComponent } from './maternity/maternity.component';
import { MotherProfileComponent } from './maternity/mother-profile/mother-profile.component';
import { DiagnosisComponent } from './maternity/diagnosis/diagnosis.component';
import { DeliveryMaternityComponent } from './maternity/delivery-maternity/delivery-maternity.component';
import { DeliveryComponent } from './hei/delivery/delivery.component';
import { BabyComponent } from './maternity/baby/baby.component';
import { MaternityTestsComponent } from './maternity/maternity-tests/maternity-tests.component';
import { MaternalDrugAdministrationComponent } from './maternity/maternal-drug-administration/maternal-drug-administration.component';
import { HivFinalResultsResolver } from './_services/resolvers/hiv-final-results.resolver';
import { DischargeComponent } from './maternity/discharge/discharge.component';
import { MaternityReferralComponent } from './maternity/maternity-referral/maternity-referral.component';
import { MaternityNextAppointmentComponent } from './maternity/maternity-next-appointment/maternity-next-appointment.component';
import { MaternityHivTestComponent } from './maternity/maternity-hiv-test/maternity-hiv-test.component';
import { BloodLossResolver } from './_services/resolvers/blood-loss.resolver';
import { GenderResolver } from './_services/resolvers/gender.resolver';
import { ReferralResolver } from './_services/resolvers/referral.resolver';
import { PmtctTestTypeResolver } from './_services/resolvers/pmtctTestType.resolver';
import { TestKitNameResolver } from './_services/resolvers/test-kit-name.resolver';
import { HivTestResultResolver } from './_services/resolvers/hiv-test-result.resolver';
import { PncEncountersComponent } from './pnc/pnc-encounters/pnc-encounters.component';
import { CheckinComponent } from './checkin/checkin.component';
import { TbScreeningResolver } from './_services/resolvers/tb-screening.resolver';
import { AncHivtestingComponent } from './anc/anc-hivtesting/anc-hivtesting.component';
import { ANCHivStatusInitialVisitResolver } from './_services/resolvers/anc-hiv-status-initial-visit.resolver';
import { MotherExaminationResolver } from './_services/resolvers/motherexamination.resolver';
import { BabyExaminationResolver } from './_services/resolvers/baby-examination.resolver';
import { HeiMedicationComponent } from './hei/hei-medication/hei-medication.component';
import { BirthOutcomeResolver } from './_services/resolvers/BirthOutcomeResolver';
import { CounselledInfantFeedingResolver } from './_services/resolvers/counselled-infant-feeding.resolver';
import { ImmunizationComponent } from './hei/immunization-history/immunization/immunization.component';
import { BirthInfoGridComponent } from './maternity/baby/birth-info-grid/birth-info-grid.component';
import { AddBirthInfoComponent } from './maternity/baby/add-birth-info/add-birth-info.component';
import { AddBabyDialogComponent } from './maternity/baby/add-baby-dialog/add-baby-dialog.component';
import { MilestonesFormComponent } from './hei/milestones/milestones-form/milestones-form.component';
import { DataService } from '../shared/_services/data.service';
import { PriorHivStatusComponent } from './pnc/prior-hiv-status/prior-hiv-status.component';

@NgModule({
    imports: [
        CommonModule,
        PmtctRoutingModule,
        CommonModule, HttpClientModule, MatDatepickerModule, MatFormFieldModule,
        MatNativeDateModule, MatInputModule, MatFormFieldModule,
        MatTableModule, MatAutocompleteModule, MatButtonModule, MatButtonToggleModule, MatCardModule,
        MatCheckboxModule, MatChipsModule, MatDialogModule, MatDividerModule, MatExpansionModule,
        MatGridListModule, MatIconModule, MatListModule, MatMenuModule, MatPaginatorModule,
        MatProgressBarModule, MatProgressSpinnerModule, MatRadioModule, MatRippleModule, MatSelectModule, MatSidenavModule, MatSliderModule,
        MatSlideToggleModule, MatSnackBarModule, MatSortModule, MatStepperModule, MatTabsModule,
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
        MaternalhistoryComponent,
        HeiHivtestingComponent,
        InfantFeedingComponent,
        TbAssessmentComponent,
        HeiOutcomeComponent,
        IptClientWorkupComponent,
        IptFollowUpComponent,
        IptOutcomeComponent,
        HivtestingmodalComponent,
        PncComponent,
        PncMaternalhistoryComponent,
        PncPostnatalexamComponent,
        PncBabyexaminationComponent,
        PncDrugadministrationComponent,
        PncPartnertestingComponent,
        PncPatienteducationComponent,
        PncCervicalcancerscreeningComponent,
        PncContraceptivehistoryComponent,
        PncHivtestingComponent,
        MaternityComponent,
        MotherProfileComponent,
        DiagnosisComponent,
        DeliveryMaternityComponent,
        DeliveryComponent,
        BabyComponent,
        MaternityTestsComponent,
        MaternalDrugAdministrationComponent,
        DischargeComponent,
        MaternityReferralComponent,
        MaternityNextAppointmentComponent,
        MaternityHivTestComponent,
        PncEncountersComponent,
        CheckinComponent,
        MaternityEncounterComponent,
        AncHivtestingComponent,
        HeiMedicationComponent,
        ImmunizationComponent,
        BirthInfoGridComponent,
        AddBirthInfoComponent,
        AddBabyDialogComponent,
        MilestonesFormComponent,
        PriorHivStatusComponent
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
        HeiService,
        HeiHivTestTypesResolver,
        HeiHivTestResultsResolver,
        HivFinalResultsResolver,
        BreastResolver,
        UterusResolver,
        LochiaResolver,
        PostPartumHaemorrhage,
        EpisiotomyResolver,
        CSectionSiteResolver,
        FistulaScreeningResolver,
        BabyConditionResolver,
        YesNoNaResolver,
        InfantPncDrugResolver,
        BloodLossResolver,
        GenderResolver,
        ReferralResolver,
        PmtctTestTypeResolver,
        TestKitNameResolver,
        HivTestResultResolver,
        InfantPncDrugResolver,
        InfantDrugsStartContinueResolver,
        FinalPartnerHivResultResolver,
        CervicalCancerScreeningMethodResolver,
        FamilyPlanningMethodResolver,
        CervicalCancerScreeningResultsResolver,
        PncService,
        TbScreeningResolver,
        ANCHivStatusInitialVisitResolver,
        MotherExaminationResolver,
        BabyExaminationResolver,
        BirthOutcomeResolver,
        CounselledInfantFeedingResolver,
        DataService,
        RecordsService
    ],
    entryComponents: [
        IptClientWorkupComponent,
        IptFollowUpComponent,
        IptOutcomeComponent,
        HivtestingmodalComponent,
        HivStatusComponent,
        CheckinComponent,
        ImmunizationComponent,
        AddBabyDialogComponent,
        MilestonesFormComponent
    ],
    exports: [
        IptClientWorkupComponent,
        IptFollowUpComponent,
        IptOutcomeComponent
    ]
})
export class PmtctModule { }
