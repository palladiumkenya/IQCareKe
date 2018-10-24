import { CervicalCancerScreeningResultsResolver } from './_services/resolvers/cervical-cancer-screening-results.resolver';
import { FamilyPlanningMethodResolver } from './_services/resolvers/family-planning-method.resolver';
import { CervicalCancerScreeningMethodResolver } from './_services/resolvers/cervical-cancer-screening-method.resolver';
import { FinalPartnerHivResultResolver } from './_services/resolvers/final-partner-hivresult.resolver';
import { InfantPncDrugResolver } from './_services/resolvers/infant-pnc-drug.resolver';
import { YesNoNaResolver } from './_services/resolvers/yes-no-na.resolver';
import { BabyConditionResolver } from './_services/resolvers/baby-condition.resolver';
import { FistulaScreeningResolver } from './_services/resolvers/fistula-screening.resolver';
import { CSectionSiteResolver } from './_services/resolvers/c-section-site.resolver';
import { EpisiotomyResolver } from './_services/resolvers/episiotomy.resolver';
import { PostPartumHaemorrhage } from './_services/resolvers/post-partum-haemorrhage.resolver';
import { LochiaResolver } from './_services/resolvers/lochia.resolver';
import { BreastResolver } from './_services/resolvers/breast.resolver';
import { PncComponent } from './pnc/pnc.component';
import { PrimaryCareGiverResolver } from './_services/primarycaregiver.resolver';
import { HeiComponent } from './hei/hei.component';
import { PreventiveServicesComponent } from './preventive-services/preventive-services.component';
import { HaartProphylaxisComponent } from './haart-prophylaxis/haart-prophylaxis.component';
import { ClientMonitoringComponent } from './client-monitoring/client-monitoring.component';
import { PatientEducationExaminationComponent } from './patient-education-examination/patient-education-examination.component';
import { AncComponent } from './anc/anc.component';

import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VisitDetailsComponent } from './visit-details/visit-details.component';
import { PlaceOfDeliveryResolver } from './_services/placeofdelivery.resolver';
import { DeliveryModeResolver } from './_services/deliverymode.resolver';
import { ARVProphylaxisResolver } from './_services/arvprophylaxis.resolver';
import { MotherStateResolver } from './_services/motherstate.resolver';
import { MotherReceiveDrugsResolver } from './_services/motherreceivedrugs.resolver';
import { HeiMotherRegimenResolver } from './_services/heimotherregimen.resolver';
import { YesNoResolver } from './_services/yesno.resolver';
import { MotherDrugsAtInfantEnrollmentResolver } from './_services/motherdrugsatinfantenrollment.resolver';
import { InfantFeedingOptionsResolver } from './_services/infant-feeding-options.resolver.service';
import { ImmunizationPeriodOptionsResolverService } from './_services/immunization-period-options-resolver.service';
import { ImmunizationGivenOptionsResolverService } from './_services/immunization-given-options-resolver.service';
import { MilestonesAssessedOptionsResolverService } from './_services/milestones-assessed-options-resolver.service';
import { MilestonesStatusOptionsResolverService } from './_services/milestones-status-options-resolver.service';
import { HeiOutcomeOptionsResolver } from './_services/hei-outcome-options.resolver.service';
import { SputumSmearResolverService } from './_services/sputum-smear-resolver.service';
import { GeneXpertResolverService } from './_services/gene-xpert-resolver.service';
import { ChestXrayResolverService } from './_services/chest-xray-resolver.service';
import { TbScreeningOutcomeResolverService } from './_services/tb-screening-outcome-resolver.service';
import { HeiHivTestTypesResolver } from './_services/resolvers/hei-hiv-testtypes.resolver';
import { HeiHivTestResultsResolver } from './_services/resolvers/hei-hiv-test-results.resolver';
import { HivFinalResultsResolver } from './_services/resolvers/hiv-final-results.resolver';
import { UterusResolver } from './_services/resolvers/uterus.resolver';
import { MaternityComponent } from './maternity/maternity.component';
import { InfantDrugsStartContinueResolver } from './_services/resolvers/infant-drugs-start-continue.resolver';
import {IptoutcomeResolverService} from './_services/resolvers/iptoutcome-resolver.service';
import {MedicationResolverService} from './_services/resolvers/medication-resolver.service';
import {MedicationPlanResolverService} from './_services/resolvers/medication-plan-resolver.service';
import {MaternityComponent} from './maternity/maternity.component';
import {GenderResolver} from './_services/resolvers/gender.resolver';
import {ReferralResolver} from './_services/resolvers/referral.resolver';
import {PmtctTestTypeResolver} from './_services/resolvers/pmtctTestType.resolver';
import {TestKitNameResolver} from './_services/resolvers/test-kit-name.resolver';
import {HivTestResultResolver} from './_services/resolvers/hiv-test-result.resolver';
import {BloodLossResolver} from './_services/resolvers/blood-loss.resolver';


const routes: Routes = [
    {
        path: '',
        component: VisitDetailsComponent,
        pathMatch: 'full',
    },
    {
        path: 'anc/:patientId/:personId/:serviceAreaId',
        component: AncComponent,
        pathMatch: 'full',
    },
    {
        path: 'pex',
        component: PatientEducationExaminationComponent,
        pathMatch: 'full',
    },
    {
        path: 'cm',
        component: ClientMonitoringComponent,
        pathMatch: 'full',
    },
    {
        path: 'haart',
        component: HaartProphylaxisComponent,
        pathMatch: 'full',
    },
    {
        path: 'ps',
        component: PreventiveServicesComponent,
        pathMatch: 'full',
    },
    {
        path: 'hei/:patientId/:personId/:serviceAreaId',
        component: HeiComponent,
        pathMatch: 'full',
        resolve: {
            placeofdeliveryOptions: PlaceOfDeliveryResolver,
            deliveryModeOptions: DeliveryModeResolver,
            arvprophylaxisOptions: ARVProphylaxisResolver,
            motherstateOptions: MotherStateResolver,
            motherreceivedrugsOptions: MotherReceiveDrugsResolver,
            heimotherregimenOptions: HeiMotherRegimenResolver,
            yesnoOptions: YesNoResolver,
            primarycaregiverOptions: PrimaryCareGiverResolver,
            motherdrugsatinfantenrollmentOptions: MotherDrugsAtInfantEnrollmentResolver,
            infantFeedingOptions: InfantFeedingOptionsResolver,
            immunizationPeriodOptions: ImmunizationPeriodOptionsResolverService,
            immunizationGivenOptions: ImmunizationGivenOptionsResolverService,
            milestoneAssessedOptions: MilestonesAssessedOptionsResolverService,
            milestoneStatusOptions: MilestonesStatusOptionsResolverService,
            heiOutcomeOptions: HeiOutcomeOptionsResolver,
            sputumSmearOptions: SputumSmearResolverService,
            geneXpertOptions: GeneXpertResolverService,
            chestXrayOptions: ChestXrayResolverService,
            tbScreeningOutComeOptions: TbScreeningOutcomeResolverService,
            heiHivTestingOptions: HeiHivTestTypesResolver,
            heiHivTestingResultsOptions: HeiHivTestResultsResolver,
            iptOutcomeOptions: IptoutcomeResolverService,
            medicationOptions: MedicationResolverService,
            medicatinPlanOptions: MedicationPlanResolverService
        }
    },
    {
        path: 'pnc/:patientId/:personId/:serviceAreaId',
        component: PncComponent,
        pathMatch: 'full',
        resolve: {
            yesnoOptions: YesNoResolver,
            hivFinalResultsOptions: HivFinalResultsResolver,
            deliveryModeOptions: DeliveryModeResolver,
            breastOptions: BreastResolver,
            uterusOptions: UterusResolver,
            lochiaOptions: LochiaResolver,
            postpartumhaemorrhageOptions: PostPartumHaemorrhage,
            episiotomyOptions: EpisiotomyResolver,
            cSectionSiteOptions: CSectionSiteResolver,
            fistulaScreeningOptions: FistulaScreeningResolver,
            babyConditionOptions: BabyConditionResolver,
            yesNoNaOptions: YesNoNaResolver,
            infantPncDrugOptions: InfantPncDrugResolver,
            infantPncDrugOptions: InfantPncDrugResolver,
            infantDrugsStartContinueOptions: InfantDrugsStartContinueResolver,
            finalPartnerHivResultOptions: FinalPartnerHivResultResolver,
            cervicalCancerScreeningMethodOptions: CervicalCancerScreeningMethodResolver,
            familyPlanningMethodOptions: FamilyPlanningMethodResolver,
            cervicalCancerScreeningResultsOptions: CervicalCancerScreeningResultsResolver
        }
    },
    {
        path: 'maternity/:patientId/:personId/:serviceAreaId',
        component: MaternityComponent,
        resolve: {
            deliveryModeOptions: DeliveryModeResolver,
            bloodLossOptions: BloodLossResolver,
            motherStateOptions: MotherStateResolver,
            yesNoOptions: YesNoResolver,
            genderOptions: GenderResolver,
            deliveryOutcomeOptions: DeliveryModeResolver,
            yesNoNaOptions: YesNoNaResolver,
            referralOptions: ReferralResolver,
            hivFinalResultOptions: HivFinalResultsResolver,
            hivTestOptions: PmtctTestTypeResolver,
            kitNameOptions: TestKitNameResolver,
            hivTestResultOptions: HivTestResultResolver
        },
        pathMatch: 'full'
    }
];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PmtctRoutingModule { }
