import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { NotificationService } from './../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute, Router } from '@angular/router';
import { VisitDetailsService } from '../_services/visit-details.service';
import { PatientEducationCommand } from '../_models/PatientEducationCommand';
import { PreventiveService } from '../_models/PreventiveService';
import { forkJoin, Subscription } from 'rxjs/index';
import { ClientMonitoringCommand } from '../_models/ClientMonitoringCommand';
import { AncService } from '../_services/anc.service';
import { HaartProphylaxisCommand } from '../_models/HaartProphylaxisCommand';
import { PatientDrugAdministration } from '../_models/PatientDrugAdministration';
import { PatientReferral } from '../_models/PatientReferral';
import { PatientPreventiveService } from '../_models/PatientPreventiveService';
import { PatientProfile } from '../_models/patientProfile';
import { PregnancyViewModel } from '../_models/viewModel/PregnancyViewModel';
import { FormArray, FormGroup } from '@angular/forms';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PatientAppointment } from '../_models/PatientAppointmet';
import { PatientEducation } from '../_models/PatientEducation';
import { PatientChronicIllness } from '../_models/PatientChronicIllness';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { AdministeredDrugInfo } from '../maternity/commands/administer-drug-info';
import { VisitDetailsCommand } from '../_models/visit-details-command';
import { PregnancyAncCommand } from '../_models/pregnancy-anc-command';
import { HivTestsCommand } from '../_models/HivTestsCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { BaselineAncProfileCommand } from '../_models/baseline-anc-profile-command';
import { DrugAdministerCommand } from '../_models/drug-administer-command';
import * as moment from 'moment';
import { VisitDetailsEditCommand } from '../_models/VisitDetailsEditCommand';

@Component({
    selector: 'app-anc',
    templateUrl: './anc.component.html',
    styleUrls: ['./anc.component.css']
})
export class AncComponent implements OnInit, OnDestroy {

    formType: string;
    visitType: number;
    isLinear: boolean = true;
    public isEdit = false;
    patientDrug: PatientDrugAdministration[] = [];
    public preventiveService: PreventiveService[] = [];
    public counselling_data_form: PatientEducation[] = [];
    public chronic_illness_data: PatientChronicIllness[] = [];
    AdministredDrugs: AdministeredDrugInfo[] = [];
    counselling_data: any[] = [];
    chronicIllnessData: any[] = [];
    lookupItems$: Subscription;
    drugOptions: LookupItemView[] = [];

    /* Edit parameters */
    public visitDetailsId: number;


    public personId: number;
    public patientId: number;
    public serviceAreaId: number;
    public patientMasterVisitId: number;
    public patientEncounterId: number;
    public visitId: number;
    public userId: number;
    public visitDate: Date;
    locationId: number;
    hivTestEntryPoint: number;
    public pregnancyId: number;
    public appointmentCommand: any;

    visitDetailsOptions: any[] = [];
    patientEducationFormOptions: any[] = [];
    clientMonitoringOptions: any[] = [];
    haartProphylaxisOptions: any[] = [];
    serviceFormOptions: any[] = [];
    referralFormOptions: any[] = [];
    antenatalCareOptions: any[] = [];
    hivTestingOptions: any[] = [];
    hiv_status_table_data: any[] = [];

    yesNoOptions: LookupItemView[] = [];
    yesNoNaOptions: LookupItemView[] = [];
    referralOptions: LookupItemView[] = [];
    visitTypeOptions: LookupItemView[] = [];
    patientEducationOptions: LookupItemView[] = [];
    hivStatusOptions: LookupItemView[] = [];
    whoStageOptions: LookupItemView[] = [];
    chronicIllnessOptions: LookupItemView[] = [];
    preventiveServiceOptions: LookupItemView[] = [];
    tbScreeningOptions: LookupItemView[] = [];
    cacxMethodOptions: LookupItemView[] = [];
    cacxResultOptions: LookupItemView[] = [];
    hivFinalResultOptions: LookupItemView[] = [];
    ancHivStatusInitialVisitOptions: LookupItemView[] = [];
    hivFinalResultsOptions: LookupItemView[] = [];

    public getPatientPregnancy$: Subscription;
    public pregnancy: PregnancyViewModel = {};
    public profile: PatientProfile = {};
    preventiServicesData: any[] = [];

    public visitDetailsFormGroup: FormArray;
    public PatientEducationMatFormGroup: FormArray;
    public HivStatusMatFormGroup: FormArray;
    public ClientMonitoringMatFormGroup: FormArray;
    public HaartProphylaxisMatFormGroup: FormArray;
    public PreventiveServiceMatFormGroup: FormArray;
    public ReferralMatFormGroup: FormArray;


    constructor(private route: ActivatedRoute, private visitDetailsService: VisitDetailsService,
        private snotifyService: SnotifyService,
        private lookupItemService: LookupItemService,
        public zone: NgZone,
        private router: Router,
        private notificationService: NotificationService,
        private ancService: AncService) {
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.visitDetailsFormGroup = new FormArray([]);
        this.PatientEducationMatFormGroup = new FormArray([]);
        this.HivStatusMatFormGroup = new FormArray([]);
        this.ClientMonitoringMatFormGroup = new FormArray([]);
        this.HaartProphylaxisMatFormGroup = new FormArray([]);
        this.PreventiveServiceMatFormGroup = new FormArray([]);
        this.ReferralMatFormGroup = new FormArray([]);
        this.formType = 'anc';
    }

    ngOnInit() {

        this.route.params.subscribe(
            (params) => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                this.serviceAreaId = params.serviceAreaId;
                this.patientMasterVisitId = params.patientMasterVisitId;
                this.patientEncounterId = params.patientEncounterId;

                if (!this.patientMasterVisitId) {
                    this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
                    this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
                    this.isLinear = true;
                } else {
                    this.visitId = this.patientMasterVisitId;
                    this.isEdit = true;
                    this.isLinear = false;
                }
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.locationId = JSON.parse(localStorage.getItem('appLocationId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));
        this.getLookupItems('DrugAdministrationANC', this.drugOptions);
        this.lookupItemService.getByGroupNameAndItemName('HTSEntryPoints', 'PMTCT').subscribe(
            (res) => {
                this.hivTestEntryPoint = res['itemId'];
            }
        );

        this.route.data.subscribe((res) => {
            const {
                yesNoOptions,
                yesNoNaOptions,
                referralOptions,
                visitTypeOptions,
                patientEducationOptions,
                hivStatusOptions,
                whoStageOptions,
                chronicIllnessOptions,
                preventiveServiceOptions,
                tbScreeningOptions,
                cacxMethodOptions,
                cacxResultOptions,
                hivFinaResultOptions,
                ancHivStatusInitialVisitOptions,
                hivFinalResultsOptions
            } = res;
            this.yesNoNaOptions = yesNoNaOptions['lookupItems'];
            this.yesNoOptions = yesNoOptions['lookupItems'];
            this.referralOptions = referralOptions['lookupItems'];
            this.visitTypeOptions = visitTypeOptions['lookupItems'];
            this.patientEducationOptions = patientEducationOptions['lookupItems'];
            this.hivStatusOptions = hivStatusOptions['lookupItems'];
            this.whoStageOptions = whoStageOptions['lookupItems'];
            this.chronicIllnessOptions = chronicIllnessOptions['lookupItems'];
            this.preventiveServiceOptions = preventiveServiceOptions['lookupItems'];
            this.tbScreeningOptions = tbScreeningOptions['lookupItems'];
            this.cacxMethodOptions = cacxMethodOptions['lookupItems'];
            this.cacxResultOptions = cacxResultOptions['lookupItems'];
            this.hivFinalResultOptions = hivFinaResultOptions['lookupItems'];
            this.ancHivStatusInitialVisitOptions = ancHivStatusInitialVisitOptions['lookupItems'];
            this.hivFinalResultsOptions = hivFinalResultsOptions['lookupItems'];
        });

        this.hivTestingOptions.push({
            'ancHivStatusInitialVisitOptions': this.ancHivStatusInitialVisitOptions,
            'yesnoOptions': this.yesNoOptions,
            'hivFinalResultsOptions': this.hivFinalResultsOptions
        });

        this.antenatalCareOptions.push({
            'visitTypeOptions': this.visitTypeOptions,
            'patientEducationOptions': this.patientEducationOptions,
            'yesnoOptions': this.yesNoOptions,
            'yesNoNaOptions': this.yesNoNaOptions,
            'hivStatusOptions': this.hivStatusOptions,
            'whoStageOptions': this.whoStageOptions,
            'chronicIllnessOptions': this.chronicIllnessOptions,
            'preventiveServiceOptions': this.preventiveServiceOptions,
            'referralOptions': this.referralOptions
        });

        this.visitDetailsOptions.push({
            'visitTypeOptions': this.visitTypeOptions
        });

        this.patientEducationFormOptions.push({
            'yesnoOptions': this.yesNoOptions,
            'patientEducationOptions': this.patientEducationOptions,
            'hivStatusOptions': this.hivStatusOptions
        });

        this.clientMonitoringOptions.push({
            'whoStageOptions': this.whoStageOptions,
            'yesnoOptions': this.yesNoOptions,
            'yesNoNaOptions': this.yesNoNaOptions,
            'tbScreeningOptions': this.tbScreeningOptions,
            'cacxMethodOptions': this.cacxMethodOptions,
            'cacxResultOptions': this.cacxResultOptions
        });

        this.haartProphylaxisOptions.push({
            'yesnoOptions': this.yesNoOptions,
            'yesNoNaOptions': this.yesNoNaOptions,
            'chronicIllnessOptions': this.chronicIllnessOptions
        });

        this.serviceFormOptions.push({
            'yesNoNaOptions': this.yesNoNaOptions,
            'yesNoOptions': this.yesNoOptions,
            'preventiveServicesOptions': this.preventiveServiceOptions,
            'hivFinalResultOptions': this.hivFinalResultOptions
        });

        this.referralFormOptions.push({
            'referralOptions': this.referralOptions,
            'yesNoOptions': this.yesNoOptions
        });

    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
        this.getPatientPregnancy(this.patientId);
    }

    OnMotherProfileNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
     }

    onPatientEducationNotify(formGroup: Object): void {
        this.PatientEducationMatFormGroup = formGroup['form'];
        this.counselling_data = formGroup['counselling_data'];
    }

    onHivStatusNotify(formGroup: Object): void {
        this.HivStatusMatFormGroup.push(formGroup['form']); // = formGroup['form'];
        this.hiv_status_table_data.push(formGroup['table_data']);
    }

    onClientMonitoringNotify(formGroup: FormGroup): void {
        this.ClientMonitoringMatFormGroup.push(formGroup);
    }

    onHaartProphylaxisNotify(formGroup: Object): void {
        this.HaartProphylaxisMatFormGroup.push(formGroup['form']); // = formGroup;
        this.chronicIllnessData = formGroup['illness_data'];
    }

    onPreventiveServiceNotify(formGroup: Object): void {
        this.PreventiveServiceMatFormGroup.push(formGroup['form']); // = formGroup;
        this.preventiServicesData = formGroup['preventive_service_data'];
        console.log('preventive services');
        console.log(this.preventiServicesData);

    }

    onReferralNotify(formGroup: FormGroup): void {
        this.ReferralMatFormGroup.push(formGroup);
    }

    public getPatientPregnancy(patientId: number) {
        this.getPatientPregnancy$ = this.visitDetailsService.getPregnancyProfile(patientId)
            .subscribe(
                p => {
                    this.pregnancy = p;
                    const pregnancy = p;
                    if (pregnancy) {

                        console.log(pregnancy);
                        this.pregnancyId = pregnancy.id;
                        console.log('pregancyId:' + this.pregnancyId);
                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching pregnancy' + err, 'Pregnancy Profile', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.pregnancy);
                }
            );
    }

    public getLookupItems(groupName: string, objOptions: any[] = []) {
        this.lookupItems$ = this.lookupItemService.getByGroupName(groupName)
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
                    console.log(this.lookupItems$);
                });
    }

    public onSubmit(): void {

        if (this.isEdit) {
            this.onSubmitEdit();
        } else {
            this.onSubmitNew();
        }
    }

    public onSubmitNew() {


        const ancVisitDetailsCommand: any = {
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            Lmp: new Date(this.visitDetailsFormGroup.value[1]['dateLMP']),
            Edd: this.visitDetailsFormGroup.value[1]['dateEDD'],
            Gestation: this.visitDetailsFormGroup.value[1]['gestation'],
            AgeAtMenarche: this.visitDetailsFormGroup.value[1]['ageAtMenarche'],
            ParityOne: this.visitDetailsFormGroup.value[1]['parityOne'],
            ParityTwo: this.visitDetailsFormGroup.value[1]['parityTwo'],
            Gravidae: this.visitDetailsFormGroup.value[1]['gravidae'],
            UserId: this.userId,
            DaysPostPartum: 0
        };

        const pregnancyCommand = {
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: this.patientMasterVisitId,
            Lmp: moment(this.visitDetailsFormGroup.value[1]['dateLMP']).toDate(),
            Edd: moment(this.visitDetailsFormGroup.value[1]['dateEDD']).toDate(),
            Gestation: this.visitDetailsFormGroup.value[1]['gestation'],
            Gravidae: this.visitDetailsFormGroup.value[1]['gravidae'],
            Parity: this.visitDetailsFormGroup.value[1]['parityOne'],
            Parity2: this.visitDetailsFormGroup.value[1]['parityTwo'],
            CreatedBy: this.userId
        } as PregnancyAncCommand;

        const visitDetailsCommand = {
            PatientId: parseInt(this.patientId.toString(), 10),
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            PregnancyId: this.pregnancyId,
            PatientMasterVisitId: this.patientMasterVisitId,
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            DaysPostPartum: (this.formType == 'pnc') ? this.visitDetailsFormGroup.value[1]['DaysPostPartum'] : 0,
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            UserId: this.userId
        } as VisitDetailsCommand;

        for (let i = 0; i < this.counselling_data.length; i++) {

            this.counselling_data_form.push({
                CounsellingTopic: this.counselling_data[i]['counsellingTopic'],
                CounsellingTopicId: this.counselling_data[i]['counsellingTopicId'],
                CounsellingDate: moment(this.counselling_data[i]['counsellingDate']).toDate(),
                description: this.counselling_data[i]['description']
            });
            // console.log(this.counselling_data[i]['counsellingTopic']);
        }

        // console.log('patient education');
        // console.log(this.PatientEducationMatFormGroup);
        const patientEducationCommand: PatientEducationCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            BreastExamDone: this.PatientEducationMatFormGroup.value['breastExamDone'],
            TreatedSyphilis: this.PatientEducationMatFormGroup.value['treatedSyphilis'],
            CreateBy: this.userId,
            CounsellingTopics: this.counselling_data
        };

        const baselineAncCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            PregnancyId: this.pregnancyId,
            HivStatusBeforeAnc: this.HivStatusMatFormGroup.value[0]['hivStatusBeforeFirstVisit'],
            TreatedForSyphilis: this.PatientEducationMatFormGroup.value['treatedSyphilis'],
            BreastExamDone: this.PatientEducationMatFormGroup.value['breastExamDone'],
            CreatedBy: this.userId
        } as BaselineAncProfileCommand;

        const yesOption = this.yesNoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesNoOptions.filter(obj => obj.itemName == 'No');
        const naOption = this.yesNoNaOptions.filter(obj => obj.itemName == 'N/A');

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
            EncounterType: this.HivStatusMatFormGroup.value[0]['testType']
        };

        const hivTestsCommand: HivTestsCommand = {
            HtsEncounterId: 0,
            ProviderId: this.userId,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            Testing: [],
            FinalTestingResult: {
                FinalResultHiv1: this.HivStatusMatFormGroup.value[0]['finalTestResult'],
                FinalResultHiv2: null,
                FinalResult: this.HivStatusMatFormGroup.value[0]['finalTestResult'],
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
        const screeningDone = this.ClientMonitoringMatFormGroup.value[0]['cacxScreeningDone'];
        const viralLoadSampleTaken = this.ClientMonitoringMatFormGroup.value[0]['viralLoadSampleTaken'];

        const clientMonitoringCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            FacilityId: 755,
            WhoStage: this.ClientMonitoringMatFormGroup.value[0]['WhoStage'],
            ServiceAreaId: 3,
            ScreeningTypeId: 0,
            ScreeningDone: (yesOption[0].itemId == screeningDone) ? true : false,
            ScreeningDate: new Date(),
            ViralLoadSampleTaken: viralLoadSampleTaken, // (yesOption[0].itemId == viralLoadSampleTaken) ? true : false,
            ScreenedTB: this.ClientMonitoringMatFormGroup.value[0]['screenedForTB'],
            CaCxMethod: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxMethod'] : 0,
            CaCxResult: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxResult'] : 0,
            Comments: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : 'na',
            ClinicalNotes: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : 'n/a',
            CreatedBy: (this.userId < 1) ? 1 : this.userId
        } as ClientMonitoringCommand;

        const ARVFirstVisit = this.drugOptions.filter(obj => obj.itemName == 'On ARV before 1st ANC Visit');
        const HaartAnc = this.drugOptions.filter(obj => obj.itemName == 'Started HAART in ANC');
        const cotrimoxazole = this.drugOptions.filter(obj => obj.itemName == 'Cotrimoxazole');
        const AZT = this.drugOptions.filter(obj => obj.itemName == 'AZT for the baby dispensed');
        const NVP = this.drugOptions.filter(obj => obj.itemName == 'NVP for baby dispensed');

        this.AdministredDrugs.push(
            {
                Id: ARVFirstVisit[0].itemId, Value: this.HaartProphylaxisMatFormGroup.value[0]['onArvBeforeANCVisit'],
                Description: 'On ARV before 1st ANC Visit'
            },
            {
                Id: HaartAnc[0].itemId, Value: this.HaartProphylaxisMatFormGroup.value[0]['startedHaartANC'],
                Description: 'Started HAART in ANC'
            },
            {
                Id: cotrimoxazole[0].itemId, Value: this.HaartProphylaxisMatFormGroup.value[0]['cotrimoxazole'],
                Description: 'Cotrimoxazole given during this visit'
            },
            {
                Id: AZT[0].itemId, Value: this.HaartProphylaxisMatFormGroup.value[0]['aztFortheBaby'],
                Description: 'AZT for the baby dispensed'
            },
            {
                Id: NVP[0].itemId, Value: this.HaartProphylaxisMatFormGroup.value[0]['nvpForBaby'],
                Description: 'NVP for the baby dispensed'
            }
        );

        // console.log('administered drugs');
        // console.log(this.AdministredDrugs);

        const yesno = this.yesNoOptions.filter(x => x.itemName == 'Yes');
        const otherIllnessOption = this.HaartProphylaxisMatFormGroup.value[0]['otherIllness'];

        if (otherIllnessOption == yesno[0]['itemId']) {
            for (let i = 0; i < this.chronicIllnessData.length; i++) {

                this.chronic_illness_data.push({
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    ChronicIllness: this.chronicIllnessData[i]['chronicIllnessId'],
                    Treatment: this.chronicIllnessData[i]['currentTreatment'],
                    // Dose: this.chronicIllnessData[i]['dose'],
                    Dose: 0,
                    Duration: 0,
                    DeleteFlag: false,
                    OnsetDate: moment(this.chronicIllnessData[i]['onSetDate']).toDate(),
                    Active: 0,
                    CreateBy: this.userId
                });
            }
        } else {
            this.chronic_illness_data = [];
        }


        const drugAdministrationCommand: DrugAdministerCommand = {
            Id: 0,
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: parseInt(this.patientMasterVisitId.toString(), 10),
            CreatedBy: this.userId,
            AdministeredDrugs: this.AdministredDrugs
        };


        const chronicIllnessCommand = {
            PatientChronicIllnesses: this.chronic_illness_data,
        };

        const haartProphylaxisCommand = {
            PatientDrugAdministration: this.patientDrug,
            PatientChronicIllnesses: this.chronic_illness_data,
            OtherIllness: this.HaartProphylaxisMatFormGroup.value[0]['otherIllness']
        } as HaartProphylaxisCommand;


        for (let j = 0; j < this.preventiServicesData.length; j++) {
            this.preventiveService.push(
                {
                    Id: 0,
                    PatientId: this.patientId,
                    PatientMasterVisitId: this.patientMasterVisitId,
                    PreventiveServiceId: this.preventiServicesData[j]['preventiveServiceId'],
                    PreventiveServiceDate: this.preventiServicesData[j]['dateGiven'],
                    Description: this.preventiServicesData[j]['comments'],
                    NextSchedule: moment(this.preventiServicesData[j]['nextSchedule']).toDate()
                });
        }

        const InsecticideGivenDate: Date = moment(this.PreventiveServiceMatFormGroup.value[0]['insecticideTreatedNetGivenDate']).toDate();
        const InsecticideTreatedNet: number = this.PreventiveServiceMatFormGroup.value[0]['insecticideTreatedNet'];
        const preventiveServiceCommand: PatientPreventiveService = {
            preventiveService: this.preventiveService,
            AntenatalExercise: this.PreventiveServiceMatFormGroup.value[0]['antenatalExercise'],
            PartnerTestingVisit: this.PreventiveServiceMatFormGroup.value[0]['PartnerTestingVisit'],
            FinalHIVResult: this.PreventiveServiceMatFormGroup.value[0]['finalHIVResult'],
            InsecticideTreatedNet: InsecticideTreatedNet,
            InsecticideGivenDate: (yesno[0]['itemId'] == InsecticideTreatedNet) ? InsecticideGivenDate : null,
            CreatedBy: this.userId

        };

        // console.log('refferalFormGroup');
        // console.log(this.ReferralMatFormGroup);
        const referralCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.ReferralMatFormGroup.value[0]['referredFrom'],
            ReferredTo: this.ReferralMatFormGroup.value[0]['referredTo'],
            ReferralReason: 'n/a',
            ReferralDate: new Date(),
            RefferedBY: this.userId,
            DeleteFlag: 0,
            CreateBy: this.userId
        } as PatientReferral;

        const appointmentId = this.ReferralMatFormGroup.value[0]['scheduledAppointment'];
        const yes = this.yesNoNaOptions.filter(x => x.itemName == 'Yes');
        if (appointmentId == yes[0]['itemId']) {
            this.appointmentCommand = {
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                AppointmentDate: moment(this.ReferralMatFormGroup.value[0]['nextAppointmentDate']).toDate(),
                Description: this.ReferralMatFormGroup.value[0]['serviceRemarks'],
                CreatedBy: this.userId,
                ServiceAreaId: this.serviceAreaId,
                StatusDate: new Date(),
                DifferentiatedCareId: 0,
                AppointmentReason: 'Follow Up'
            } as PatientAppointment;
        } else {
            this.appointmentCommand = {
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                AppointmentDate: moment(this.ReferralMatFormGroup.value[0]['nextAppointmentDate']).toDate(),
                Description: this.ReferralMatFormGroup.value[0]['serviceRemarks'],
                CreatedBy: this.userId,
                ServiceAreaId: this.serviceAreaId,
                StatusDate: new Date(),
                DifferentiatedCareId: 0,
                AppointmentReason: 'None'
            } as PatientAppointment;
        }


        // const AncPregnancy = this.ancService.savePregnancy(pregnancyCommand);
        // const ancVisitDetails = this.ancService.saveANCVisitDetails(ancVisitDetailsCommand);
        const visitDetails = this.ancService.saveVisitDetails(visitDetailsCommand);
        const baseline = this.ancService.SaveBaselineProfile(baselineAncCommand);
        const ancEducation = this.ancService.savePatientEducation(patientEducationCommand);
        const ancHivStatus = this.ancService.saveAncHivStatus(hivStatusCommand, this.hiv_status_table_data);
        const ancClientMonitoring = this.ancService.saveClientMonitoring(clientMonitoringCommand);
        const ancHaart = this.ancService.saveHaartProphylaxis(haartProphylaxisCommand);
        const drugAdministration = this.ancService.saveDrugAdministration(drugAdministrationCommand);
        const chronicIllness = this.ancService.savePatientChronicIllness(chronicIllnessCommand);
        const ancPreventiveService = this.ancService.savePreventiveServices(preventiveServiceCommand);
        const ancReferral = this.ancService.saveReferral(referralCommand);
        const ancAppointment = this.ancService.saveAppointment(this.appointmentCommand);

        if (this.pregnancyId < 1) {
            const AncPregnancy = this.ancService.savePregnancy(pregnancyCommand).subscribe(
                (res) => {
                    console.log(`pregancyId new` + res);
                    this.pregnancyId = res.pregnancyId;
                    console.log(res.pregnancyId);

                    const visitDetailsCommands = {
                        PatientId: parseInt(this.patientId.toString(), 10),
                        ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
                        PregnancyId: this.pregnancyId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        VisitDate: new Date(this.visitDetailsFormGroup.value[0]['visitDate']),
                        VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
                        DaysPostPartum: (this.formType == 'pnc') ? this.visitDetailsFormGroup.value[1]['DaysPostPartum'] : 0,
                        VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
                        UserId: this.userId

                    } as VisitDetailsCommand;

                    const baselineAncCommands = {
                        PatientId: parseInt(this.patientId.toString(), 10),
                        PatientMasterVisitId: this.patientMasterVisitId,
                        PregnancyId: res.pregnancyId,
                        HivStatusBeforeAnc: this.HivStatusMatFormGroup.value[0]['hivStatusBeforeFirstVisit'],
                        TreatedForSyphilis: this.PatientEducationMatFormGroup.value['treatedSyphilis'],
                        BreastExamDone: this.PatientEducationMatFormGroup.value['breastExamDone'],
                        CreatedBy: this.userId
                    } as BaselineAncProfileCommand;

                    const ancVisitDetail = this.ancService.saveVisitDetails(visitDetailsCommands);
                    const baselineAnc = this.ancService.SaveBaselineProfile(baselineAncCommands);

                    forkJoin([
                        ancVisitDetail,
                        baselineAnc,
                        ancEducation,
                        ancHivStatus,
                        ancClientMonitoring,
                        ancHaart,
                        drugAdministration,
                        chronicIllness,
                        ancPreventiveService,
                        ancReferral,
                        ancAppointment
                    ])
                        .subscribe(
                            (result) => {
                                hivTestsCommand.HtsEncounterId = result[3]['htsEncounterId'];
                                hivTestsCommand.PatientMasterVisitId = result[3]['patientMasterVisitId'];
                                const ancHivResultsCommand = this.ancService.saveHivResults(hivTestsCommand).subscribe(
                                    (testRes) => {
                                        console.log(testRes);
                                    }
                                );
                                // console.log(result);
                                this.snotifyService.success('Successfully saved ANC encounter ', 'ANC',
                                    this.notificationService.getConfig());
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
            );
        } else {
            // spinner
            forkJoin([
                visitDetails,
                baseline,
                ancEducation,
                ancHivStatus,
                ancClientMonitoring,
                ancHaart,
                drugAdministration,
                chronicIllness,
                ancPreventiveService,
                ancReferral,
                ancAppointment
            ])
                .subscribe(
                    (result) => {
                        console.log(result);
                        hivTestsCommand.HtsEncounterId = result[3]['htsEncounterId'];
                        hivTestsCommand.PatientMasterVisitId = result[3]['patientMasterVisitId'];
                        const ancHivResultsCommand = this.ancService.saveHivResults(hivTestsCommand).subscribe(
                            (testRes) => {
                                console.log(testRes);
                            }
                        );
                        this.snotifyService.success('Successfully saved ANC encounter ', 'ANC',
                            this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                        });
                    },
                    (error) => {
                        console.log(`error ` + error);
                    },
                    () => {
                        // close spinner
                        console.log(`complete`);
                    }
                );
        }
    }

    public onSubmitEdit(): void {

        const yesOption = this.yesNoOptions.filter(obj => obj.itemName == 'Yes');
        const noOption = this.yesNoOptions.filter(obj => obj.itemName == 'No');
        const naOption = this.yesNoNaOptions.filter(obj => obj.itemName == 'N/A');
        const screeningDone = this.ClientMonitoringMatFormGroup.value[0]['cacxScreeningDone'];
        const viralLoadSampleTaken = this.ClientMonitoringMatFormGroup.value[0]['viralLoadSampleTaken'];

        const ancVisitDetailsCommandEdit: any = {
            Id: this.pregnancyId,
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: this.visitId,
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            VisitDate: moment(this.visitDetailsFormGroup.value[0]['visitDate']).toDate(),
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            Lmp: new Date(this.visitDetailsFormGroup.value[1]['dateLMP']),
            Edd: this.visitDetailsFormGroup.value[1]['dateEDD'],
            Gestation: this.visitDetailsFormGroup.value[1]['gestation'],
            AgeAtMenarche: this.visitDetailsFormGroup.value[1]['ageAtMenarche'],
            ParityOne: this.visitDetailsFormGroup.value[1]['parityOne'],
            ParityTwo: this.visitDetailsFormGroup.value[1]['parityTwo'],
            Gravidae: this.visitDetailsFormGroup.value[1]['gravidae'],
            UserId: this.userId,
            DaysPostPartum: 0
        };

        const visitDetailsEditCommand: any = {
            Id: this.visitDetailsFormGroup.value[0]['id'],
            VisitNumber: parseInt(this.visitDetailsFormGroup.value[0]['visitNumber'], 10),
            VisitType: this.visitDetailsFormGroup.value[0]['visitType'],
            DaysPostPartum: 0
        };

        const VisitDetails = { VisitDetails: visitDetailsEditCommand };

        const baselineAncCommandEdit = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            PregnancyId: this.pregnancyId,
            HivStatusBeforeAnc: this.HivStatusMatFormGroup.value[0]['hivStatusBeforeFirstVisit'],
            TreatedForSyphilis: this.PatientEducationMatFormGroup.value['treatedSyphilis'],
            BreastExamDone: this.PatientEducationMatFormGroup.value['breastExamDone'],
            CreatedBy: this.userId
        } as BaselineAncProfileCommand;

        for (let i = 0; i < this.counselling_data.length; i++) {

            this.counselling_data_form.push({
                CounsellingTopic: this.counselling_data[i]['counsellingTopic'],
                CounsellingTopicId: this.counselling_data[i]['counsellingTopicId'],
                CounsellingDate: moment(this.counselling_data[i]['counsellingDate']).toDate(),
                description: this.counselling_data[i]['description']
            });
            console.log(this.counselling_data[i]['counsellingTopic']);
        }

        console.log('patient education');
        console.log(this.PatientEducationMatFormGroup);
        const patientEducationCommand: PatientEducationCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            BreastExamDone: this.PatientEducationMatFormGroup.value['breastExamDone'],
            TreatedSyphilis: this.PatientEducationMatFormGroup.value['treatedSyphilis'],
            CreateBy: this.userId,
            CounsellingTopics: this.counselling_data
        };

        const clientMonitoringCommandEdit: any = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ViralLoadSampleTaken: (viralLoadSampleTaken < 1 ) ? 0 : viralLoadSampleTaken,
            WhoStage: this.ClientMonitoringMatFormGroup.value[0]['WhoStage'],
            FacilityId: 755,
            ServiceAreaId: 3,
            ClinicalNotes: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : 'n/a',
            ScreeningTypeId: 0,
            ScreeningDone: (yesOption[0].itemId == screeningDone) ? true : false,
            ScreeningDate: new Date(),
            ScreenedTB: this.ClientMonitoringMatFormGroup.value[0]['screenedForTB'],
            CaCxMethod: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxMethod'] : 0,
            CaCxResult: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxResult'] : 0,
            Comments: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : 'na',
            CreatedBy: (this.userId < 1) ? 1 : this.userId
        };

        const appointmentId = this.ReferralMatFormGroup.value[0]['scheduledAppointment'];
        const yes = this.yesNoNaOptions.filter(x => x.itemName == 'Yes');
        if (appointmentId == yes[0]['itemId']) {
            this.appointmentCommand = {
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                AppointmentDate: moment(this.ReferralMatFormGroup.value[0]['nextAppointmentDate']).toDate(),
                Description: this.ReferralMatFormGroup.value[0]['serviceRemarks'],
                CreatedBy: this.userId,
                ServiceAreaId: this.serviceAreaId,
                StatusDate: new Date(),
                DifferentiatedCareId: 0,
                AppointmentReason: 'Follow Up'
            } as PatientAppointment;
        } else {
            this.appointmentCommand = {
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                AppointmentDate: moment(this.ReferralMatFormGroup.value[0]['nextAppointmentDate']).toDate(),
                Description: this.ReferralMatFormGroup.value[0]['serviceRemarks'],
                CreatedBy: this.userId,
                ServiceAreaId: this.serviceAreaId,
                StatusDate: new Date(),
                DifferentiatedCareId: 0,
                AppointmentReason: 'None'
            } as PatientAppointment;
        }

        const referralEditCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.ReferralMatFormGroup.value[0]['referredFrom'],
            ReferredTo: this.ReferralMatFormGroup.value[0]['referredTo'],
            ReferralReason: 'n/a',
            ReferralDate: new Date(),
            RefferedBY: this.userId,
            DeleteFlag: 0,
            CreateBy: this.userId
        } as PatientReferral;

     //   const clientMonitoringEditCommand = { clientMonitoringEditCommand: clientMonitoringCommandEdit }

        console.log('client monitoring command edit');
        console.log(clientMonitoringCommandEdit);
        const AncvisitDetailsEdit = this.ancService.EditANCVisitDetails(ancVisitDetailsCommandEdit);
        const visitDetailsEdit = this.ancService.EditVisitDetails(VisitDetails);
        const baselineEdit = this.ancService.EditBaselineProfile(baselineAncCommandEdit);
        const ancEducation = this.ancService.savePatientEducation(patientEducationCommand);
        const ancClientMonitoringEdit = this.ancService.EditClientMonitoring(clientMonitoringCommandEdit);

        const PatientAppointmentEdit = this.ancService.EditAppointment(this.appointmentCommand);
        const referralEdit = this.ancService.EditReferral(referralEditCommand);

        forkJoin([
            AncvisitDetailsEdit,
            visitDetailsEdit,
            baselineEdit,
            ancEducation,
            ancClientMonitoringEdit
            // PatientAppointmentEdit,
           // referralEdit

        ]).subscribe(
            (result) => {
                console.log(result);
                this.snotifyService.success('Successfully Edited ANC encounter ', 'ANC',
                    this.notificationService.getConfig());
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
                });
            },
            (error) => {

            },
            () => { }
        );
    }

    ngOnDestroy(): void {
        this.getPatientPregnancy$.unsubscribe();
    }
}
