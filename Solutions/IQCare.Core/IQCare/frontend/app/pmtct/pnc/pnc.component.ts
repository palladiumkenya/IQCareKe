import { PatientReferralCommand } from './../_models/PatientReferralCommand';
import { PatientDiagnosisCommand } from './../_models/PatientDiagnosisCommand';
import { HivTestsCommand } from './../_models/HivTestsCommand';
import { LookupItemService } from './../../shared/_services/lookup-item.service';
import { HivStatusCommand } from './../_models/HivStatusCommand';
import { PncVisitDetailsCommand } from './../_models/PncVisitDetailsCommand';
import { PncService } from './../_services/pnc.service';
import { LookupItemView } from './../../shared/_models/LookupItemView';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, NgZone } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { forkJoin } from 'rxjs';

@Component({
    selector: 'app-pnc',
    templateUrl: './pnc.component.html',
    styleUrls: ['./pnc.component.css']
})
export class PncComponent implements OnInit {
    patientId: number;
    personId: number;
    serviceAreaId: number;
    userId: number;
    patientMasterVisitId: number;
    patientEncounterId: number;
    visitDate: Date;
    visitType: number;

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
    referralFromOptions: LookupItemView[] = [];

    pncHivOptions: any[] = [];
    matHistoryOptions: any[] = [];
    postNatalExamOptions: any[] = [];
    babyExaminationOptions: any[] = [];
    patientEducationOptions: any[] = [];
    drugAdministrationOptions: any[] = [];
    partnerTestingOptions: any[] = [];
    cervicalCancerScreeningOptions: any[] = [];
    contraceptiveHistoryExercise: any[] = [];
    referralOptions: any[] = [];

    hiv_status_table_data: any[] = [];
    hivTestEntryPoint: number;
    htsEncounterId: number;

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
        private router: Router,
        private pncService: PncService,
        private lookupitemservice: LookupItemService) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.matHistory_PostNatalExam_FormGroup = new FormArray([]);
        this.drugAdministration_PartnerTesting_FormGroup = new FormArray([]);
        this.hivStatusFormGroup = new FormArray([]);
        this.cervicalCancerScreeningFormGroup = new FormArray([]);
        this.diagnosisReferralAppointmentFormGroup = new FormArray([]);

        this.formType = 'pnc';
    }

    ngOnInit() {
        this.route.params.subscribe(
            params => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                this.serviceAreaId = params.serviceAreaId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
        this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));

        this.lookupitemservice.getByGroupNameAndItemName('HTSEntryPoints', 'PMTCT').subscribe(
            (res) => {
                this.hivTestEntryPoint = res['itemId'];
            }
        );

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
                cervicalCancerScreeningResultsOptions,
                referralFromOptions } = res;
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
            this.referralFromOptions = referralFromOptions['lookupItems'];
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

        this.referralOptions.push({
            referrals: this.referralFromOptions
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

    onHivStatusNotify(formGroup: Object): void {
        this.hivStatusFormGroup.push(formGroup['form']);
        this.hiv_status_table_data.push(formGroup['table_data']);
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
        // this.snotifyService.success('Success', 'PNC Encounter', this.notificationService.getConfig());
        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
        const naOption = this.yesNoNaOptions.filter(obj => obj.itemName == 'N/A');

        const pncVisitDetailsCommand: PncVisitDetailsCommand = {
            PatientId: this.patientId,
            ServiceAreaId: this.serviceAreaId,
            VisitDate: this.visitDetailsFormGroup.value[0]['visitDate'],
            VisitNumber: this.visitDetailsFormGroup.value[0]['visitNumber'],
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            UserId: this.userId,
            DaysPostPartum: this.visitDetailsFormGroup.value[0]['dayPostPartum'],
            PatientMasterVisitId: this.patientMasterVisitId
        };

        const hivStatusCommand: HivStatusCommand = {
            PersonId: this.personId,
            ProviderId: this.userId,
            PatientEncounterID: this.patientEncounterId,
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            EverTested: null,
            MonthsSinceLastTest: null,
            MonthSinceSelfTest: null,
            TestedAs: null,
            TestingStrategy: null,
            EncounterRemarks: '',
            TestEntryPoint: this.hivTestEntryPoint,
            Consent: this.hiv_status_table_data.length > 0 ? yesOption[0].itemId : noOption[0].itemId,
            EverSelfTested: null,
            GeoLocation: null,
            HasDisability: null,
            Disabilities: [],
            TbScreening: null,
            ServiceAreaId: this.serviceAreaId,
            EncounterTypeId: 1,
            EncounterDate: this.visitDetailsFormGroup.value[0]['visitDate'],
            EncounterType: this.hivStatusFormGroup.value[0]['testType']
        };

        const hivTestsCommand: HivTestsCommand = {
            HtsEncounterId: 0,
            ProviderId: this.userId,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            Testing: [],
            FinalTestingResult: {
                FinalResultHiv1: this.hivStatusFormGroup.value[0]['finalTestResult'],
                FinalResultHiv2: null,
                FinalResult: this.hivStatusFormGroup.value[0]['finalTestResult'],
                FinalResultGiven: yesOption[0].itemId,
                CoupleDiscordant: naOption[0].itemId,
                FinalResultsRemarks: 'n/a',
                AcceptedPartnerListing: yesOption[0].itemId,
                ReasonsDeclinePartnerListing: null
            }
        };

        for (let i = 0; i < this.hiv_status_table_data.length; i++) {
            for (let j = 0; j < this.hiv_status_table_data[i].length; j++) {
                hivTestsCommand.Testing.push({
                    KitId: this.hiv_status_table_data[i][j]['kitname']['itemId'],
                    KitLotNumber: this.hiv_status_table_data[i][j]['lotnumber'],
                    ExpiryDate: this.hiv_status_table_data[i][j]['expirydate'],
                    Outcome: this.hiv_status_table_data[i][j]['testresult']['itemId'],
                    TestRound: this.hiv_status_table_data[i][j]['testtype']['itemName'] == 'HIV Test-1' ? 1 : 2,
                });
            }
        }

        const pncPatientDiagnosis: PatientDiagnosisCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Diagnosis: this.diagnosisReferralAppointmentFormGroup[0]['diagnosis'],
            ManagementPlan: '',
            CreatedBy: this.userId
        };

        const pncReferralCommand: PatientReferralCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.diagnosisReferralAppointmentFormGroup[1]['referredFrom'],
            ReferredTo: this.diagnosisReferralAppointmentFormGroup[1]['referredTo'],
            ReferralReason: null,
            ReferralDate: null,
            ReferredBy: this.userId,
            DeleteFlag: 0,
            CreatedBy: this.userId
        };

        const pncVisitDetails = this.pncService.savePncVisitDetails(pncVisitDetailsCommand);
        const pncPostNatalExam = this.pncService.savePncPostNatalExam();
        const pncHivStatus = this.pncService.savePncHivStatus(hivStatusCommand, this.hiv_status_table_data);
        const pncDiagnosis = this.pncService.saveDiagnosis(pncPatientDiagnosis);
        const pncReferral = this.pncService.savePncReferral(pncReferralCommand);

        forkJoin([pncHivStatus, pncDiagnosis, pncReferral])
            .subscribe(
                (result) => {
                    console.log(`success `);
                    console.log(result);
                    // {"htsEncounterId":4,"patientMasterVisitId":21410}
                    this.htsEncounterId = result[0]['htsEncounterId'];

                    hivTestsCommand.HtsEncounterId = this.htsEncounterId;
                    const pncHivTests = this.pncService.savePncHivTests(hivTestsCommand).subscribe(
                        (res) => {
                            console.log(`result`, res);
                        }
                    );
                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {
                    console.log(`complete`);
                }
            );

        /*this.zone.run(() => {
            this.router.navigate(['/dashboard/personhome/'], { relativeTo: this.route });
        });*/
    }
}
