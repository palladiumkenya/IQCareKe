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
import {MaternityVisitDetailsCommand} from './commands/maternity-visit-details-command';
import {MaternityService} from '../_services/maternity.service';
import {PregnancyCommand} from './commands/pregnancy-command';
import {MaternityDeliveryCommand} from './commands/maternity-delivery-command';
import {BabyConditionCommand} from './commands/baby-condition-command';
import {ApgarScoreCommand} from './commands/apgar-score-command';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {Subscription} from 'rxjs/index';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';

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
    lookupItems$: Subscription;
    _options: any[] = [];

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

    constructor(private route: ActivatedRoute,
                private matService: MaternityService,
                private _lookupItemService: LookupItemService ,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
        this.visitDetailsFormGroup = new FormArray([]);
        this.maternalDrugAdministrationForGroup = new FormArray([]);
        this.dischargeFormGroup = new FormArray([]);
        this.babyFormGroup = new FormArray([]);
        this.maternityTestsFormGroup = new FormArray([]);
        this.formType = 'maternity';
    }

    ngOnInit() {
        this.getLookupItems('ApgarScore');
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
            'yesNoNaOptions': this.yesNoOptions
        });
    }

    onVisitDetailsNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    OnMotherProfileNotify(formGroup: FormGroup): void {
        this.visitDetailsFormGroup.push(formGroup);
    }

    onPatientDiagnosis(formGroup: FormGroup): void {
        this.diagnosisFormGroup.push(formGroup);
    }

    onPatientDeliveryNotify(formGroup: FormGroup) {
        this.diagnosisFormGroup.push(formGroup);
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

    public getLookupItems(groupName: string) {
        this.lookupItems$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        this._options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
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
            ageAtMenarche: null,
            pregnancyId: null,
            visitNumber: null,
            visitType: null,
            treatedForSyphilis: null,
            deleteFlag: false,
            createDate: new Date(),
            createdBy: this.userId,
            postpartum: null
        };

        const pregnancyCommand: PregnancyCommand = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            Lmp: new Date(this.visitDetailsFormGroup[1].get('dateLMP').value),
            Edd: new Date(this.visitDetailsFormGroup[1].get('dateEDD').value),
            Gestation: this.visitDetailsFormGroup[1].get('gestation').value,
            Gravidae: parseInt(this.visitDetailsFormGroup[1].get('gravidae').value, 10),
            Parity: this.visitDetailsFormGroup[1].get('parityOne').value,
            Parity2: this.visitDetailsFormGroup[1].get('parityTwo').value,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            DeleteFlag: false
        };

        const maternityDeliveryCommand: MaternityDeliveryCommand = {
            PatinetMasterVisitId: this.patientMasterVisitId,
            ProfileId: 0,
            DurationOfLabour: this.diagnosisFormGroup[1].get('labourDuration').value,
            DateOfDelivery: this.diagnosisFormGroup[1].get('deliveryDate').value,
            TimeOfDelivery: this.diagnosisFormGroup[1].get('deliveryTime').value,
            ModeOfDelivery: this.diagnosisFormGroup[1].get('deliveryMode').value,
            PlacentaComplete: this.diagnosisFormGroup[1].get('placentaComplete').value,
            BloodLossCapacity: this.diagnosisFormGroup[1].get('bloodLossCount').value,
            BloodLossClassification: this.diagnosisFormGroup[1].get('bloodLoss').value,
            MotherCondition: this.diagnosisFormGroup[1].get('deliveryCondition').value,
            MaternalDeathAudited: this.diagnosisFormGroup[1].get('maternalDeathsAudited').value,
            MaternalDeathAuditDate: this.diagnosisFormGroup[1].get('auditDate').value,
            DeliveryComplicationsExperienced: this.diagnosisFormGroup[1].get('deliveryComplications').value,
            DeliveryComplicationNotes: this.diagnosisFormGroup[1].get('deliveryComplicationNotes').value,
            DeliveryConductedBy: this.diagnosisFormGroup[1].get('deliveryConductedBy').value,
            CreatedBy: this.userId
        };

        const apgarscoreOne = this._options.filter(x => x.itemName == 'Apgar Score 1 min');
        const apgarscoreTwo = this._options.filter(x => x.itemName == 'Apgar Score 5 min');
        const apgarscoreThree = this._options.filter(x => x.itemName == 'Apgar Score 10 min');

        this.apgarSCore.push(
           {ApgarSCoreId: apgarscoreOne[0].itemId , ApgarScoreType: '', SCore: this.babyFormGroup[0].get('agparScore1min').value},
            {ApgarSCoreId: apgarscoreTwo[0].itemId, ApgarScoreType: '', SCore: this.babyFormGroup[0].get('agparScore5min').value},
            {ApgarSCoreId: apgarscoreThree[0].itemId, ApgarScoreType: '', SCore: this.babyFormGroup[0].get('agparScore10min').value}
        );

        const babyconditionCommand: BabyConditionCommand = {
            PatientDeliveryInformationId: 0,
            PatientMasterVisitId: this.patientMasterVisitId,
            BirthWeight: this.babyFormGroup[0].get('birthWeight').value,
            Sex: this.babyFormGroup[0].get('babySex').value,
            DeliveryOutcome: this.babyFormGroup[0].get('outcome').value,
            ResuscitationDone: this.babyFormGroup[0].get('resuscitationDone').value,
            BirthDeformity: this.babyFormGroup[0].get('deformity').value,
            TeoGiven: this.babyFormGroup[0].get('teoGiven').value,
            BreastFedWithinHour: this.babyFormGroup[0].get('breastFed').value,
            BirthNotificationNumber: this.babyFormGroup[0].get('notificationNumber').value,
            Comment: this.babyFormGroup[0].get('comment').value,
            CreatedBy: this.userId,
            ApgrarScore: this.apgarSCore
        };


        const matMotherProfile = this.matService.savePregnancyProfile(pregnancyCommand);
        const matVisitDetails = this.matService.saveVisitDetails(visitDetailsCommand);
        const matDelivery = this.matService.savePatientDelivery(maternityDeliveryCommand);
        const matBabyCondition = this.matService.saveBabySection(babyconditionCommand);

    }
}





