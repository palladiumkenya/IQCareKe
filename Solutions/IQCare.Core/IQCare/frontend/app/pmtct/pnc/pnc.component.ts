import { LookupItemView } from './../../shared/_models/LookupItemView';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, NgZone } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-pnc',
    templateUrl: './pnc.component.html',
    styleUrls: ['./pnc.component.css']
})
export class PncComponent implements OnInit {
    isLinear: boolean = true;
    formType: string;

    yesnoOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];
    deliveryModeOptions: LookupItemView[] = [];
    breastOptions: LookupItemView[] = [];
    uterusOptions: LookupItemView[] = [];
    lochiaOptions: LookupItemView[] = [];
    postpartumhaemorrhageOptions: LookupItemView[] = [];
    episiotomyOptions: LookupItemView[] = [];
    cSectionSiteOptions: LookupItemView[] = [];
    fistulaScreeningOptions: LookupItemView[] = [];
    babyConditionOptions: LookupItemView[] = [];
    yesNoNaOptions: LookupItemView[] = [];
    infantPncDrugOptions: LookupItemView[] = [];
    infantDrugsStartContinueOptions: LookupItemView[] = [];
    finalPartnerHivResultOptions: LookupItemView[] = [];
    cervicalCancerScreeningMethodOptions: LookupItemView[] = [];
    familyPlanningMethodOptions: LookupItemView[] = [];
    cervicalCancerScreeningResultsOptions: LookupItemView[] = [];

    pncHivOptions: any[] = [];
    matHistoryOptions: any[] = [];
    postNatalExamOptions: any[] = [];
    babyExaminationOptions: any[] = [];
    patientEducationOptions: any[] = [];
    drugAdministrationOptions: any[] = [];
    partnerTestingOptions: any[] = [];
    cervicalCancerScreeningOptions: any[] = [];
    contraceptiveHistoryExercise: any[] = [];


    visitDetailsFormGroup: FormArray;
    matHistory_PostNatalExam_FormGroup: FormArray;
    drugAdministration_PartnerTesting_FormGroup: FormArray;
    hivStatusFormGroup: FormArray;
    cervicalCancerScreeningFormGroup: FormArray;
    diagnosisReferralAppointmentFormGroup: FormArray;

    constructor(private route: ActivatedRoute,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public zone: NgZone,
        private router: Router) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.matHistory_PostNatalExam_FormGroup = new FormArray([]);
        this.drugAdministration_PartnerTesting_FormGroup = new FormArray([]);
        this.hivStatusFormGroup = new FormArray([]);
        this.cervicalCancerScreeningFormGroup = new FormArray([]);
        this.diagnosisReferralAppointmentFormGroup = new FormArray([]);

        this.formType = 'pnc';
    }

    ngOnInit() {
        this.route.data.subscribe((res) => {
            const {
                yesnoOptions,
                hivFinalResultsOptions,
                deliveryModeOptions,
                breastOptions,
                uterusOptions,
                lochiaOptions,
                postpartumhaemorrhageOptions,
                episiotomyOptions,
                cSectionSiteOptions,
                fistulaScreeningOptions,
                babyConditionOptions,
                yesNoNaOptions,
                infantPncDrugOptions,
                infantDrugsStartContinueOptions,
                finalPartnerHivResultOptions,
                cervicalCancerScreeningMethodOptions,
                familyPlanningMethodOptions,
                cervicalCancerScreeningResultsOptions } = res;
            this.yesnoOptions = yesnoOptions['lookupItems'];
            this.hivFinalResultsOptions = hivFinalResultsOptions['lookupItems'];
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.breastOptions = breastOptions['lookupItems'];
            this.uterusOptions = uterusOptions['lookupItems'];
            this.lochiaOptions = lochiaOptions['lookupItems'];
            this.postpartumhaemorrhageOptions = postpartumhaemorrhageOptions['lookupItems'];
            this.episiotomyOptions = episiotomyOptions['lookupItems'];
            this.cSectionSiteOptions = cSectionSiteOptions['lookupItems'];
            this.fistulaScreeningOptions = fistulaScreeningOptions['lookupItems'];
            this.babyConditionOptions = babyConditionOptions['lookupItems'];
            this.yesNoNaOptions = yesNoNaOptions['lookupItems'];
            this.infantPncDrugOptions = infantPncDrugOptions['lookupItems'];
            this.infantDrugsStartContinueOptions = infantDrugsStartContinueOptions['lookupItems'];
            this.finalPartnerHivResultOptions = finalPartnerHivResultOptions['lookupItems'];
            this.cervicalCancerScreeningMethodOptions = cervicalCancerScreeningMethodOptions['lookupItems'];
            this.familyPlanningMethodOptions = familyPlanningMethodOptions['lookupItems'];
            this.cervicalCancerScreeningResultsOptions = cervicalCancerScreeningResultsOptions['lookupItems'];
        });

        this.pncHivOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'hivFinalResultsOptions': this.hivFinalResultsOptions
        });

        this.matHistoryOptions.push({
            'deliveryModeOptions': this.deliveryModeOptions
        });

        this.postNatalExamOptions.push({
            'breastOptions': this.breastOptions,
            'uterusOptions': this.uterusOptions,
            'lochiaOptions': this.lochiaOptions,
            'postpartumhaemorrhageOptions': this.postpartumhaemorrhageOptions,
            'episiotomyOptions': this.episiotomyOptions,
            'cSectionSiteOptions': this.cSectionSiteOptions,
            'fistulaScreeningOptions': this.fistulaScreeningOptions
        });

        this.babyExaminationOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'babyConditionOptions': this.babyConditionOptions
        });

        this.patientEducationOptions.push({
            'yesnoOptions': this.yesnoOptions
        });

        this.drugAdministrationOptions.push({
            'yesNoNaOptions': this.yesNoNaOptions,
            'yesnoOptions': this.yesnoOptions,
            'infantPncDrugOptions': this.infantPncDrugOptions,
            'infantDrugsStartContinueOptions': this.infantDrugsStartContinueOptions
        });

        this.partnerTestingOptions.push({
            'yesNoNaOptions': this.yesNoNaOptions,
            'finalPartnerHivResultOptions': this.finalPartnerHivResultOptions
        });

        this.cervicalCancerScreeningOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'cervicalCancerScreeningMethodOptions': this.cervicalCancerScreeningMethodOptions,
            'cervicalCancerScreeningResultsOptions': this.cervicalCancerScreeningResultsOptions
        });

        this.contraceptiveHistoryExercise.push({
            'yesnoOptions': this.yesnoOptions,
            'familyPlanningMethodOptions': this.familyPlanningMethodOptions
        });
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    onMaternalHistoryNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onPostNatalExamNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onBabyExaminationNotify(formGroup: FormGroup): void {
        this.matHistory_PostNatalExam_FormGroup.push(formGroup);
    }

    onDrugAdministrationNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onPartnerTestingNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onPatientEducationNotify(formGroup: FormGroup): void {
        this.drugAdministration_PartnerTesting_FormGroup.push(formGroup);
    }

    onCervicalCancerScreeningNotify(formGroup: FormGroup): void {
        this.cervicalCancerScreeningFormGroup.push(formGroup);
    }

    onContraceptiveHistoryNotify(formGroup: FormGroup): void {
        this.cervicalCancerScreeningFormGroup.push(formGroup);
    }

    onHivStatusNotify(formGroup: FormGroup): void {
        this.hivStatusFormGroup.push(formGroup);
    }

    onDiagnosisNotify(formGroup: FormGroup): void {
        this.diagnosisReferralAppointmentFormGroup.push(formGroup);
    }

    onReferralNotify(formGroup: FormGroup): void {
        this.diagnosisReferralAppointmentFormGroup.push(formGroup);
    }

    onNextAppointmentNotify(formGroup: FormGroup): void {
        this.diagnosisReferralAppointmentFormGroup.push(formGroup);
    }

    onSubmitForm() {
        this.snotifyService.success('Success', 'PNC Encounter', this.notificationService.getConfig());

        /*this.zone.run(() => {
            this.router.navigate(['/dashboard/personhome/'], { relativeTo: this.route });
        });*/
    }
}
