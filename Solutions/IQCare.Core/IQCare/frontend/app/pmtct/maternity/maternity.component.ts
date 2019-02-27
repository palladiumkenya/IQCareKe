import { AncService } from './../_services/anc.service';
import { Component, OnInit, NgZone } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { MaternityVisitDetailsCommand } from './commands/maternity-visit-details-command';
import { MaternityService } from '../_services/maternity.service';
import { PregnancyCommand } from './commands/pregnancy-command';
import { MaternityDeliveryCommand } from './commands/maternity-delivery-command';
import { ApgarScoreCommand } from './commands/apgar-score-command';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { forkJoin, Subscription } from 'rxjs/index';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { DrugAdministrationCommand } from './commands/drug-administration-command';
import { AdministeredDrugInfo } from './commands/administer-drug-info';
import { MaternityCounsellingCommand } from './commands/maternity-counselling-command';
import { ReferralCommand } from './commands/referral-command';
import { NextAppointmentCommand } from './commands/next-appointment-command';
import { DischargeCommand } from './commands/discharge-command';
import { DiagnosisCommand } from './commands/diagnosis-command';
import { MotherProfileCommand } from './commands/mother-profile-command';
import { PartnerTestingCommand } from '../_models/PartnerTestingCommand';
import { PatientReferralCommand } from '../_models/PatientReferralCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { HivTestsCommand } from '../_models/HivTestsCommand';
import * as moment from 'moment';
import { VisitDetailsCommand } from '../_models/visit-details-command';
import { VisitDetailsService } from '../_services/visit-details.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { AddBirthInfoComponent } from './baby/add-birth-info/add-birth-info.component';
import { AddBabyDialogComponent } from './baby/add-baby-dialog/add-baby-dialog.component';

@Component({
    selector: 'app-maternity',
    templateUrl: './maternity.component.html',
    styleUrls: ['./maternity.component.css']
})
export class MaternityComponent implements OnInit {
    isLinear: boolean = true;
    public visitId: number;
    public isEdit = false;

    visitDetailsFormGroup: FormArray;
    diagnosisFormGroup: FormArray;
    babyFormGroup: FormArray;
    maternityTestsFormGroup: FormArray;
    maternalDrugAdministrationForGroup: FormArray;
    dischargeFormGroup: FormArray;
    formType: string;
    apgarSCore: ApgarScoreCommand[] = [];
    AdministeredDrugs: AdministeredDrugInfo[] = [];
    lookupItems$: Subscription;
    apgarOptions: any[] = [];
    drugAdminOptions: any[] = [];
    counsellingOptions: any[] = [];
    babyNotifyData: any[] = [];
    pncHivOptions: any[] = [];
    DeliveredBabyBirthInfoCollection: any[] = [];
    hiv_status_table_data: any[] = [];
    hivTestEntryPoint: number;
    htsEncounterId: number;


    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    patientEncounterId: number;
    visitDate: Date;
    visitType: number;
    pregnancyId: number = 0;
    deliveryId: number;


    deliveryModeOptions: LookupItemView[] = [];
    bloodLossOptions: LookupItemView[] = [];
    motherStateOptions: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    yesnoOptions: LookupItemView[] = [];
    genderOptions: LookupItemView[] = [];
    deliveryOutcomeOptions: LookupItemView[] = [];
    birthoutcomeOptions: LookupItemView[] = [];
    yesNoNaOptions: LookupItemView[] = [];
    referralOptions: LookupItemView[] = [];
    hivFinalResultOptions: LookupItemView[] = [];
    hivTestOptions: LookupItemView[] = [];
    kitNameOptions: LookupItemView[] = [];
    hivTestResultOptions: LookupItemView[] = [];
    // hivTestResultsOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];
    hivStatusOptions: LookupItemView[] = [];

    diagnosisOptions: any[] = [];
    babySectionOptions: any[] = [];
    maternityTestOptions: any[] = [];
    drugAdministrationOptions: any[] = [];
    dischargeOptions: any[] = [];
    partnerTestingOptions: any[] = [];
    patientEducationOptions: any[] = [];

    constructor(private route: ActivatedRoute,
        private matService: MaternityService,
        private _lookupItemService: LookupItemService,
        private visitDetailsService: VisitDetailsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        public zone: NgZone,
        private router: Router,
        private dialog: MatDialog,
        private ancservice: AncService,
        private lookupitemservice: LookupItemService) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.diagnosisFormGroup = new FormArray([]);
        this.maternalDrugAdministrationForGroup = new FormArray([]);
        this.dischargeFormGroup = new FormArray([]);
        this.babyFormGroup = new FormArray([]);
        this.maternityTestsFormGroup = new FormArray([]);
        this.formType = 'maternity';
    }

    ngOnInit() {
        this.getLookupItems('ApgarScore', this.apgarOptions);
        this.getLookupItems('MaternalDrugAdministration', this.drugAdminOptions);
        this.getLookupItems('counselledOn', this.counsellingOptions);

        this.lookupitemservice.getByGroupNameAndItemName('HTSEntryPoints', 'PMTCT').subscribe(
            (res) => {
                this.hivTestEntryPoint = res['itemId'];
            }
        );

        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const { patientId, personId, serviceAreaId, patientMasterVisitId } = params;
                this.patientId = parseInt(patientId, 10);
                this.personId = personId;
                this.patientMasterVisitId = patientMasterVisitId;
                this.serviceAreaId = serviceAreaId;
                this.patientEncounterId = params.patientEncounterId;

                if (!this.patientMasterVisitId) {
                    console.log('test here');
                    this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
                    this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
                } else {
                    this.visitId = this.patientMasterVisitId;
                    this.isEdit = true;
                }
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        // this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
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
                birthOutcomeOptions,
                yesNoNaOptions,
                referralOptions,
                hivFinalResultOptions,
                hivFinalResultsOptions,
                hivStatusOptions
            } = res;
            this.deliveryModeOptions = deliveryModeOptions['lookupItems'];
            this.bloodLossOptions = bloodLossOptions['lookupItems'];
            this.motherStateOptions = motherStateOptions['lookupItems'];
            this.yesNoNaOptions = yesNoNaOptions['lookupItems'];
            this.yesNoOptions = yesNoOptions['lookupItems'];
            this.yesnoOptions = yesNoOptions['lookupItems'];
            this.genderOptions = genderOptions['lookupItems'];
            this.deliveryOutcomeOptions = deliveryOutcomeOptions['lookupItems'];
            this.birthoutcomeOptions = birthOutcomeOptions['lookupItems'];
            this.referralOptions = referralOptions['lookupItems'];
            this.hivFinalResultOptions = hivFinalResultOptions['lookupItems'];
            this.hivFinalResultsOptions = hivFinalResultsOptions['lookupItems'];
            this.hivStatusOptions = hivStatusOptions['lookupItems'];
        });

        this.diagnosisOptions.push({
            'deliveryModes': this.deliveryModeOptions,
            'bloodLoss': this.bloodLossOptions,
            'motherStates': this.motherStateOptions,
            'yesNos': this.yesNoOptions,
        });

        this.pncHivOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'hivFinalResultsOptions': this.hivFinalResultsOptions
        });

        // console.log('hiv test');
        // console.log(this.pncHivOptions);

        this.babySectionOptions.push({
            'gender': this.genderOptions,
            'deliveryOutcomes': this.deliveryOutcomeOptions,
            'birthOutcomes': this.birthoutcomeOptions,
            'yesNos': this.yesNoOptions
        });

        this.maternityTestOptions.push({
            'yesNos': this.yesNoOptions,
            'hivStatusOptions': this.hivStatusOptions
        });

        this.drugAdministrationOptions.push({
            'yesNo': this.yesNoOptions,
            'finalResult': this.hivFinalResultOptions,
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
            'yesnoOptions': this.yesNoOptions
        });
    }

    log() {
        console.log(this.visitDetailsFormGroup);
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
        this.getPatientPregnancy(this.patientId);
    }

    OnMotherProfileNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    onPatientDiagnosisNotify(formGroup: FormGroup): void {
        this.diagnosisFormGroup.push(formGroup);
    }

    onPatientDeliveryNotify(formGroup: FormGroup): void {
        this.diagnosisFormGroup.push(formGroup);
    }

    onBabyNotify(formGroup: FormGroup): void {
        this.babyFormGroup.push(formGroup);
    }

    onBabyNotifyData(babyNotifyData: any[]): void {
        console.log("Baby Notify data " + this.babyNotifyData.length);
        this.babyNotifyData.push(babyNotifyData);
    }

    onMaternityTests(formGroup: FormGroup): void {
        this.maternityTestsFormGroup.push(formGroup);
    }

    onHivStatusNotify(formGroup: Object): void {
        this.maternityTestsFormGroup.push(formGroup['form']);
        // console.log(formGroup);
        this.hiv_status_table_data.push(formGroup['table_data']);
    }

    onMaternalDrugAdministration(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPartnerTestingNotify(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }

    onPatientEducationNotify(formGroup: FormGroup): void {
        this.maternalDrugAdministrationForGroup.push(formGroup);
    }


    onPatientDischarge(formGroup: FormGroup): void {
        this.dischargeFormGroup.push(formGroup);
    }


    onPatientreferralNotify(formGroup: FormGroup): void {
        this.dischargeFormGroup.push(formGroup);
    }

    onPatientNextAppointent(formGroup: FormGroup): void {
        this.dischargeFormGroup.push(formGroup);
    }

    public getLookupItems(groupName: string, objOptions: any[] = []) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        objOptions.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    // console.log(this.lookupItems$);
                });
    }

    public getPatientPregnancy(patientId: number) {
        this.visitDetailsService.getPregnancyProfile(patientId)
            .subscribe(
                pregnancy => {
                    if (pregnancy != null) {
                        this.pregnancyId = pregnancy.id;
                        console.log('pregancyId:' + this.pregnancyId);
                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching pregnancy' + err, 'Pregnancy Profile', this.notificationService.getConfig());
                },
                () => {

                }
            );
    }

    onSubmit() {
        const visitDetailsCommand = {
            PatientId: parseInt(this.patientId.toString(), 10),
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            PregnancyId: this.pregnancyId,
            PatientMasterVisitId: this.patientMasterVisitId,
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: 0,
            DaysPostPartum: 0,
            VisitType: 0,
            UserId: this.userId
        } as VisitDetailsCommand;

        console.log(this.visitDetailsFormGroup);
        const pregnancyCommand: PregnancyCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Outcome: 0,
            DateOfOutcome: null,
            Lmp: moment(this.visitDetailsFormGroup.value[1]['dateLMP']).toDate(),
            Edd: moment(this.visitDetailsFormGroup.value[1]['dateEDD']).toDate(),
            Gestation: this.visitDetailsFormGroup.value[1]['gestation'],
            Gravidae: parseInt(this.visitDetailsFormGroup.value[1]['gravidae'], 10),
            Parity: this.visitDetailsFormGroup.value[1]['parityOne'],
            Parity2: this.visitDetailsFormGroup.value[1]['parityTwo'],
            AgeAtMenarche: this.visitDetailsFormGroup.value[1]['ageAtMenarche'],
            CreateDate: new Date(),
            CreatedBy: this.userId,
            DeleteFlag: false
        };

        const diagnosisCommand: DiagnosisCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Diagnosis: this.diagnosisFormGroup.value[0]['diagnosis'],
            ManagementPlan: 'na',
            CreatedBy: this.userId
        };

        const maternityDeliveryCommand: MaternityDeliveryCommand = {
            PatientMasterVisitId: this.patientMasterVisitId,
            PregnancyId: this.pregnancyId,
            DurationOfLabour: this.diagnosisFormGroup.value[1]['labourDuration'],
            DateOfDelivery: moment(this.diagnosisFormGroup.value[1]['deliveryDate']).toDate(),
            TimeOfDelivery: this.diagnosisFormGroup.value[1]['deliveryTime'],
            ModeOfDelivery: this.diagnosisFormGroup.value[1]['deliveryMode'].itemId,
            PlacentaComplete: this.diagnosisFormGroup.value[1]['placentaComplete'].itemId,
            BloodLossCapacity: parseInt(this.diagnosisFormGroup.value[1]['bloodLossCount'], 10),
            BloodLossClassification: this.diagnosisFormGroup.value[1]['bloodLoss'].itemId,
            MotherCondition: this.diagnosisFormGroup.value[1]['deliveryCondition'].itemId,
            MaternalDeathAudited: this.diagnosisFormGroup.value[1]['maternalDeathsAudited'],
            MaternalDeathAuditDate: moment(this.diagnosisFormGroup.value[1]['auditDate']).toDate(),
            DeliveryComplicationsExperienced: this.diagnosisFormGroup.value[1]['deliveryComplications'].itemId,
            DeliveryComplicationNotes: this.diagnosisFormGroup.value[1]['deliveryComplicationNotes'],
            DeliveryConductedBy: this.diagnosisFormGroup.value[1]['deliveryConductedBy'],
            CreatedBy: this.userId
        };

        const apgarscoreOne = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 1 min');
        const apgarscoreTwo = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 5 min');
        const apgarscoreThree = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 10 min');

        console.log('baby data' + this.babyNotifyData.length);
        console.log(this.babyNotifyData);

        for (let i = 0; i < this.babyNotifyData.length; i++) {
            for (let j = 0; j < this.babyNotifyData[i].length; j++) {
                this.DeliveredBabyBirthInfoCollection.push({

                    PatientDeliveryInformationId: 0,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    BirthWeight: parseFloat(this.babyNotifyData[i][j]['birthWeight']),
                    Sex: this.babyNotifyData[i][j]['sex'],
                    DeliveryOutcome: this.babyNotifyData[i][j]['outcome'],
                    ResuscitationDone: this.babyNotifyData[i][j]['resuscitate'],
                    BirthDeformity: this.babyNotifyData[i][j]['deformity'],
                    TeoGiven: this.babyNotifyData[i][j]['teo'],
                    BreastFedWithinHour: this.babyNotifyData[i][j]['breastFeeding'],
                    BirthNotificationNumber: this.babyNotifyData[i][j]['notificationNo'],
                    Comment: this.babyNotifyData[i][j]['comment'],
                    CreatedBy: this.userId,
                    ApgarScores: [
                        {
                            ApgarSCoreId: apgarscoreOne[0].itemId, SCore: this.babyNotifyData[i][j]['apgarScoreOne'],
                            ApgarScoreType: 'Apgar Score 1 min'
                        },
                        {
                            ApgarSCoreId: apgarscoreTwo[0].itemId, SCore: this.babyNotifyData[i][j]['apgarScoreFive'],
                            ApgarScoreType: 'Apgar Score 5 min'
                        },
                        {
                            ApgarSCoreId: apgarscoreThree[0].itemId, SCore: this.babyNotifyData[i][j]['apgarScoreTen'],
                            ApgarScoreType: 'Apgar Score 10 min'
                        }
                    ]
                });
            }
        }

        const babyConditionInfo = {
            DeliveredBabyBirthInfoCollection: this.DeliveredBabyBirthInfoCollection
        };

        const vitaminA = this.drugAdminOptions.filter(x => x.itemName == 'Vitamin A Supplementation');
        const haartAnc = this.drugAdminOptions.filter(x => x.itemName == 'Started HAART in ANC');
        const maternityArv = this.drugAdminOptions.filter(x => x.itemName == 'ARVs Started in Maternity');
        const infantArv = this.drugAdminOptions.filter(x => x.itemName == 'Infant Provided With ARV prophylaxis');
        const cotrimoxazole = this.drugAdminOptions.filter(x => x.itemName == 'Cotrimoxazole');

        this.AdministeredDrugs.push(
            {
                Id: vitaminA[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['VitaminASupplementation'],
                Description: 'Vitamin A Supplementation'
            },
            {
                Id: haartAnc[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['StartedHAARTinANC'],
                Description: 'Started HAART in ANC'
            },
            {
                Id: cotrimoxazole[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['Cotrimoxazole'],
                Description: 'Cotrimoxazole'
            },
            {
                Id: maternityArv[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['ARVsStartedinMaternity'],
                Description: 'ARVs Started in Maternity'
            },
            {
                Id: infantArv[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['InfantProvidedWithARVprophylaxis'],
                Description: 'Infant Provided With ARV prophylaxis'
            }
        );

        const drugAdministrationCommand: DrugAdministrationCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreatedBy: this.userId,
            AdministeredDrugs: this.AdministeredDrugs
        };


        const partnerTestingCommand: PartnerTestingCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            PartnerTested: this.maternalDrugAdministrationForGroup.value[1]['partnerHivTestDone'],
            PartnerHIVResult: this.maternalDrugAdministrationForGroup.value[1]['finalPartnerHivResult'],
            CreateDate: new Date(),
            CreatedBy: this.userId,
            DeleteFlag: false,
            AuditData: ''
        };


        const infantFeeding = this.counsellingOptions.filter(x => x.itemName == 'Infant Feeding');

        const patiendEducationCommand: MaternityCounsellingCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CounsellingTopicId: infantFeeding[0].itemId,
            IsCounsellingDone: this.maternalDrugAdministrationForGroup.value[2]['counselledInfantFeeding'],
            CounsellingDate: new Date(),
            Description: null,
            CreatedBy: this.userId
        };

        const dischargeCommand: DischargeCommand = {
            PatientMasterVisitId: this.patientMasterVisitId,
            OutcomeDescription: 'na',
            OutcomeStatus: this.dischargeFormGroup.value[0]['babyStatus'],
            DateDischarged: moment(this.dischargeFormGroup.value[0]['dischargeDate']).toDate(),
            CreatedBy: this.userId
        };

        const referralCommand: ReferralCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.diagnosisFormGroup.value[1]['referredFrom'],
            ReferredTo: this.diagnosisFormGroup.value[1]['referredTo'],
            ReferralReason: '',
            ReferralDate: new Date(),
            ReferredBy: this.userId,
            DeleteFlag: false,
            CreatedBy: this.userId
        };

        const pncReferralCommand: PatientReferralCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.dischargeFormGroup.value[1]['referredFrom'],
            ReferredTo: this.dischargeFormGroup.value[1]['referredTo'],
            ReferralReason: 'Referral',
            ReferralDate: new Date(),
            ReferredBy: this.userId,
            DeleteFlag: 0,
            CreatedBy: this.userId
        };


        const nextAppointmentCommand: NextAppointmentCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            AppointmentDate: this.dischargeFormGroup.value[2]['nextAppointmentDate']
                ? moment(this.dischargeFormGroup.value[2]['nextAppointmentDate']).toDate() : null,
            Description: this.dischargeFormGroup.value[2]['remarks'],
            StatusDate: new Date(),
            DifferentiatedCareId: 0,
            AppointmentReason: 'Follow up',
            CreatedBy: this.userId

        };

        const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
        const naOption = this.yesNoNaOptions.filter(obj => obj.itemName == 'N/A');


        const hivStatusCommand: HivStatusCommand = {
            PersonId: this.personId,
            ProviderId: this.userId,
            PatientEncounterID: this.patientEncounterId ? this.patientEncounterId : 0,
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
            EncounterType: this.maternityTestsFormGroup.value[0]['testType']
        };

        const hivTestsCommand: HivTestsCommand = {
            HtsEncounterId: 0,
            ProviderId: this.userId,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            Testing: [],
            FinalTestingResult: {
                FinalResultHiv1: this.maternityTestsFormGroup.value[0]['finalTestResult'],
                FinalResultHiv2: null,
                FinalResult: this.maternityTestsFormGroup.value[0]['finalTestResult'],
                FinalResultGiven: yesOption[0].itemId,
                CoupleDiscordant: naOption[0].itemId,
                FinalResultsRemarks: 'n/a',
                AcceptedPartnerListing: yesOption[0].itemId,
                ReasonsDeclinePartnerListing: null
            }
        };

        for (let i = 0; i < this.hiv_status_table_data.length; i++) {
            console.log(this.hiv_status_table_data);
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

        const motherProfileCommand: MotherProfileCommand = {
            PatientPregnancy: pregnancyCommand
        };

        const matDiagnosis = this.matService.saveDiagnosis(diagnosisCommand);
        const matDrugAdministartion = this.matService.saveMaternalDrugAdministration(drugAdministrationCommand);
        const matEducation = this.matService.savePatientEducation(patiendEducationCommand);
        const matDischarge = this.matService.saveDischarge(dischargeCommand);
        const matReferral = this.matService.saveReferrals(pncReferralCommand);
        const matNextAppointment = this.matService.saveNextAppointment(nextAppointmentCommand);
        const matPartnerTesting = this.matService.savePartnerTesting(partnerTestingCommand);
        const matHivStatus = this.matService.savePncHivStatus(hivStatusCommand, this.hiv_status_table_data);

        forkJoin([
            matDiagnosis,
            matHivStatus,
            matDrugAdministartion,
            matEducation,
            matPartnerTesting,
            matDischarge,
            matReferral,
            matNextAppointment
        ])
            .subscribe(
                (result) => {
                    console.log(`success `);
                    console.log(result);

                    hivTestsCommand.HtsEncounterId = result[1]['htsEncounterId'];
                    hivTestsCommand.PatientMasterVisitId = result[1]['patientMasterVisitId'];
                    const ancHivResultsCommand = this.ancservice.saveHivResults(hivTestsCommand).subscribe(
                        (testRes) => {
                            console.log(testRes);
                        }
                    );

                    if (this.pregnancyId == 0) {
                        this.matService.savePregnancyProfile(pregnancyCommand).subscribe((res) => {
                            maternityDeliveryCommand.PregnancyId = res.pregnancyId;
                            visitDetailsCommand.PregnancyId = res.pregnancyId;

                            this.sendPatientDeliveryInfoRequest(maternityDeliveryCommand, babyConditionInfo);
                            this.sendVisitDetailsRequest(visitDetailsCommand);

                            this.zone.run(() => {
                                this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                            });
                        },
                            (err) => {
                                console.log('An error occured while add patient pregnancy details', err);
                            }, () => {

                            });

                    } else {
                        this.sendPatientDeliveryInfoRequest(maternityDeliveryCommand, babyConditionInfo);
                        this.sendVisitDetailsRequest(visitDetailsCommand);

                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                        });
                    }
                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {
                    console.log(`complete`);
                    // this.zone.run(() => {
                    //     this.router.navigate(['/dashboard/personhome/' + this.personId], {relativeTo: this.route});
                    // });
                }
            );

    }

    private sendPatientDeliveryInfoRequest(deliveryCommand: MaternityDeliveryCommand, babyConditionInfo: any) {
        this.matService.savePatientDelivery(deliveryCommand).subscribe(
            (res) => {

                this.deliveryId = res['patientDeliveryId'];
                console.log('patient DeliveryID:' + this.deliveryId);
                for (let i = 0; i < babyConditionInfo.DeliveredBabyBirthInfoCollection.length; i++) {
                    babyConditionInfo.DeliveredBabyBirthInfoCollection[i].PatientDeliveryInformationId = this.deliveryId;
                }
                console.log(`result`, res);
                console.log(babyConditionInfo);
                const matBabyCondition = this.matService.saveBabySection(babyConditionInfo).subscribe(
                    (re) => {
                        console.log(`Baby Delivery Information`);
                        console.log(res);
                    },
                    (er) => {
                        console.log(`error ` + er);
                    },
                    () => {
                        this.snotifyService.success('Successfully saved Maternity encounter ', 'Maternity',
                            this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                        });
                    }
                );
            }
        );
    }

    private sendVisitDetailsRequest(visitDetailsCommand: VisitDetailsCommand) {
        this.matService.saveVisitDetails(visitDetailsCommand).subscribe((result) => {
            console.log('Patient visit details added succesfully');
        },
            (err) => {
                console.log('An error occured while adding patient visit details', err);

            }, () => {

            });
    }

    public AddNewBabyInfo() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
            babySectionOptions: this.babySectionOptions,
            patientId: this.patientId,
            patientMasterVisitId: this.patientMasterVisitId,
            isNew: true
        };

        const dialogRef = this.dialog.open(AddBabyDialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data)
                    return;
                console.log(data);
            });
    }
}





