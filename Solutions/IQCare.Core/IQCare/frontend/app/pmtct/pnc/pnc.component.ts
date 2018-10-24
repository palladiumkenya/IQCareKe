import { LookupItemView } from './../../shared/_models/LookupItemView';
import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';

@Component({
    selector: 'app-pnc',
    templateUrl: './pnc.component.html',
    styleUrls: ['./pnc.component.css']
})
export class PncComponent implements OnInit {
    isLinear: boolean = true;

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


    matHistory_PostNatalExam_FormGroup: FormArray;
    drugAdministration_PartnerTesting_FormGroup: FormArray;
    hivStatusFormGroup: FormArray;

    constructor(private route: ActivatedRoute) {
        this.matHistory_PostNatalExam_FormGroup = new FormArray([]);
        this.drugAdministration_PartnerTesting_FormGroup = new FormArray([]);
        this.hivStatusFormGroup = new FormArray([]);
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

    }

    onContraceptiveHistoryNotify(formGroup: FormGroup): void {

    }

    onHivStatusNotify(formGroup: FormGroup): void {
        this.hivStatusFormGroup.push(formGroup);
    }
}
