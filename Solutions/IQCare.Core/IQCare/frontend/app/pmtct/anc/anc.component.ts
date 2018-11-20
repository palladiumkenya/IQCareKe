import {Component, OnInit, OnDestroy, NgZone} from '@angular/core';
import {NotificationService} from './../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';
import {ActivatedRoute, Router} from '@angular/router';
import {VisitDetailsService} from '../_services/visit-details.service';
import {PatientEducationCommand} from '../_models/PatientEducationCommand';
import {PreventiveService} from '../_models/PreventiveService';
import {forkJoin, Subscription} from 'rxjs/index';
import {ClientMonitoringCommand} from '../_models/ClientMonitoringCommand';
import {AncService} from '../_services/anc.service';
import {HaartProphylaxisCommand} from '../_models/HaartProphylaxisCommand';
import {PatientDrugAdministration} from '../_models/PatientDrugAdministration';
import {PatientReferral} from '../_models/PatientReferral';
import {PatientPreventiveService} from '../_models/PatientPreventiveService';
import {PatientProfile} from '../_models/patientProfile';
import {PregnancyViewModel} from '../_models/viewModel/PregnancyViewModel';
import {FormArray, FormGroup} from '@angular/forms';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {PatientAppointment} from '../_models/PatientAppointmet';
import {PatientEducation} from '../_models/PatientEducation';
import {PatientChronicIllness} from '../_models/PatientChronicIllness';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {HivStatusCommand} from '../_models/HivStatusCommand';
import {HivTestsCommand} from '../_models/HivTestsCommand';
import {AdministerDrugInfo} from '../maternity/commands/administer-drug-info';

@Component({
    selector: 'app-anc',
    templateUrl: './anc.component.html',
    styleUrls: ['./anc.component.css']
})
export class AncComponent implements OnInit, OnDestroy {

    formType: string;
    visitType: number;
    isLinear: boolean = false;
    public isEdit = false;
    patientDrug: PatientDrugAdministration[] = [];
    public preventiveService: PreventiveService[] = [];
    public counselling_data_form: PatientEducation[] = [];
    public chronic_illness_data: PatientChronicIllness[] = [];
    AdministredDrugs: AdministerDrugInfo[] = [];
    counselling_data: any[] = [];
    chronicIllnessData: any[] = [];
    lookupItems$: Subscription;
    drugOptions: LookupItemView[] = [];


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
                } else {
                    this.visitId = this.patientMasterVisitId;
                    this.isEdit = true;
                }
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
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

    onNextClick() {
        console.log(this.visitDetailsFormGroup.value);
        return;
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
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

    }

    onReferralNotify(formGroup: FormGroup): void {
        this.ReferralMatFormGroup.push(formGroup);
    }

    public getPatientPregnanc(patientId: number) {
        this.getPatientPregnancy$ = this.visitDetailsService.getPregnancyProfile(this.patientId)
            .subscribe(
                p => {
                    this.pregnancy = p;
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
                        objOptions.push({'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
                    }
                },
                (err) => {
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItems$);
                });
    }

    public onSubmit() {

        const ancVisitDetailsCommand: any = {
            PatientId: parseInt(this.patientId.toString(), 10),
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: parseInt(this.serviceAreaId.toString(), 10),
            VisitDate: this.visitDetailsFormGroup.value[0]['visitDate'],
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

        for (let i = 0; i < this.counselling_data.length; i++) {

            this.counselling_data_form.push({
                CounsellingTopic: this.counselling_data[i]['counsellingTopic'],
                CounsellingTopicId: this.counselling_data[i]['counsellingTopicId'],
                CounsellingDate: this.counselling_data[i]['counsellingDate'],
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

        const clientMonitoringCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            FacilityId: 755,
            WhoStage: this.ClientMonitoringMatFormGroup.value[0]['WhoStage'],
            ServiceAreaId: 3,
            ScreeningTypeId: 0,
            ScreeningDone: (yesOption[0].itemId == screeningDone) ? true : false,
            ScreeningDate: new Date(),
            ScreeningTB: this.ClientMonitoringMatFormGroup.value[0]['screenedForTB'],
            CaCxMethod: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxMethod'] : 0,
            CaCxResult: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxResult'] : 0,
            Comments: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : '',
            ClinicalNotes: (yesOption[0].itemId == screeningDone) ? this.ClientMonitoringMatFormGroup.value[0]['cacxComments'] : '',
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

        /*
                this.patientDrug.push(
                    {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DrugAdministered: ARVFirstVisit[0].itemId,
                        Value: this.HaartProphylaxisMatFormGroup.value[0]['onArvBeforeANCVisit'], DeleteFlag: 0, Description: '',
                        Id: 0, CreatedBy: this.userId
                    },
                    {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DrugAdministered: HaartAnc[0].itemId,
                        Value: this.HaartProphylaxisMatFormGroup.value[0]['startedHaartANC'], DeleteFlag: 0, Description: '',
                        Id: 0, CreatedBy: this.userId
                    },
                    {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DrugAdministered: cotrimoxazole[0].itemId,
                        Value: this.HaartProphylaxisMatFormGroup.value[0]['cotrimoxazole'], DeleteFlag: 0, Description: '',
                        Id: 0, CreatedBy: this.userId
                    },
                    {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DrugAdministered: AZT[0].itemId,
                        Value: this.HaartProphylaxisMatFormGroup.value[0]['aztFortheBaby'], DeleteFlag: 0, Description: '',
                        Id: 0, CreatedBy: this.userId
                    },
                    {
                        PatientId: this.patientId,
                        PatientMasterVisitId: this.patientMasterVisitId,
                        DrugAdministered: NVP[0].itemId,
                        Value: this.HaartProphylaxisMatFormGroup.value[0]['nvpForBaby'], DeleteFlag: 0, Description: '',
                        Id: 0, CreatedBy: this.userId
                    }
                ); */

        for (let i = 0; i < this.chronicIllnessData.length; i++) {

            this.chronic_illness_data.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                ChronicIllness: this.chronicIllnessData[i]['chronicIllnessId'],
                Treatment: this.chronicIllnessData[i]['currentTreatment'],
                Dose: this.chronicIllnessData[i]['dose'],
                Duration: 0,
                DeleteFlag: false,
                OnsetDate: this.chronicIllnessData[i]['onSetDate'],
                Active: 0,
                CreateBy: this.userId
            });


            const drugAdministrationCommand: any = {
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                CreatedBy: this.userId,
                AdministredDrugs: this.AdministredDrugs
            };



            const chronicIllnessCommand = {
                'PatientChronicIllnesses': this.chronic_illness_data,
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
                        PreventiveServiceId: this.preventiServicesData[i]['preventiveServiceId'],
                        PreventiveServiceDate: this.preventiServicesData[i]['dateGiven'],
                        Description: this.preventiServicesData[i]['comments'],
                        NextSchedule: new Date(this.preventiServicesData[i]['nextSchedule'])
                    });
            }

            const preventiveServiceCommand: PatientPreventiveService = {
                preventiveService: this.preventiveService,
                InsecticideGivenDate: new Date(this.PreventiveServiceMatFormGroup.value[0]['insecticideTreatedNetGivenDate']),
                AntenatalExercise: this.PreventiveServiceMatFormGroup.value[0]['antenatalExercise'],
                PartnerTestingVisit: this.PreventiveServiceMatFormGroup.value[0]['PartnerTestingVisit'],
                FinalHIVResult: this.PreventiveServiceMatFormGroup.value[0]['finalHIVResult'],
                InsecticideTreatedNet: this.PreventiveServiceMatFormGroup.value[0]['insecticideTreatedNet'],
                CreatedBy: this.userId
            };

            console.log('refferalFormGroup');
            console.log(this.ReferralMatFormGroup);
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

            const appointmentCommand = {
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                AppointmentDate: new Date(this.ReferralMatFormGroup.value[0]['nextAppointmentDate']),
                Description: this.ReferralMatFormGroup.value[0]['serviceRemarks'],
                CreatedBy: this.userId,
                ServiceAreaId: this.serviceAreaId,
                StatusDate: new Date(),
                DifferentiatedCareId: 0,
                AppointmentReason: 'Follow Up'
            } as PatientAppointment;

            const ancVisitDetails = this.ancService.saveANCVisitDetails(ancVisitDetailsCommand);
            const ancEducation = this.ancService.savePatientEducation(patientEducationCommand);
            const ancHivStatus = this.ancService.saveAncHivStatus(hivStatusCommand, this.hiv_status_table_data);
            const ancClientMonitoring = this.ancService.saveClientMonitoring(clientMonitoringCommand);
           // const ancHaart = this.ancService.saveHaartProphylaxis(haartProphylaxisCommand);
             const drugAdministration = this.ancService.saveDrugAdministration(drugAdministrationCommand);
             const chronicIllness = this.ancService.savePatientChronicIllness(chronicIllnessCommand);
            const ancPreventiveService = this.ancService.savePreventiveServices(preventiveServiceCommand);
            const ancReferral = this.ancService.saveReferral(referralCommand);
            const ancAppointment = this.ancService.saveAppointment(appointmentCommand);


            forkJoin([
                ancVisitDetails,
                ancEducation,
                ancHivStatus,
                ancClientMonitoring,
                //  ancHaart,
                ancPreventiveService,
                ancReferral,
                ancAppointment

            ])
                .subscribe(
                    (result) => {
                        console.log(result);

                        this.snotifyService.success('Successfully saved ANC encounter ', 'ANC', this.notificationService.getConfig());

                        this.zone.run(() => {
                            this.router.navigate(['/dashboard/personhome/' + this.personId], {relativeTo: this.route});
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

    ngOnDestroy(): void {

    }
}
