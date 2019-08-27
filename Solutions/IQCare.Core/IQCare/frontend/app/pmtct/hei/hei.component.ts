import { HeiOutComeCommand } from './../_models/hei/HeiOutcomeCommand';
import { PatientAppointment } from './../_models/PatientAppointmet';
import { PncService } from './../_services/pnc.service';
import { OrdVisitCommand } from './../_models/hei/OrdVisitCommand';
import { LabOrder } from './../_models/hei/LabOrder';
import { HeiService } from './../_services/hei.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { FormGroup, FormArray } from '@angular/forms';
import { Vaccination } from '../_models/hei/Vaccination';
import { Milestone } from '../_models/hei/Milestone';
import { PatientIcf } from '../_models/hei/PatientIcf';
import { PatientIcfAction } from '../_models/hei/PatientIcfAction';
import { DefaultParameters } from '../_models/hei/DefaultParameters';
import { forkJoin } from 'rxjs/index';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { CompleteLabOrderCommand } from '../_models/hei/CompleteLabOrderCommand';
import { PatientFeedingCommand } from '../_models/hei/PatientFeedingCommand';
import * as moment from 'moment';
import { HeiDeliveryEditCommand } from '../_models/HeiDeliveryEditCommand';
import { HeiFeedingEditCommand } from '../_models/HeiFeedingEditCommand';
import { PatientAppointmentEditCommand } from '../_models/PatientAppointmentEditCommand';
import { mergeMap, switchMap, concatMap } from 'rxjs/operators';
import { RegistrationService } from '../../registration/_services/registration.service';
import { FamilyPartnerControlsService } from '../../hts/_services/family-partner-controls.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LookupItemService } from '../../shared/_services/lookup-item.service';

@Component({
    selector: 'app-hei',
    templateUrl: './hei.component.html',
    styleUrls: ['./hei.component.css'],
    providers: [LookupItemService]
})

export class HeiComponent implements OnInit {
    patientId: number;
    personId: number;
    ptn_pk: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    formType: string;
    locationId: number;
    patientEncounterId: number;
    visitDate: Date;
    visitType: number;
    isEdit: boolean = false;
    appointmentStatusId: number;
    appointmentReasonId: number;

    defaultParameters: DefaultParameters;

    immunizationHistoryTableData: any[] = [];
    milestoneHistoryData: any[] = [];
    vaccination: Vaccination[] = [];
    milestone: Milestone[] = [];
    deliveryOptions: any[] = [];
    nextAppointmentOptions: any[] = [];
    maternalhistoryOptions: any[] = [];
    hivtestingOptions: any[] = [];
    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: LookupItemView[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];
    primarycaregiverOptions: any[] = [];
    immunizationHistoryOptions: any[] = [];
    milestoneOptions: any[] = [];
    tbAssessmentOptions: any[] = [];
    milestone_table_data: any[] = [];
    immunization_table_data: any[] = [];

    deliveryModeOptions: LookupItemView[] = [];
    arvprophylaxisOptions: LookupItemView[] = [];
    placeofdeliveryOptions: LookupItemView[] = [];
    motherstateOptions: LookupItemView[] = [];
    infantFeedingOptions: LookupItemView[] = [];
    immunizationPeriodOptions: LookupItemView[] = [];
    immunizationGivenOptions: LookupItemView[] = [];
    milestoneAssessedOptions: LookupItemView[] = [];
    milestoneStatusOptions: LookupItemView[] = [];
    heiOutcomeOptions: LookupItemView[] = [];
    heiHivTestingOptions: LookupItemView[] = [];
    heiHivTestingResultsOptions: LookupItemView[] = [];
    sputumSmearOptions: LookupItemView[] = [];
    geneXpertOptions: LookupItemView[] = [];
    chestXrayOptions: LookupItemView[] = [];
    tbScreeningOptions: LookupItemView[] = [];
    iptOutcomeOptions: LookupItemView[] = [];
    pcrLabTest: any;
    viralLoadLabTest: any;
    antibodyLabTest: any;

    pcrLabTestParameters: any[];
    viralLoadLabTestParameters: any[];
    antibodyLabTestParameters: any[];

    isLinear: boolean = true;
    deliveryMatFormGroup: FormArray;
    visitDetailsFormGroup: FormArray;
    tbAssessmentFormGroup: FormArray;
    milestonesFormGroup: FormArray;
    immunizationHistoryFormGroup: FormArray;
    infantFeedingFormGroup: FormArray;

    heiOutcomeFormGroup: FormArray;
    // nextAppointmentFormGroup: FormArray;
    hivTestingFormGroup: any[];
    motherRelationshipId: number;

    constructor(private route: ActivatedRoute,
        private heiService: HeiService,
        private pncService: PncService,
        private zone: NgZone,
        private router: Router,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private registrationService: RegistrationService,
        private service: FamilyPartnerControlsService,
        private spinner: NgxSpinnerService,
        private lookupitemservice: LookupItemService) {
        this.deliveryMatFormGroup = new FormArray([]);
        this.visitDetailsFormGroup = new FormArray([]);
        this.tbAssessmentFormGroup = new FormArray([]);

        this.immunizationHistoryFormGroup = new FormArray([]);
        this.milestonesFormGroup = new FormArray([]);
        this.infantFeedingFormGroup = new FormArray([]);

        this.heiOutcomeFormGroup = new FormArray([]);
        // this.nextAppointmentFormGroup = new FormArray([]);

        this.hivTestingFormGroup = [];
        this.formType = 'hei';
    }

    ngOnInit() {
        this.service.getRelationshipTypes().subscribe(
            (res) => {
                const motherOption = ['Mother'];
                const options = res['lookupItems'];
                for (let j = 0; j < options.length; j++) {
                    if (motherOption.includes(options[j].itemName)) {
                        this.motherRelationshipId = options[j].itemId;
                    }
                }
            }
        );

        this.route.params.subscribe(
            (params) => {
                const { patientId, personId, serviceAreaId } = params;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceAreaId = serviceAreaId;
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
        this.locationId = JSON.parse(localStorage.getItem('appLocationId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));

        this.lookupitemservice.getByGroupNameAndItemName('AppointmentStatus', 'Pending').subscribe(
            (res) => {
                this.appointmentStatusId = res['itemId'];
            }
        );

        this.lookupitemservice.getByGroupNameAndItemName('AppointmentReason', 'Follow Up').subscribe(
            (res) => {
                this.appointmentReasonId = res['itemId'];
            }
        );

        this.defaultParameters = {
            patientId: this.patientId,
            personId: this.personId,
            userId: this.userId,
            patientMasterVisitId: this.patientMasterVisitId
        } as DefaultParameters;

        this.route.data.subscribe((res) => {
            const {
                placeofdeliveryOptions,
                deliveryModeOptions,
                arvprophylaxisOptions,
                motherstateOptions,
                motherreceivedrugsOptions,
                heimotherregimenOptions,
                yesnoOptions,
                primarycaregiverOptions,
                motherdrugsatinfantenrollmentOptions,
                infantFeedingOptions,
                immunizationPeriodOptions,
                immunizationGivenOptions,
                milestoneAssessedOptions,
                milestoneStatusOptions,
                heiOutcomeOptions,
                sputumSmearOptions,
                geneXpertOptions,
                chestXrayOptions,
                tbScreeningOutComeOptions,
                heiHivTestingOptions,
                heiHivTestingResultsOptions,
                iptOutcomeOptions,
            } = res;
            this.placeofdeliveryOptions = placeofdeliveryOptions['lookupItems'];
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.arvprophylaxisOptions = arvprophylaxisOptions['lookupItems'];
            this.motherstateOptions = motherstateOptions['lookupItems'];
            this.motherreceivedrugsOptions = motherreceivedrugsOptions['lookupItems'];
            this.heimotherregimenOptions = heimotherregimenOptions['lookupItems'];
            this.yesnoOptions = yesnoOptions['lookupItems'];
            this.motherdrugsatinfantenrollmentOptions = motherdrugsatinfantenrollmentOptions['lookupItems'];
            this.primarycaregiverOptions = primarycaregiverOptions['lookupItems'];
            this.infantFeedingOptions = infantFeedingOptions['lookupItems'];
            this.immunizationPeriodOptions = immunizationPeriodOptions['lookupItems'];
            this.immunizationGivenOptions = immunizationGivenOptions['lookupItems'];
            this.milestoneAssessedOptions = milestoneAssessedOptions['lookupItems'];
            this.milestoneStatusOptions = milestoneStatusOptions['lookupItems'];
            this.heiOutcomeOptions = heiOutcomeOptions['lookupItems'];
            this.sputumSmearOptions = sputumSmearOptions['lookupItems'];
            this.geneXpertOptions = geneXpertOptions['lookupItems'];
            this.chestXrayOptions = chestXrayOptions['lookupItems'];
            this.tbScreeningOptions = tbScreeningOutComeOptions['lookupItems'];
            this.heiHivTestingOptions = heiHivTestingOptions['lookupItems'];
            this.heiHivTestingResultsOptions = heiHivTestingResultsOptions['lookupItems'];
            this.iptOutcomeOptions = iptOutcomeOptions['lookupItems'];
        });

        this.deliveryOptions.push({
            'placeofdeliveryOptions': this.placeofdeliveryOptions,
            'deliveryModeOptions': this.deliveryModeOptions,
            'arvprophylaxisOptions': this.arvprophylaxisOptions
        });

        this.maternalhistoryOptions.push({
            'motherstateOptions': this.motherstateOptions,
            'motherreceivedrugsOptions': this.motherreceivedrugsOptions,
            'heimotherregimenOptions': this.heimotherregimenOptions,
            'yesnoOptions': this.yesnoOptions,
            'motherdrugsatinfantenrollmentOptions': this.motherdrugsatinfantenrollmentOptions,
            'primarycaregiverOptions': this.primarycaregiverOptions
        });

        this.immunizationHistoryOptions.push({
            'immunizationPeriod': this.immunizationPeriodOptions,
            'immunizationGiven': this.immunizationGivenOptions

        });

        this.milestoneOptions.push({
            'assessed': this.milestoneAssessedOptions,
            'status': this.milestoneStatusOptions,
            'yesnoOption': this.yesnoOptions
        });

        this.tbAssessmentOptions.push({
            'yesnoOption': this.yesnoOptions,
            'sputumSmear': this.sputumSmearOptions,
            'genexpert': this.geneXpertOptions,
            'chestXray': this.chestXrayOptions,
            'tbScreeningOutcome': this.tbScreeningOptions,
            'iptOutcomes': this.iptOutcomeOptions,
        });

        this.hivtestingOptions.push({
            'hivTestType': this.heiHivTestingOptions,
            'testResults': this.heiHivTestingResultsOptions,
            'YesNo': this.yesnoOptions
        });

        this.nextAppointmentOptions.push({
            'YesNo': this.yesnoOptions
        });

        this.heiService.getPatientById(this.patientId).subscribe(
            (result) => {
                const { ptn_pk } = result;
                this.ptn_pk = ptn_pk;
            }
        );

        this.heiService.getHeiLabTests().subscribe(
            (result) => {
                for (let i = 0; i < result.length; i++) {
                    if (result[i].key == 'PCR') {
                        this.pcrLabTest = result[i].value;
                    } else if (result[i].key == 'Viral Load') {
                        this.viralLoadLabTest = result[i].value;
                    } else if (result[i].key == 'HIV Rapid Test') {
                        this.antibodyLabTest = result[i].value;
                    }
                }
            }
        );
    }

    onDeliveryNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }

    onMatHistoryNotify(formGroup: FormGroup): void {
        this.deliveryMatFormGroup.push(formGroup);
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    onInfantFeedingNotify(formGroup: FormGroup): void {
        this.infantFeedingFormGroup.push(formGroup);
    }

    onMilestonesNotify(formGroup: object): void {
        // this.milestonesFormGroup.push(formGroup['form']);
        this.milestone_table_data.push(formGroup['data']);
    }

    onImmunizationHistory(formGroup: Object): void {
        // this.immunizationHistoryFormGroup.push(formGroup['form']);
        this.immunization_table_data.push(formGroup['data']);
    }

    onTbAssessment(formGroup: FormGroup) {
        this.tbAssessmentFormGroup.push(formGroup);
    }

    onHeiOutcomeNotify(formGroup: FormGroup) {
        this.infantFeedingFormGroup.push(formGroup);
    }

    onHivTestingNotify(hivTests: any) {
        this.hivTestingFormGroup.push(hivTests);
    }

    onNextAppointmentNotify(formGroup: FormGroup) {
        this.infantFeedingFormGroup.push(formGroup);
    }

    onSubmit() {
        if (this.isEdit) {
            this.onUpdateHeiEncounter();
        } else {
            this.onCompleteEncounter();
        }
    }

    onUpdateHeiEncounter() {
        if (!this.infantFeedingFormGroup.valid) {
            this.snotifyService.error('Complete the highlighted fields before submitting', 'HEI Encounter',
                this.notificationService.getConfig());
            return;
        }

        this.spinner.show();

        const motherRegistered = this.yesnoOptions.filter(
            obj => obj.itemId == this.deliveryMatFormGroup.value[1]['motherregisteredinclinic']
        );

        let isMotherRegistered: boolean = false;
        if (motherRegistered.length > 0) {
            if (motherRegistered[0]['itemName'] == 'Yes') {
                isMotherRegistered = true;
            } else if (motherRegistered[0]['itemName'] == 'No') {
                isMotherRegistered = false;
            }
        }

        const heiDeliveryCommand: HeiDeliveryEditCommand = {
            Id: this.deliveryMatFormGroup.value[1]['id'],
            PlaceOfDeliveryId: this.deliveryMatFormGroup.value[0]['placeofdelivery'],
            ModeOfDeliveryId: this.deliveryMatFormGroup.value[0]['modeofdelivery'],
            BirthWeight: this.deliveryMatFormGroup.value[0]['birthweight'],
            ArvProphylaxisId: this.deliveryMatFormGroup.value[0]['arvprophylaxisreceived'],
            ArvProphylaxisOther: this.deliveryMatFormGroup.value[0]['arvprophylaxisother'],
            MotherIsRegistered: isMotherRegistered,
            MotherArtInfantEnrolRegimenId: this.deliveryMatFormGroup.value[1]['pmtctheimotherdrugsatinfantenrollment'],
            MotherPersonId: this.deliveryMatFormGroup.value[1]['motherpersonid'],
            MotherStatusId: this.deliveryMatFormGroup.value[1]['stateofmother'],
            PrimaryCareGiverID: this.deliveryMatFormGroup.value[1]['primarycaregiver'],
            MotherName: this.deliveryMatFormGroup.value[1]['nameofmother'],
            MotherCCCNumber: this.deliveryMatFormGroup.value[1]['cccno'],
            MotherPMTCTDrugsId: this.deliveryMatFormGroup.value[1]['pmtctheimotherreceivedrugs'],
            MotherPMTCTRegimenId: this.deliveryMatFormGroup.value[1]['pmtctheimotherregimen'],
            MotherPMTCTRegimenOther: this.deliveryMatFormGroup.value[1]['otherspecify'],
            MotherArtInfantEnrolId: this.deliveryMatFormGroup.value[1]['motheronartatinfantenrollment']
        };

        const heiFeedingCommand: HeiFeedingEditCommand = {
            Id: this.infantFeedingFormGroup.value[0]['id'],
            FeedingModeId: this.infantFeedingFormGroup.value[0]['infantFeedingOptions']
        };

        const heiOutComeCommand: HeiOutComeCommand = {
            HeiEncounterId: this.deliveryMatFormGroup.value[1]['id'],
            OutcomeAt24MonthsId: this.infantFeedingFormGroup.value[2]['heiOutcomeOptions']
        };

        const patientAppointmentEditCommand: PatientAppointmentEditCommand = {
            AppointmentId: this.infantFeedingFormGroup.value[1]['id'],
            AppointmentDate: this.infantFeedingFormGroup.value[1]['nextAppointmentDate'],
            Description: this.infantFeedingFormGroup.value[1]['serviceRemarks'],
            UserId: this.userId,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            DifferentiatedCareId: null,
            ReasonId: this.appointmentReasonId,
            ServiceAreaId: this.serviceAreaId,
            StatusId: this.appointmentStatusId
        };

        const heiAppointment: PatientAppointment = {
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            PatientId: this.patientId,
            AppointmentDate: this.infantFeedingFormGroup.value[1]['nextAppointmentDate'],
            Description: this.infantFeedingFormGroup.value[1]['serviceRemarks'],
            StatusDate: null,
            DifferentiatedCareId: 0,
            AppointmentReason: 'Follow Up',
            CreatedBy: this.userId
        };

        const OnAntiTbDrugsValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['currentlyOnAntiTb']);
        let OnAntiTbDrugs = null;
        if (OnAntiTbDrugsValue.length > 0) {
            OnAntiTbDrugs = OnAntiTbDrugsValue[0].itemName == 'Yes' ? true : false;
        }

        const CoughValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['coughAnyDuration']);
        let Cough = null;
        if (CoughValue.length > 0) {
            Cough = CoughValue[0].itemName == 'Yes' ? true : false;
        }

        const FeverValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['fever']);
        let Fever = null;
        if (FeverValue.length > 0) {
            Fever = FeverValue[0].itemName == 'Yes' ? true : false;
        }

        const WeightLossValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['weightLoss']);
        let WeightLoss = null;
        if (WeightLossValue.length > 0) {
            WeightLoss = WeightLossValue[0].itemName == 'Yes' ? true : false;
        }

        const ContactWithTbValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['contactTB']);
        let ContactWithTb = null;
        if (ContactWithTbValue.length > 0) {
            ContactWithTb = ContactWithTbValue[0].itemName == 'Yes' ? true : false;
        }

        const OnIptValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['onIPT']);
        let OnIpt = null;
        if (OnIptValue.length > 0) {
            OnIpt = OnIptValue[0].itemName == 'Yes' ? true : false;
        }

        const EverBeenOnIptValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['startIPT']);
        let EverBeenOnIpt = null;
        if (EverBeenOnIptValue.length > 0) {
            EverBeenOnIpt = EverBeenOnIptValue[0].itemName == 'Yes' ? true : false;
        }

        const patientIcf = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            OnAntiTbDrugs: OnAntiTbDrugs,
            Cough: Cough,
            Fever: Fever,
            WeightLoss: WeightLoss,
            ContactWithTb: ContactWithTb,
            OnIpt: OnIpt,
            EverBeenOnIpt: EverBeenOnIpt
        } as PatientIcf;

        const patientIcfAction = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            SputumSmear: this.tbAssessmentFormGroup.value[0]['sputumSmear'],
            ChestXray: this.tbAssessmentFormGroup.value[0]['chestXray'],
            GeneXpert: this.tbAssessmentFormGroup.value[0]['geneXpert'],
            StartAntiTb: this.tbAssessmentFormGroup.value[0]['startAntiTb'],
            EvaluatedForIpt: this.tbAssessmentFormGroup.value[0]['EvaluatedForAAntitb'],
            InvitationOfContacts: this.tbAssessmentFormGroup.value[0]['invitationContacts'],
        } as PatientIcfAction;

        const ordVisitCommand: OrdVisitCommand = {
            Ptn_Pk: this.ptn_pk,
            LocationID: this.locationId,
            VisitDate: this.visitDate,
            UserID: this.userId
        };


        const laborder: LabOrder = {
            Ptn_Pk: this.ptn_pk,
            PatientId: this.patientId,
            LocationId: this.locationId,
            FacilityId: this.locationId,
            VisitId: 1,
            ModuleId: 1,
            OrderedBy: this.userId,
            OrderDate: new Date(),
            ClinicalOrderNotes: '',
            CreateDate: new Date(),
            OrderStatus: 'Pending',
            UserId: this.userId,
            PatientMasterVisitId: this.patientMasterVisitId,
            LabTests: []
        };

        for (let i = 0; i < this.hivTestingFormGroup.length; i++) {
            let labTestId;
            let latTestNotes;
            let labTestName;
            for (let j = 0; j < this.hivTestingFormGroup[i].length; j++) {
                if (
                    this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '1st DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '2nd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '3rd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Repeat confirmatory PCR (for +ve)'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Confirmatory PCR (for  +ve)'
                ) {
                    labTestId = this.pcrLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.pcrLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.pcrLabTestParameters = res;
                        }
                    );
                    // this.pcrLabTestParameters
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Baseline Viral Load (for +ve)') {
                    labTestId = this.viralLoadLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.viralLoadLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.viralLoadLabTestParameters = res;
                        }
                    );
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Final Antibody') {
                    labTestId = this.antibodyLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.antibodyLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.antibodyLabTestParameters = res;
                        }
                    );
                }
            }
            if (labTestId) {
                laborder.LabTests.push({
                    Id: labTestId,
                    Notes: latTestNotes,
                    LabTestName: labTestName
                });
            }
        }

        const completeLabOrderCommand: CompleteLabOrderCommand = {
            LabOrderId: 0,
            LabOrderTestId: 0,
            LabTestId: 0,
            UserId: this.userId,
            LabTestResults: [],
            DateResultsCollected: moment(new Date()).utc(true).toDate()
        };

        const heiLabTestsTypes: any = {
            LabOrderId: 0,
            PatientId: this.patientId,
            HeiLabTestTypes: []
        };

        for (let i = 0; i < this.immunization_table_data.length; i++) {
            for (let j = 0; j < this.immunization_table_data[i].length; j++) {
                this.vaccination.push({
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    PeriodId: this.immunization_table_data[i][j]['immunizationPeriodId'],
                    Vaccine: this.immunization_table_data[i][j]['immunizationGivenId'],
                    VaccineStage: this.immunization_table_data[i][j]['immunizationPeriodId'],
                    DeleteFlag: 0,
                    CreatedBy: this.userId,
                    CreateDate: new Date(),
                    VaccineDate: moment(this.immunization_table_data[i][j]['dateImmunized']).toDate(),
                    Active: 0,
                    AppointmentId: 0
                    //  NextSchedule: new Date(this.immunization_table_data[i][j]['nextScheduled'])
                });
            }
        }

        for (let i = 0; i < this.milestone_table_data.length; i++) {
            for (let j = 0; j < this.milestone_table_data[i].length; j++) {
                this.milestone.push({
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    TypeAssessedId: this.milestone_table_data[i][j].milestoneId,
                    AchievedId: this.milestone_table_data[i][j].achievedId,
                    StatusId: this.milestone_table_data[i][j].statusId,
                    Comment: this.milestone_table_data[i][j].comment,
                    CreateDate: new Date(),
                    CreatedBy: this.userId,
                    DeleteFlag: 0,
                    DateAssessed: moment(this.milestone_table_data[i][j].dateAssessed).toDate()
                });
            }

        }

        const heiTbOutcomeCommand: any = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ScreeningTypeId: this.tbScreeningOptions[0].masterId,
            ScreeningDone: true,
            ScreeningDate: this.visitDate,
            ScreeningCategoryId: 0,
            ScreeningValueId: this.tbAssessmentFormGroup.value[0]['tbScreeningOutcome'],
            Comment: '',
            Active: 0,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: null,
            VisitDate: this.visitDate
        };

        const heiMilestone = this.heiService.saveMilestoneHistory(this.milestone);
        const heiImmunization = this.heiService.saveImmunizationHistory(this.vaccination);
        const heiOrdVisit = this.heiService.saveOrdVisit(ordVisitCommand, laborder);
        const heitbAssessment = this.heiService.saveTbAssessment(patientIcf, patientIcfAction);
        const heiDeliveryEditCommand = this.heiService.updateHeiDelivery(heiDeliveryCommand);
        const heiFeedingEditCommand = this.heiService.updateHeiInfantFeeding(heiFeedingCommand);
        const heiOutCome = this.heiService.saveHeiOutCome(heiOutComeCommand);
        const heiUpdateAppointment = this.pncService.updateAppointment(patientAppointmentEditCommand);
        const heiAppoinment = this.pncService.savePncNextAppointment(heiAppointment);
        const heiTbOutcome = this.heiService.saveHeiTbOutcome(heiTbOutcomeCommand);

        let isAddOrInsertAppointment;
        if (patientAppointmentEditCommand.AppointmentId && patientAppointmentEditCommand.AppointmentId > 0) {
            isAddOrInsertAppointment = heiUpdateAppointment;
        } else {
            isAddOrInsertAppointment = heiAppoinment;
        }

        forkJoin([heiOrdVisit, heiDeliveryEditCommand, heiFeedingEditCommand, heiOutCome,
            isAddOrInsertAppointment, heitbAssessment, heiImmunization, heiMilestone, heiTbOutcome]).subscribe(
                (result) => {
                    laborder.VisitId = result[0]['visit_Id'];
                    const heiLab = this.heiService.saveHeiLabOrder(laborder).pipe(
                        mergeMap(res => this.heiService.getLabOrderTestsByOrderId(res['labOrderId']))
                    ).subscribe(res => {
                        // console.log(res);
                        if (res.length > 0 && res[0]['labOrderId']) {
                            completeLabOrderCommand.LabOrderId = res[0]['labOrderId'];
                            completeLabOrderCommand.LabOrderTestId = res[0]['id'];
                            completeLabOrderCommand.LabTestId = res[0]['labTestId'];

                            // set hei lab test types
                            heiLabTestsTypes.LabOrderId = res[0]['labOrderId'];
                        }

                        for (let i = 0; i < this.hivTestingFormGroup.length; i++) {
                            for (let j = 0; j < this.hivTestingFormGroup[i].length; j++) {
                                completeLabOrderCommand.DateResultsCollected = 
                                    moment(this.hivTestingFormGroup[i][j]['dateresultscollected']).utc(true).toDate();
                                if (
                                    this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '1st DNA PCR'
                                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '2nd DNA PCR'
                                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '3rd DNA PCR'
                                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Repeat confirmatory PCR (for +ve)'
                                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Confirmatory PCR (for  +ve)'
                                ) {
                                    heiLabTestsTypes.HeiLabTestTypes.push({
                                        id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                    });
                                    if (this.pcrLabTestParameters.length > 0) {
                                        if (this.hivTestingFormGroup[i][j]['result'] && this.hivTestingFormGroup[i][j]['result'] != null
                                            && this.hivTestingFormGroup[i][j]['result'] != '') {
                                            completeLabOrderCommand.LabTestResults.push({
                                                ParameterId: this.pcrLabTestParameters[0]['id'],
                                                ResultValue: null,
                                                ResultText: this.hivTestingFormGroup[i][j]['result']['itemName'],
                                                ResultOptionId: null,
                                                ResultOption: null,
                                                ResultUnit: null,
                                                ResultUnitId: null,
                                                Undetectable: false,
                                                DetectionLimit: this.pcrLabTestParameters[0]['detectionLimit'],
                                            });
                                        }
                                    }
                                } else if (
                                    this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Baseline Viral Load (for +ve)') {
                                    heiLabTestsTypes.HeiLabTestTypes.push({
                                        id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                    });
                                    if (this.viralLoadLabTestParameters.length > 0) {
                                        if (this.hivTestingFormGroup[i][j]['resultText']
                                            && this.hivTestingFormGroup[i][j]['resultText'] != null
                                            && this.hivTestingFormGroup[i][j]['resultText'] != '') {
                                            completeLabOrderCommand.LabTestResults.push({
                                                ParameterId: this.viralLoadLabTestParameters[0]['id'],
                                                ResultValue: this.hivTestingFormGroup[i][j]['resultText'],
                                                ResultText: null,
                                                ResultOptionId: null,
                                                ResultOption: null,
                                                ResultUnit: null,
                                                ResultUnitId: this.viralLoadLabTestParameters[0]['unitId'],
                                                Undetectable: false,
                                                DetectionLimit: this.viralLoadLabTestParameters[0]['detectionLimit'],
                                            });
                                        }
                                    }
                                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Final Antibody') {
                                    heiLabTestsTypes.HeiLabTestTypes.push({
                                        id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                    });
                                    if (this.antibodyLabTestParameters.length > 0) {
                                        if (this.hivTestingFormGroup[i][j]['result'] && this.hivTestingFormGroup[i][j]['result'] != null
                                            && this.hivTestingFormGroup[i][j]['result'] != '') {
                                            completeLabOrderCommand.LabTestResults.push({
                                                ParameterId: this.antibodyLabTestParameters[0]['id'],
                                                ResultValue: this.hivTestingFormGroup[i][j]['result']['itemName'] == 'Positive' ? 1 : 2,
                                                ResultText: null,
                                                ResultOptionId: null,
                                                ResultOption: null,
                                                ResultUnit: null,
                                                ResultUnitId: this.antibodyLabTestParameters[0]['unitId'],
                                                Undetectable: false,
                                                DetectionLimit: this.antibodyLabTestParameters[0]['detectionLimit'],
                                            });
                                        }
                                    }
                                }
                            }
                        }

                        const completeHeiLabOrder = this.heiService.saveCompleteHeiLabOrder(completeLabOrderCommand).subscribe(
                            (completeRes) => {
                                // console.log('complete laborder');
                                console.log(completeRes);
                            },
                            (completeError) => {
                                console.log('Error completing laborder' + completeError);
                            }
                        );

                        const heiLabTestsCommand = this.heiService.saveHeiLabTestsTypes(heiLabTestsTypes).subscribe(
                            (completeRes) => {
                                // console.log('complete laborder');
                                console.log(completeRes);
                            },
                            (completeError) => {
                                console.log('Error completing laborder' + completeError);
                            }
                        );
                    });

                    this.spinner.hide();
                    this.snotifyService.success('Successfully updated HEI encounter ', 'HEI', this.notificationService.getConfig());
                    this.zone.run(() => {
                        this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                    });
                }
            );

    }

    onCompleteEncounter() {
        if (!this.infantFeedingFormGroup.valid) {
            this.snotifyService.error('Complete the highlighted fields before submitting', 'HEI Encounter',
                this.notificationService.getConfig());
            return;
        }

        this.spinner.show();

        for (let i = 0; i < this.immunization_table_data.length; i++) {
            for (let j = 0; j < this.immunization_table_data[i].length; j++) {
                this.vaccination.push({
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    PeriodId: this.immunization_table_data[i][j]['immunizationPeriodId'],
                    Vaccine: this.immunization_table_data[i][j]['immunizationGivenId'],
                    VaccineStage: this.immunization_table_data[i][j]['immunizationPeriodId'],
                    DeleteFlag: 0,
                    CreatedBy: this.userId,
                    CreateDate: new Date(),
                    VaccineDate: moment(this.immunization_table_data[i][j]['dateImmunized']).toDate(),
                    Active: 0,
                    AppointmentId: 0
                    //  NextSchedule: new Date(this.immunization_table_data[i][j]['nextScheduled'])
                });
            }
        }

        for (let i = 0; i < this.milestone_table_data.length; i++) {
            for (let j = 0; j < this.milestone_table_data[i].length; j++) {
                this.milestone.push({
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    TypeAssessedId: this.milestone_table_data[i][j].milestoneId,
                    AchievedId: this.milestone_table_data[i][j].achievedId,
                    StatusId: this.milestone_table_data[i][j].statusId,
                    Comment: this.milestone_table_data[i][j].comment,
                    CreateDate: new Date(),
                    CreatedBy: this.userId,
                    DeleteFlag: 0,
                    DateAssessed: moment(this.milestone_table_data[i][j].dateAssessed).toDate()
                });
            }

        }

        const OnAntiTbDrugsValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['currentlyOnAntiTb']);
        let OnAntiTbDrugs = null;
        if (OnAntiTbDrugsValue.length > 0) {
            OnAntiTbDrugs = OnAntiTbDrugsValue[0].itemName == 'Yes' ? true : false;
        }

        const CoughValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['coughAnyDuration']);
        let Cough = null;
        if (CoughValue.length > 0) {
            Cough = CoughValue[0].itemName == 'Yes' ? true : false;
        }

        const FeverValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['fever']);
        let Fever = null;
        if (FeverValue.length > 0) {
            Fever = FeverValue[0].itemName == 'Yes' ? true : false;
        }

        const WeightLossValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['weightLoss']);
        let WeightLoss = null;
        if (WeightLossValue.length > 0) {
            WeightLoss = WeightLossValue[0].itemName == 'Yes' ? true : false;
        }

        const ContactWithTbValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['contactTB']);
        let ContactWithTb = null;
        if (ContactWithTbValue.length > 0) {
            ContactWithTb = ContactWithTbValue[0].itemName == 'Yes' ? true : false;
        }

        const OnIptValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['onIPT']);
        let OnIpt = null;
        if (OnIptValue.length > 0) {
            OnIpt = OnIptValue[0].itemName == 'Yes' ? true : false;
        }

        const EverBeenOnIptValue = this.yesnoOptions.filter(obj => obj.itemId == this.tbAssessmentFormGroup.value[0]['startIPT']);
        let EverBeenOnIpt = null;
        if (EverBeenOnIptValue.length > 0) {
            EverBeenOnIpt = EverBeenOnIptValue[0].itemName == 'Yes' ? true : false;
        }

        const patientIcf = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            OnAntiTbDrugs: OnAntiTbDrugs,
            Cough: Cough,
            Fever: Fever,
            WeightLoss: WeightLoss,
            ContactWithTb: ContactWithTb,
            OnIpt: OnIpt,
            EverBeenOnIpt: EverBeenOnIpt
        } as PatientIcf;

        const patientIcfAction = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            SputumSmear: this.tbAssessmentFormGroup.value[0]['sputumSmear'],
            ChestXray: this.tbAssessmentFormGroup.value[0]['chestXray'],
            GeneXpert: this.tbAssessmentFormGroup.value[0]['geneXpert'],
            StartAntiTb: this.tbAssessmentFormGroup.value[0]['startAntiTb'],
            EvaluatedForIpt: this.tbAssessmentFormGroup.value[0]['EvaluatedForAAntitb'],
            InvitationOfContacts: this.tbAssessmentFormGroup.value[0]['invitationContacts'],
        } as PatientIcfAction;

        const ordVisitCommand: OrdVisitCommand = {
            Ptn_Pk: this.ptn_pk,
            LocationID: this.locationId,
            VisitDate: this.visitDate,
            UserID: this.userId
        };


        const laborder: LabOrder = {
            Ptn_Pk: this.ptn_pk,
            PatientId: this.patientId,
            LocationId: this.locationId,
            FacilityId: this.locationId,
            VisitId: 1,
            ModuleId: 1,
            OrderedBy: this.userId,
            OrderDate: new Date(),
            ClinicalOrderNotes: '',
            CreateDate: new Date(),
            OrderStatus: 'Pending',
            UserId: this.userId,
            PatientMasterVisitId: this.patientMasterVisitId,
            LabTests: []
        };

        for (let i = 0; i < this.hivTestingFormGroup.length; i++) {
            let labTestId;
            let latTestNotes;
            let labTestName;
            for (let j = 0; j < this.hivTestingFormGroup[i].length; j++) {
                if (
                    this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '1st DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '2nd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '3rd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Repeat confirmatory PCR (for +ve)'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Confirmatory PCR (for  +ve)'
                ) {
                    labTestId = this.pcrLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.pcrLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.pcrLabTestParameters = res;
                        }
                    );
                    // this.pcrLabTestParameters
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Baseline Viral Load (for +ve)') {
                    labTestId = this.viralLoadLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.viralLoadLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.viralLoadLabTestParameters = res;
                        }
                    );
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Final Antibody') {
                    labTestId = this.antibodyLabTest.id;
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.antibodyLabTest.name;

                    this.heiService.getLabTestPametersByLabTestId(labTestId).subscribe(
                        (res) => {
                            this.antibodyLabTestParameters = res;
                        }
                    );
                }
            }
            if (labTestId) {
                laborder.LabTests.push({
                    Id: labTestId,
                    Notes: latTestNotes,
                    LabTestName: labTestName
                });
            }
        }

        const motherRegistered = this.yesnoOptions.filter(
            obj => obj.itemId == this.deliveryMatFormGroup.value[1]['motherregisteredinclinic']
        );

        let isMotherRegistered: boolean = false;
        if (motherRegistered.length > 0) {
            if (motherRegistered[0]['itemName'] == 'Yes') {
                isMotherRegistered = true;
            } else if (motherRegistered[0]['itemName'] == 'No') {
                isMotherRegistered = false;
            }
        }

        const visitDetailsData = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: 0,
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            UserId: this.userId,
            DaysPostPartum: 0,
            AgeMenarche: 0,
        };

        const patientFeedingCommand: PatientFeedingCommand = {
            PatientMasterVisitId: this.patientMasterVisitId,
            PatientId: this.patientId,
            UserId: this.userId,
            FeedingModeId: this.infantFeedingFormGroup.value[0]['infantFeedingOptions']
        };

        const heiAppointment: PatientAppointment = {
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            PatientId: this.patientId,
            AppointmentDate: this.infantFeedingFormGroup.value[1]['nextAppointmentDate'],
            Description: this.infantFeedingFormGroup.value[1]['serviceRemarks'],
            StatusDate: null,
            DifferentiatedCareId: 0,
            AppointmentReason: 'Follow Up',
            CreatedBy: this.userId
        };

        const heiOutComeCommand: HeiOutComeCommand = {
            HeiEncounterId: 0,
            OutcomeAt24MonthsId: this.infantFeedingFormGroup.value[2]['heiOutcomeOptions']
        };

        const completeLabOrderCommand: CompleteLabOrderCommand = {
            LabOrderId: 0,
            LabOrderTestId: 0,
            LabTestId: 0,
            UserId: this.userId,
            LabTestResults: [],
            DateResultsCollected: new Date()
        };

        const heiLabTestsTypes: any = {
            LabOrderId: 0,
            PatientId: this.patientId,
            HeiLabTestTypes: []
        };

        if (isMotherRegistered) {
            const personRelation = {};
            personRelation['PersonId'] = this.deliveryMatFormGroup.value[1]['motherpersonid'];
            personRelation['PatientId'] = this.patientId;
            personRelation['RelationshipTypeId'] = this.motherRelationshipId;
            personRelation['UserId'] = this.userId;

            const patientAdd = this.registrationService.addPersonRelationship(personRelation);
            patientAdd.subscribe(
                (relationshipResult) => {
                    console.log(relationshipResult);
                }
            );
        }

        const heiTbOutcomeCommand: any = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ScreeningTypeId: this.tbScreeningOptions[0].masterId,
            ScreeningDone: true,
            ScreeningDate: this.visitDate,
            ScreeningCategoryId: 0,
            ScreeningValueId: this.tbAssessmentFormGroup.value[0]['tbScreeningOutcome'],
            Comment: '',
            Active: 0,
            DeleteFlag: false,
            CreatedBy: this.userId,
            CreateDate: new Date(),
            AuditData: null,
            VisitDate: this.visitDate
        };

        const heiVisitDetails = this.heiService.saveHeiVisitDetails(visitDetailsData);
        const heiDelivery = this.heiService.saveHieDelivery(this.patientId, this.patientMasterVisitId, this.userId,
            isMotherRegistered, this.deliveryMatFormGroup.value[0], this.deliveryMatFormGroup.value[1]);
        const heiImmunization = this.heiService.saveImmunizationHistory(this.vaccination);
        const heiMilestone = this.heiService.saveMilestoneHistory(this.milestone);
        const heitbAssessment = this.heiService.saveTbAssessment(patientIcf, patientIcfAction);
        const heiOrdVisit = this.heiService.saveOrdVisit(ordVisitCommand, laborder);
        const heiFeeding = this.heiService.saveHeiInfantFeeding(patientFeedingCommand);
        const heiAppoinment = this.pncService.savePncNextAppointment(heiAppointment);
        const heiTbOutcome = this.heiService.saveHeiTbOutcome(heiTbOutcomeCommand);

        forkJoin([
            heiOrdVisit,
            heiVisitDetails,
            heiImmunization,
            heiMilestone,
            heiDelivery,
            heiFeeding,
            heiAppoinment,
            heitbAssessment,
            heiTbOutcome
        ]).subscribe(
            (result) => {
                laborder.VisitId = result[0]['visit_Id'];
                const heiLab = this.heiService.saveHeiLabOrder(laborder).pipe(
                    mergeMap(res => this.heiService.getLabOrderTestsByOrderId(res['labOrderId']))
                ).subscribe(res => {
                    if (res.length > 0 && res[0]['labOrderId']) {
                        completeLabOrderCommand.LabOrderId = res[0]['labOrderId'];
                        completeLabOrderCommand.LabOrderTestId = res[0]['id'];
                        completeLabOrderCommand.LabTestId = res[0]['labTestId'];

                        heiLabTestsTypes.LabOrderId = res[0]['labOrderId'];
                    }

                    for (let i = 0; i < this.hivTestingFormGroup.length; i++) {
                        for (let j = 0; j < this.hivTestingFormGroup[i].length; j++) {
                            completeLabOrderCommand.DateResultsCollected =
                                this.hivTestingFormGroup[i][j]['dateresultscollected'];
                            if (
                                this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '1st DNA PCR'
                                || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '2nd DNA PCR'
                                || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '3rd DNA PCR'
                                || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Repeat confirmatory PCR (for +ve)'
                                || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Confirmatory PCR (for  +ve)'
                            ) {
                                heiLabTestsTypes.HeiLabTestTypes.push({
                                    id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                });
                                if (this.pcrLabTestParameters.length > 0) {
                                    if (this.hivTestingFormGroup[i][j]['result'] && this.hivTestingFormGroup[i][j]['result'] != null
                                        && this.hivTestingFormGroup[i][j]['result'] != '') {
                                        completeLabOrderCommand.LabTestResults.push({
                                            ParameterId: this.pcrLabTestParameters[0]['id'],
                                            ResultValue: null,
                                            ResultText: this.hivTestingFormGroup[i][j]['result']['itemName'],
                                            ResultOptionId: null,
                                            ResultOption: null,
                                            ResultUnit: null,
                                            ResultUnitId: null,
                                            Undetectable: false,
                                            DetectionLimit: this.pcrLabTestParameters[0]['detectionLimit'],
                                        });
                                    }
                                }
                            } else if (
                                this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Baseline Viral Load (for +ve)') {
                                heiLabTestsTypes.HeiLabTestTypes.push({
                                    id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                });

                                if (this.viralLoadLabTestParameters.length > 0) {
                                    if (this.hivTestingFormGroup[i][j]['resultText'] != null
                                        && this.hivTestingFormGroup[i][j]['resultText'] != '') {
                                        completeLabOrderCommand.LabTestResults.push({
                                            ParameterId: this.viralLoadLabTestParameters[0]['id'],
                                            ResultValue: this.hivTestingFormGroup[i][j]['resultText'],
                                            ResultText: null,
                                            ResultOptionId: null,
                                            ResultOption: null,
                                            ResultUnit: null,
                                            ResultUnitId: this.viralLoadLabTestParameters[0]['unitId'],
                                            Undetectable: false,
                                            DetectionLimit: this.viralLoadLabTestParameters[0]['detectionLimit'],
                                        });
                                    }
                                }
                            } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Final Antibody') {
                                heiLabTestsTypes.HeiLabTestTypes.push({
                                    id: this.hivTestingFormGroup[i][j]['testtype']['itemId']
                                });

                                if (this.antibodyLabTestParameters.length > 0) {
                                    if (this.hivTestingFormGroup[i][j]['result'] != null
                                        && this.hivTestingFormGroup[i][j]['result'] != '') {
                                        completeLabOrderCommand.LabTestResults.push({
                                            ParameterId: this.antibodyLabTestParameters[0]['id'],
                                            ResultValue: this.hivTestingFormGroup[i][j]['result']['itemName'] == 'Positive' ? 1 : 2,
                                            ResultText: null,
                                            ResultOptionId: null,
                                            ResultOption: null,
                                            ResultUnit: null,
                                            ResultUnitId: this.antibodyLabTestParameters[0]['unitId'],
                                            Undetectable: false,
                                            DetectionLimit: this.antibodyLabTestParameters[0]['detectionLimit'],
                                        });
                                    }
                                }
                            }
                        }
                    }

                    const completeHeiLabOrder = this.heiService.saveCompleteHeiLabOrder(completeLabOrderCommand).subscribe(
                        (completeRes) => {
                            // console.log('complete laborder');
                            console.log(completeRes);
                        },
                        (completeError) => {
                            console.log('Error completing laborder' + completeError);
                        }
                    );

                    const heiLabTestsCommand = this.heiService.saveHeiLabTestsTypes(heiLabTestsTypes).subscribe(
                        (completeRes) => {
                            // console.log('complete laborder');
                            console.log(completeRes);
                        },
                        (completeError) => {
                            console.log('Error completing laborder' + completeError);
                        }
                    );
                });

                heiOutComeCommand.HeiEncounterId = result[4]['heiEncounterId'];
                const heiOutCome = this.heiService.saveHeiOutCome(heiOutComeCommand).subscribe(

                );

                this.spinner.hide();
                this.snotifyService.success('Successfully saved HEI encounter ', 'HEI', this.notificationService.getConfig());
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
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
}
