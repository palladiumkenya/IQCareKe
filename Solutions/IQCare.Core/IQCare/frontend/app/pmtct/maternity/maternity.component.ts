import {Component, OnInit} from '@angular/core';
import {FormArray, FormGroup} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {MaternityVisitDetailsCommand} from './commands/maternity-visit-details-command';
import {MaternityService} from '../_services/maternity.service';
import {PregnancyCommand} from './commands/pregnancy-command';
import {MaternityDeliveryCommand} from './commands/maternity-delivery-command';
import {BabyConditionCommand} from './commands/baby-condition-command';
import {ApgarScoreCommand} from './commands/apgar-score-command';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {forkJoin, Subscription} from 'rxjs/index';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {DrugAdministrationCommand} from './commands/drug-administration-command';
import {AdministerDrugInfo} from './commands/administer-drug-info';
import {MaternityCounsellingCommand} from './commands/maternity-counselling-command';
import {ReferralCommand} from './commands/referral-command';
import {NextAppointmentCommand} from './commands/next-appointment-command';
import {DischargeCommand} from './commands/discharge-command';
import {DiagnosisCommand} from './commands/diagnosis-command';
import {MotherProfileCommand} from './commands/mother-profile-command';


@Component({
    selector: 'app-maternity',
    templateUrl: './maternity.component.html',
    styleUrls: ['./maternity.component.css']
})
export class MaternityComponent implements OnInit {
    isLinear: boolean = false;

    visitDetailsFormGroup: FormArray;
    diagnosisFormGroup: FormArray;
    babyFormGroup: FormArray;
    maternityTestsFormGroup: FormArray;
    maternalDrugAdministrationForGroup: FormArray;
    dischargeFormGroup: FormArray;
    formType: string;
    apgarSCore: ApgarScoreCommand[] = [];
    AdministredDrugs: AdministerDrugInfo[] = [];
    lookupItems$: Subscription;
    apgarOptions: any[] = [];
    drugAdminOptions: any[] = [];
    counsellingOptions: any[] = [];
    babyNotifyData: any[] = [];
    DeliveredBabyBirthInfoCollection: any[] = [];

    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    patientEncounterId: number;
    visitDate: Date;
    visitType: number;
    pregnancyId: number;
    deliveryId: number;


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

    birthInformationCollection: any[] = [];

    constructor(private route: ActivatedRoute,
                private matService: MaternityService,
                private _lookupItemService: LookupItemService,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
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
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const {patientId, personId, serviceAreaId} = params;
                this.patientId = parseInt(patientId, 10);
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

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
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

    onBabyNotfiyData(babyNotifyData: any[]): void {
        this.babyNotifyData.push(babyNotifyData);
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

    onSubmit() {


        const visitDetailsCommand: MaternityVisitDetailsCommand = {
            patientId: this.patientId,
            patientMasterVisitId: this.patientMasterVisitId,
            ageAtMenarche: 0,
            pregnancyId: 0,
            visitNumber: 0,
            visitType: 0,
            treatedForSyphilis: 0,
            deleteFlag: false,
            createDate: new Date(),
            createdBy: this.userId,
            postpartum: 'na'
        };

        console.log(this.visitDetailsFormGroup);
        const pregnancyCommand: PregnancyCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Outcome: 0,
            DateOfOutcome: null,
            Lmp: this.visitDetailsFormGroup.value[1]['dateLMP'],
            Edd: new Date(this.visitDetailsFormGroup.value[1]['dateEDD']),
            Gestation: this.visitDetailsFormGroup.value[1]['gestation'],
            Gravidae: parseInt(this.visitDetailsFormGroup.value[1]['gravidae'], 10),
            Parity: this.visitDetailsFormGroup.value[1]['parityOne'],
            Parity2: this.visitDetailsFormGroup.value[1]['parityTwo'],
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
            ProfileId: 3,
            DurationOfLabour: this.diagnosisFormGroup.value[1]['labourDuration'],
            DateOfDelivery: this.diagnosisFormGroup.value[1]['deliveryDate'],
            TimeOfDelivery: this.diagnosisFormGroup.value[1]['deliveryTime'],
            ModeOfDelivery: this.diagnosisFormGroup.value[1]['deliveryMode'],
            PlacentaComplete: this.diagnosisFormGroup.value[1]['placentaComplete'],
            BloodLossCapacity: parseInt(this.diagnosisFormGroup.value[1]['bloodLossCount'], 10),
            BloodLossClassification: this.diagnosisFormGroup.value[1]['bloodLoss'],
            MotherCondition: this.diagnosisFormGroup.value[1]['deliveryCondition'],
            MaternalDeathAudited: this.diagnosisFormGroup.value[1]['maternalDeathsAudited'],
            MaternalDeathAuditDate: this.diagnosisFormGroup.value[1]['auditDate'],
            DeliveryComplicationsExperienced: this.diagnosisFormGroup.value[1]['deliveryComplications'],
            DeliveryComplicationNotes: this.diagnosisFormGroup.value[1]['deliveryComplicationNotes'],
            DeliveryConductedBy: this.diagnosisFormGroup.value[1]['deliveryConductedBy'],
            CreatedBy: this.userId
        };

        const apgarscoreOne = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 1 min');
        const apgarscoreTwo = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 5 min');
        const apgarscoreThree = this.apgarOptions.filter(x => x.itemName == 'Apgar Score 10 min');

        this.apgarSCore.push(
            {
                ApgarSCoreId: apgarscoreOne[0].itemId, ApgarScoreType: 'Apgar Score 1 min',
                SCore: this.babyFormGroup.value[0]['agparScore1min']
            },
            {
                ApgarSCoreId: apgarscoreTwo[0].itemId, ApgarScoreType: 'Apgar Score 5 min',
                SCore: this.babyFormGroup.value[0]['agparScore5min']
            },
            {
                ApgarSCoreId: apgarscoreThree[0].itemId, ApgarScoreType: 'Apgar Score 10 min',
                SCore: this.babyFormGroup.value[0]['agparScore10min']
            }
        );

        console.log('baby data' + this.babyNotifyData.length[0]);
        console.log(this.babyNotifyData);

        for (let i = 0; i < this.babyNotifyData.length; i++) { for (let j = 0; j < this.babyNotifyData.length; j++) {
            this.DeliveredBabyBirthInfoCollection.push({

                    PatientDeliveryInformationId: 5,
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
        }}

       // this.birthInformationCollection.push('DeliveredBabyBirthInfoCollection': this.DeliveredBabyBirthInfoCollection);

       const col = {
           DeliveredBabyBirthInfoCollection: this.DeliveredBabyBirthInfoCollection
       };

        console.log('arrays');
            console.log(col);
            /*  this.DeliveredBabyBirthInfoCollection.push(
                            'DeliveredBabyBirthInfoCollection': [
                                {
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
                                }]
                    );*/




        /*  const babyconditionCommand: BabyConditionCommand = {
              PatientDeliveryInformationId: 0,
              PatientMasterVisitId: this.patientMasterVisitId,
              BirthWeight: this.babyFormGroup.value[0]['birthWeight'],
              Sex: this.babyFormGroup.value[0]['babySex'],
              DeliveryOutcome: this.babyFormGroup.value[0]['outcome'],
              ResuscitationDone: this.babyFormGroup.value[0]['resuscitationDone'],
              BirthDeformity: this.babyFormGroup.value[0]['deformity'],
              TeoGiven: this.babyFormGroup.value[0]['teoGiven'],
              BreastFedWithinHour: this.babyFormGroup.value[0]['breastFed'],
              BirthNotificationNumber: this.babyFormGroup.value[0]['notificationNumber'],
              Comment: this.babyFormGroup.value[0]['comment'],
              CreatedBy: this.userId,
              ApgrarScore: this.apgarSCore
          };*/

        const vitaminA = this.drugAdminOptions.filter(x => x.itemName == 'Vitamin A Supplementation');
        const haartAnc = this.drugAdminOptions.filter(x => x.itemName == 'Started HAART in ANC');
        const maternityArv = this.drugAdminOptions.filter(x => x.itemName == 'ARVs Started in Maternity');
        const infantArv = this.drugAdminOptions.filter(x => x.itemName == 'Infant Provided With ARV prophylaxis');
        const cotrimoxazole = this.drugAdminOptions.filter(x => x.itemName == 'Cotrimoxazole');

        this.AdministredDrugs.push(
            {
                Id: vitaminA[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['vitaminASupplement'],
                Description: 'Vitamin A Supplementation'
            },
            {
                Id: haartAnc[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['HaartANC'],
                Description: 'Started HAART in ANC'
            },
            {
                Id: cotrimoxazole[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['cotrimoxazole'],
                Description: 'ARVs Started in Maternity'
            },
            {
                Id: maternityArv[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['ARVStartedMaternity'],
                Description: 'Infant Provided With ARV prophylaxis'
            },
            {
                Id: infantArv[0].itemId, Value: this.maternalDrugAdministrationForGroup.value[0]['ARVStartedMaternity'],
                Description: 'Cotrimoxazole'
            }
        );

        const drugAdministrationCommand: DrugAdministrationCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreatedBy: this.userId,
            AdministredDrugs: this.AdministredDrugs
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
            DateDischarged: this.dischargeFormGroup.value[0]['dischargeDate'],
            CreatedBy: this.userId
        };

        const referralCommand: ReferralCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ReferredFrom: this.diagnosisFormGroup.value[1]['referredFrom'],
            ReferredTo: this.diagnosisFormGroup.value[1]['referredTo'],
            ReferralReason: '',
            ReferralDate: null,
            ReferredBy: this.userId,
            DeleteFlag: false,
            CreatedBy: this.userId
        };

        const nextAppointmentCommand: NextAppointmentCommand = {
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            ServiceAreaId: this.serviceAreaId,
            AppointmentDate: this.dischargeFormGroup.value[2]['nextAppointmentDate'],
            Description: this.dischargeFormGroup.value[2]['remarks'],
            StatusDate: new Date(),
            DifferentiatedCareId: 0,
            AppointmentReason: 'Follow up',
            CreatedBy: this.userId

        };

        const motherProfileCommand: MotherProfileCommand = {
            PatientPregnancy: pregnancyCommand
        };

        // const matMotherProfile = this.matService.savePregnancyProfile(pregnancyCommand);
        const matVisitDetails = this.matService.saveVisitDetails(visitDetailsCommand);
        const matDiagnosis = this.matService.saveDiagnosis(diagnosisCommand);
        const matDrugAdministartion = this.matService.saveMaternalDrugAdministration(drugAdministrationCommand);
        const matEducation = this.matService.savePatientEducation(patiendEducationCommand);
        const matDischarge = this.matService.saveDischarge(dischargeCommand);
        const matReferral = this.matService.saveReferrals(referralCommand);
        const matNextAppointment = this.matService.saveNextAppointment(nextAppointmentCommand);
        const matDelivery = this.matService.savePatientDelivery(maternityDeliveryCommand);
        const matBabyCondition = this.matService.saveBabySection(col);



        forkJoin([ // matVisitDetails,
            // matMotherProfile,
            // matDiagnosis,
            //  matDrugAdministartion,
            //  matDelivery
            //  matEducation,
            //  matDischarge,
            //  matReferral,
            // matNextAppointment
            matBabyCondition
        ])
            .subscribe(
                (result) => {
                    console.log(`success `);
                    console.log(result);

                    /* this.pregnancyId = result[1]['PregnancyId'];
                     maternityDeliveryCommand.ProfileId = this.pregnancyId;

                     const matDelivery = this.matService.savePatientDelivery(maternityDeliveryCommand).subscribe(
                         (res) => {
                             this.deliveryId = res['PatientDeliveryInformationId'];
                             babyconditionCommand.PatientDeliveryInformationId = this.deliveryId;
                             const matBabyCondition = this.matService.saveBabySection(babyconditionCommand);
                             console.log(`result`, res);
                         }
                     );*/
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





