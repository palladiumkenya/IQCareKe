import { FamilyPlanningCommand } from './../_models/FamilyPlanningCommand';
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
import { PatientAppointment } from '../_models/PatientAppointmet';
import { PostNatalExamCommand } from '../_models/PostNatalExamCommand';
import { FamilyPlanningMethodCommand } from '../_models/FamilyPlanningMethodCommand';
import { DrugAdministrationCommand } from '../maternity/commands/drug-administration-command';
import { PartnerTestingCommand } from '../_models/PartnerTestingCommand';
import { MaternityCounsellingCommand } from '../maternity/commands/maternity-counselling-command';
import { MaternityService } from '../_services/maternity.service';
import { PatientScreeningCommand } from '../_models/PatientScreeningCommand';
import { VisitDetailsCommand } from '../_models/visit-details-command';
import * as moment from 'moment';
import { VisitDetailsEditCommand } from '../_models/VisitDetailsEditCommand';
import { PatientReferralEditCommand } from '../_models/PatientReferralEditCommand';
import { PatientAppointmentEditCommand } from '../_models/PatientAppointmentEditCommand';

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
    isEdit: boolean = false;

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
    motherExaminationOptions: LookupItemView[] = [];
    babyExaminationControls: LookupItemView[] = [];
    drugAdministrationCategories: LookupItemView[] = [];

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
    infantFeedingTopicId: number;

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
        private lookupitemservice: LookupItemService,
        private maternityService: MaternityService) {
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
                this.patientMasterVisitId = params.patientMasterVisitId;
                this.patientEncounterId = params.patientEncounterId;

                if (!this.patientMasterVisitId) {
                    this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
                    this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
                } else {
                    this.isEdit = true;
                }
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));

        this.lookupitemservice.getByGroupNameAndItemName('HTSEntryPoints', 'PMTCT').subscribe(
            (res) => {
                this.hivTestEntryPoint = res['itemId'];
            }
        );

        this.lookupitemservice.getByGroupName('PncDrugAdministration').subscribe(
            (res) => {
                this.drugAdministrationCategories = res['lookupItems'];
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
                referralFromOptions,
                motherExaminationOptions,
                babyExaminationControls,
                counselledInfantFeedingOptions } = res;
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
            this.motherExaminationOptions = motherExaminationOptions['lookupItems'];
            this.babyExaminationControls = babyExaminationControls['lookupItems'];
            this.infantFeedingTopicId = counselledInfantFeedingOptions['itemId'];
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
            'fistulaScreeningOptions': this.fistulaScreeningOptions,
            'motherExaminationOptions': this.motherExaminationOptions
        });

        this.babyExaminationOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'babyConditionOptions': this.babyConditionOptions,
            'babyExaminationControls': this.babyExaminationControls
        });

        this.patientEducationOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'infantFeedingTopicId': this.infantFeedingTopicId
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
        this.hiv_status_table_data = formGroup['table_data'];
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
        if (!this.diagnosisReferralAppointmentFormGroup.valid) {
            this.snotifyService.error('Complete the highlighted fields before submitting', 'PNC Encounter',
                this.notificationService.getConfig());
            return;
        }

        if (this.isEdit) {
            this.submitOnEdit();
        } else {
            this.submitOnAddNew();
        }
    }

    submitOnAddNew(): void {
        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
        const naOption = this.yesNoNaOptions.filter(obj => obj.itemName == 'N/A');
        const isCounsellingDone = this.yesnoOptions.filter(obj =>
            obj.itemId == this.drugAdministration_PartnerTesting_FormGroup.value[2]['counselledInfantFeeding']);

        // const motherExaminationTypeId = this.motherExaminationOptions.filter(obj => obj.masterName == 'MotherExamination');

        /* const pncVisitDetailsCommand: PncVisitDetailsCommand = {
             PatientId: this.patientId,
             ServiceAreaId: this.serviceAreaId,
             VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
             VisitNumber: this.visitDetailsFormGroup.value[0]['visitNumber'],
             VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
             UserId: this.userId,
             DaysPostPartum: this.visitDetailsFormGroup.value[0]['dayPostPartum'],
             PatientMasterVisitId: this.patientMasterVisitId
         };*/

        const visitDetailsCommand = {
            PatientId: parseInt(this.patientId.toString(), 10),
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            PregnancyId: 0,
            PatientMasterVisitId: this.patientMasterVisitId,
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            DaysPostPartum: this.visitDetailsFormGroup.value[0]['dayPostPartum'],
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            UserId: this.userId
        } as VisitDetailsCommand;


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
            EncounterDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
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
                    ExpiryDate: moment(this.hiv_status_table_data[i][j]['expirydate']).toDate(),
                    Outcome: this.hiv_status_table_data[i][j]['testresult']['itemId'],
                    TestRound: this.hiv_status_table_data[i][j]['testtype']['itemName'] == 'HIV Test-1' ? 1 : 2,
                });
            }
        }

        const pncPatientDiagnosis: PatientDiagnosisCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Diagnosis: this.diagnosisReferralAppointmentFormGroup.value[0]['diagnosis'],
            ManagementPlan: '',
            CreatedBy: this.userId
        };

        const pncReferralCommand: PatientReferralCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.diagnosisReferralAppointmentFormGroup.value[1]['referredFrom'],
            ReferredTo: this.diagnosisReferralAppointmentFormGroup.value[1]['referredTo'],
            ReferralReason: 'Referral',
            ReferralDate: new Date(),
            ReferredBy: this.userId,
            DeleteFlag: 0,
            CreatedBy: this.userId
        };

        const pncNextAppointmentCommand: PatientAppointment = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            AppointmentDate: moment(this.diagnosisReferralAppointmentFormGroup.value[2]['nextAppointmentDate']).toDate(),
            Description: this.diagnosisReferralAppointmentFormGroup.value[2]['remarks'],
            StatusDate: null,
            DifferentiatedCareId: 0,
            CreatedBy: this.userId,
            AppointmentReason: 'Follow Up'
        };

        const pncPostNatalExamCommand: PostNatalExamCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ExaminationTypeId: this.motherExaminationOptions[0]['masterId'],
            CreateBy: this.userId,
            DeleteFlag: false,
            PostNatalExamResults: []
        };

        for (let i = 0; i < this.motherExaminationOptions.length; i++) {
            pncPostNatalExamCommand.PostNatalExamResults.push({
                ExamId: this.motherExaminationOptions[i].itemId,
                FindingId: this.matHistory_PostNatalExam_FormGroup.value[1][this.motherExaminationOptions[i].itemName.toLowerCase()],
                FindingsNotes: ''
            });
        }

        const pncBabyExaminationCommand: PostNatalExamCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ExaminationTypeId: this.babyExaminationControls[0]['masterId'],
            CreateBy: this.userId,
            DeleteFlag: false,
            PostNatalExamResults: []
        };

        for (let i = 0; i < this.babyExaminationControls.length; i++) {
            pncBabyExaminationCommand.PostNatalExamResults.push({
                ExamId: this.babyExaminationControls[i].itemId,
                FindingId: this.matHistory_PostNatalExam_FormGroup.value[2][this.babyExaminationControls[i].itemName.toLowerCase()],
                FindingsNotes: ''
            });
        }

        const familyPlanningCommand: FamilyPlanningCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            FamilyPlanningStatusId: this.cervicalCancerScreeningFormGroup.value[1]['onFamilyPlanning'],
            ReasonNotOnFPId: 0,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            VisitDate: new Date(),
            AuditData: ''
        };

        const familyPlanningMethodCommand: FamilyPlanningMethodCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientFPId: 0,
            FPMethodId: this.cervicalCancerScreeningFormGroup.value[1]['familyPlanningMethod'],
            Active: true,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: ''
        };

        const drugAdministrationCommand: DrugAdministrationCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreatedBy: this.userId,
            AdministeredDrugs: []
        };

        for (let i = 0; i < this.drugAdministrationCategories.length; i++) {
            let value;
            if (this.drugAdministrationCategories[i].itemName == 'Started HAART in PNC') {
                value = this.drugAdministration_PartnerTesting_FormGroup.value[0]['startedARTPncVisit'];
            } else if (this.drugAdministrationCategories[i].itemName == 'Haematinics given') {
                value = this.drugAdministration_PartnerTesting_FormGroup.value[0]['haematinics_given'];
            } else if (this.drugAdministrationCategories[i].itemName == 'Infant_Drug') {
                value = this.drugAdministration_PartnerTesting_FormGroup.value[0]['infant_drug'];
            } else if (this.drugAdministrationCategories[i].itemName == 'Infant_Start_Continue') {
                value = this.drugAdministration_PartnerTesting_FormGroup.value[0]['infant_start'];
            }

            drugAdministrationCommand.AdministeredDrugs.push({
                Id: this.drugAdministrationCategories[i].itemId,
                Value: value,
                Description: this.drugAdministrationCategories[i].itemName
            });
        }

        const partnerTestingCommand: PartnerTestingCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            PartnerTested: this.drugAdministration_PartnerTesting_FormGroup.value[1]['partnerHivTestDone'],
            PartnerHIVResult: this.drugAdministration_PartnerTesting_FormGroup.value[1]['finalPartnerHivResult'],
            CreateDate: new Date(),
            CreatedBy: this.userId,
            DeleteFlag: false,
            AuditData: ''
        };

        const patiendEducationCommand: MaternityCounsellingCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CounsellingTopicId: this.infantFeedingTopicId,
            IsCounsellingDone: isCounsellingDone[0].itemName == 'Yes' ? true : false,
            CounsellingDate: new Date(),
            Description: null,
            CreatedBy: this.userId
        };

        const isCacxDone = this.yesnoOptions.filter(obj =>
            obj.itemId == this.cervicalCancerScreeningFormGroup.value[0]['cervicalcancerscreening']);

        let screeningTypeId;
        if (this.cervicalCancerScreeningFormGroup.value[0]['method']) {
            const screeningMethod = this.cervicalCancerScreeningMethodOptions.filter(obj =>
                obj.itemId == this.cervicalCancerScreeningFormGroup.value[0]['method']);
            screeningTypeId = screeningMethod[0]['masterId'];
        } else {
            screeningTypeId = 0;
        }

        const patientScreeningCommand: PatientScreeningCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ScreeningTypeId: screeningTypeId,
            ScreeningDone: isCacxDone[0].itemName == 'Yes' ? true : false,
            ScreeningDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            ScreeningCategoryId: this.cervicalCancerScreeningFormGroup.value[0]['method'],
            ScreeningValueId: this.cervicalCancerScreeningFormGroup.value[0]['results'],
            Comment: '',
            Active: true,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: '',
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate()
        };

        const pncVisitDetails = this.pncService.savePncVisitDetails(visitDetailsCommand);
        const pncPostNatalExam = this.pncService.savePncPostNatalExam(pncPostNatalExamCommand);
        const pncBabyExam = this.pncService.savePncPostNatalExam(pncBabyExaminationCommand);
        const pncHivStatus = this.pncService.savePncHivStatus(hivStatusCommand, this.hiv_status_table_data);
        const pncDiagnosis = this.pncService.saveDiagnosis(pncPatientDiagnosis);
        const pncReferral = this.pncService.savePncReferral(pncReferralCommand);
        const pncNextAppointment = this.pncService.savePncNextAppointment(pncNextAppointmentCommand);
        const pncFamilyPlanning = this.pncService.savePncFamilyPlanning(familyPlanningCommand);
        const pncDrugAdministration = this.pncService.savePncDrugAdministration(drugAdministrationCommand);
        const pncPartnerTesting = this.pncService.savePartnerTesting(partnerTestingCommand);
        const pncPatientEducation = this.maternityService.savePatientEducation(patiendEducationCommand);
        const pncPatientScreening = this.maternityService.saveScreening(patientScreeningCommand);

        forkJoin([
            pncHivStatus, pncDiagnosis, pncReferral,
            pncNextAppointment, pncVisitDetails,
            pncPostNatalExam, pncBabyExam, pncFamilyPlanning,
            pncPartnerTesting, pncPatientEducation, pncDrugAdministration,
            pncPatientScreening])
            .subscribe(
                (result) => {
                    console.log(result);
                    this.htsEncounterId = result[0]['htsEncounterId'];

                    hivTestsCommand.HtsEncounterId = this.htsEncounterId;
                    const pncHivTests = this.pncService.savePncHivTests(hivTestsCommand).subscribe(
                        (res) => {
                            console.log(`result`, res);
                        }
                    );

                    familyPlanningMethodCommand.PatientFPId = result[7]['patientId'];
                    const pncFamilyPlanningMethod = this.pncService.savePncFamilyPlanningMethod(familyPlanningMethodCommand).subscribe(
                        (res) => {
                            console.log(`family planning method`);
                            console.log(res);
                        }
                    );

                    this.snotifyService.success('Successfully saved PNC encounter ', 'PNC', this.notificationService.getConfig());
                    this.zone.run(() => {
                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                        });
                    });

                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {
                    console.log(`complete`);
                }
            );
    }

    submitOnEdit(): void {
        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');

        const visitDetailsEditCommand: VisitDetailsEditCommand = {
            Id: this.visitDetailsFormGroup.value[0]['id'],
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            DaysPostPartum: this.visitDetailsFormGroup.value[0]['dayPostPartum'],
        };

        const patientDiagnosisEdit = {
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            Diagnosis: this.diagnosisReferralAppointmentFormGroup.value[0]['diagnosis'],
            ManagementPlan: ''
        };

        const pncPostNatalExamCommand: PostNatalExamCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ExaminationTypeId: this.motherExaminationOptions[0]['masterId'],
            CreateBy: this.userId,
            DeleteFlag: false,
            PostNatalExamResults: []
        };

        for (let i = 0; i < this.motherExaminationOptions.length; i++) {
            pncPostNatalExamCommand.PostNatalExamResults.push({
                ExamId: this.motherExaminationOptions[i].itemId,
                FindingId: this.matHistory_PostNatalExam_FormGroup.value[1][this.motherExaminationOptions[i].itemName.toLowerCase()],
                FindingsNotes: ''
            });
        }

        const pncBabyExaminationCommand: PostNatalExamCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ExaminationTypeId: this.babyExaminationControls[0]['masterId'],
            CreateBy: this.userId,
            DeleteFlag: false,
            PostNatalExamResults: []
        };

        for (let i = 0; i < this.babyExaminationControls.length; i++) {
            pncBabyExaminationCommand.PostNatalExamResults.push({
                ExamId: this.babyExaminationControls[i].itemId,
                FindingId: this.matHistory_PostNatalExam_FormGroup.value[2][this.babyExaminationControls[i].itemName.toLowerCase()],
                FindingsNotes: ''
            });
        }

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
            EncounterDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            EncounterType: this.hivStatusFormGroup.value[0]['testType']
        };

        const patientReferralEditCommand: PatientReferralEditCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.diagnosisReferralAppointmentFormGroup.value[1]['referredFrom'],
            ReferredTo: this.diagnosisReferralAppointmentFormGroup.value[1]['referredTo'],
            ReferralReason: 'Referral',
            ReferralDate: new Date(),
            ReferredBy: this.userId,
            DeleteFlag: 0,
            CreateDate: new Date(),
            CreateBy: this.userId
        };

        const patientAppointmentEditCommand: PatientAppointmentEditCommand = {
            AppointmentId: this.diagnosisReferralAppointmentFormGroup.value[2]['id'],
            AppointmentDate: moment(this.diagnosisReferralAppointmentFormGroup.value[2]['nextAppointmentDate']).toDate(),
            Description: this.diagnosisReferralAppointmentFormGroup.value[2]['remarks']
        };

        const pncVisitDetailsEdit = this.pncService.editPncVisitDetails(visitDetailsEditCommand);
        const pncPatientDiagnosisEdit = this.pncService.updatePatientDiagnosis(patientDiagnosisEdit);
        const pncPostnatalexamEdit = this.pncService.updatePncPostNatalExam(pncPostNatalExamCommand);
        const pncbabyexamEdit = this.pncService.updatePncPostNatalExam(pncBabyExaminationCommand);
        const pncHivStatus = this.pncService.savePncHivStatus(hivStatusCommand, this.hiv_status_table_data);
        const pncReferralEdit = this.pncService.updateReferral(patientReferralEditCommand);
        const pncAppointmentEdit = this.pncService.updateAppointment(patientAppointmentEditCommand);

        forkJoin([pncVisitDetailsEdit, pncPatientDiagnosisEdit,
            pncPostnatalexamEdit, pncbabyexamEdit, pncHivStatus,
            pncReferralEdit, pncAppointmentEdit]).subscribe(
                (result) => {
                    console.log(result);

                    this.snotifyService.success('Successfully updated PNC encounter ', 'PNC', this.notificationService.getConfig());
                    this.zone.run(() => {
                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                        });
                    });
                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {

                }
            );
    }
}
