import {Component, OnInit} from '@angular/core';
import {FormArray, FormGroup} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {DefaultParameters} from '../_models/hei/DefaultParameters';
import {BloodLossResolver} from '../_services/resolvers/blood-loss.resolver';
import {HivTestResultResolver} from '../_services/resolvers/hiv-test-result.resolver';
import {YesNoNaResolver} from '../_services/resolvers/yes-no-na.resolver';
import {MotherStateResolver} from '../_services/motherstate.resolver';
import {GenderResolver} from '../_services/resolvers/gender.resolver';
import {HivFinalResultsResolver} from '../_services/resolvers/hiv-final-results.resolver';
import {DeliveryModeResolver} from '../_services/deliverymode.resolver';
import {TestKitNameResolver} from '../_services/resolvers/test-kit-name.resolver';
import {PmtctTestTypeResolver} from '../_services/resolvers/pmtctTestType.resolver';
import {ReferralResolver} from '../_services/resolvers/referral.resolver';
import {LookupItemView} from '../../shared/_models/LookupItemView';

@Component({
    selector: 'app-maternity',
    templateUrl: './maternity.component.html',
    styleUrls: ['./maternity.component.css']
})
export class MaternityComponent implements OnInit {
    isLinear: boolean = false;
    visitDetailsFormGroup: FormArray;
    motherProfileForm: FormGroup;
    formType: string;
    diagnosisFormGroup: FormArray;
    deliveryFormGroup: FormArray;
    babyFormGroup: FormArray;
    maternityTestsFormGroup: FormArray;
    maternalDrugAdministrationForGroup: FormArray;
    motherProfileFormGroup: FormGroup;
    PartnerTestingForm: FormArray;
    patientEducationForm: FormArray;
    dischargeFormGroup: FormArray;
    referralForm: FormGroup;
    nextAppointmentFormGroup: FormGroup;

    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    patientEncounterId: number;
    visitDate: Date;
    visitType: number;


    deliveryModeOptions: LookupItemView[] = [];
    bloodLossOptions: LookupItemView[] = [];
    motherStateOptions: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    genderOptions: LookupItemView[] = [];
    deliveryOutcomeOptions: LookupItemView[] = [];
    yesNoNaOptions: LookupItemView[] = [];
    referralOptions: LookupItemView[] = [];
    hivFinalResultOptions: LookupItemView[] = [];
    hivTestOptions: LookupItemView[] = [];
    kitNameOptions: LookupItemView[] = [];
    hivTestResultOptions: LookupItemView[] = [];

    diagnosisOptions: any[] = [];
    babySectionOptions: any[] = [];
    maternityTestOptions: any[] = [];
    drugAdministrationOptions: any[] = [];
    dischargeOptions: any[] = [];
    partnerTestingOptions: any[] = [];
    patientEducationOptions: any[] = [];

    constructor(private route: ActivatedRoute) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.maternalDrugAdministrationForGroup = new FormArray([]);
        this.dischargeFormGroup = new FormArray([]);
        this.babyFormGroup = new FormArray([]);
        this.maternityTestsFormGroup = new FormArray([]);
        this.formType = 'maternity';
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const {patientId, personId, serviceAreaId} = params;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceAreaId = serviceAreaId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
        this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));

        this.route.data.subscribe((res) => {
            const {
                deliveryModeOptions,
                bloodLossOptions,
                motherStateOptions,
                yesNoOptions,
                genderOptions,
                deliveryOutcomeOptions,
                yesNoNaOptions,
                referralOptions,
                hivFinalResultOptions,
                hivTestOptions,
                kitNameOptions,
                hivTestResultOptions
            } = res;
            console.log('test options');
            console.log(res);
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.bloodLossOptions = bloodLossOptions['lookupItems'];
            this.motherStateOptions = motherStateOptions['lookupItems'];
            this.yesNoNaOptions = yesNoNaOptions['lookupItems'];
            this.yesNoOptions = yesNoOptions['lookupItems'];
            this.genderOptions = genderOptions['lookupItems'];
            this.deliveryOutcomeOptions = deliveryOutcomeOptions['lookupItems'];
            this.referralOptions = referralOptions['lookupItems'];
            this.hivFinalResultOptions = hivFinalResultOptions['lookupItems'];
            this.hivTestOptions = hivTestOptions['lookupItems'];
            this.kitNameOptions = kitNameOptions['lookupItems'];
            this.hivTestResultOptions = hivTestResultOptions['LookupItems'];
            this.partnerTestingOptions = this.yesNoOptions;
        });

        this.diagnosisOptions.push({
            'deliveryModes': this.deliveryModeOptions,
            'bloodLoss': this.bloodLossOptions,
            'motherStates': this.motherStateOptions,
            'yesNos': this.yesNoOptions,
        });

        this.babySectionOptions.push({
            'gender': this.genderOptions,
            'deliveryOutcomes': this.deliveryOutcomeOptions,
            'yesNos': this.yesNoOptions
        });

        this.maternityTestOptions.push({
            'yesNos': this.yesNoOptions
        });

        this.drugAdministrationOptions.push({
            'yesNo' : this.yesNoOptions,
            'finalResult' : this.hivFinalResultOptions,
            'yesNoNa': this.yesNoNaOptions
        });

        this.dischargeOptions.push({
           'deliveryStates': this.motherStateOptions,
           'referrals': this.referralOptions,
           'yesNos': this.yesNoOptions
        });

        this.partnerTestingOptions.push({
            'yesNoNaOptions': this.yesNoNaOptions,
            'finalPartnerHivResultOptions': this.hivFinalResultOptions
        });

        this.maternityTestOptions.push({
            'yesNoOptions': this.yesNoOptions
        });
        this.patientEducationOptions.push({
            'yesNoNaOptions': this.yesNoOptions
        });


    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    OnMotherProfileNotify(formGroup: FormGroup): void {
        this.motherProfileForm = formGroup;
    }

    onPatientDiagnosis(formGroup: FormGroup): void {
        this.diagnosisFormGroup.push(formGroup);
    }

    onPatientDeliveryNotify(formGroup: FormGroup) {
        this.deliveryFormGroup.push(formGroup);
    }

    onBabyNotify(formGroup: FormGroup): void {
        this.babyFormGroup.push(formGroup);
    }

    onMaternityTests(formGroup: FormGroup): void {
        this.maternityTestsFormGroup.push(formGroup);
    }

    onMaternalDrugAdministration(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPartnerTestingNotify(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPatientDischarge(formGroup: FormGroup): void {
        this.dischargeFormGroup.push(formGroup);
    }

    onPatientEducationNotify(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPatientreferralNotify(formGroup: FormGroup): void {
        this.referralForm = formGroup;
    }

    onPatientNextAppointent(formGroup: FormGroup): void {
        this.nextAppointmentFormGroup = formGroup;
    }

}
