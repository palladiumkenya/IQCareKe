import {HeiService} from './../_services/hei.service';
import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {LookupItemView} from '../../shared/_models/LookupItemView';
import {FormGroup, FormArray} from '@angular/forms';
import {ImmunizationHistoryTableData} from '../_models/hei/ImmunizationHistoryTableData';
import {Vaccination} from '../_models/hei/Vaccination';
import {MilestoneData} from '../_models/hei/MilestoneData';
import {Milestone} from '../_models/hei/Milestone';
import {PatientIcf} from '../_models/hei/PatientIcf';
import {PatientIcfAction} from '../_models/hei/PatientIcfAction';
import {DefaultParameters} from '../_models/hei/DefaultParameters';

@Component({
    selector: 'app-hei',
    templateUrl: './hei.component.html',
    styleUrls: ['./hei.component.css']
})

export class HeiComponent implements OnInit {
    patientId: number;
    personId: number;
    serviceAreaId: number;
    patientMasterVisitId: number;
    userId: number;
    formType: string;

    defaultParameters: DefaultParameters;

    immunizationHistoryTableData: any[] = [];
    milestoneHistoryData: any[] = [];
    vaccination: Vaccination[] = [];
    milestone: Milestone[] = [];
    deliveryOptions: any[] = [];
    maternalhistoryOptions: any[] = [];
    hivtestingOptions: any[] = [];
    motherreceivedrugsOptions: any[] = [];
    heimotherregimenOptions: any[] = [];
    yesnoOptions: any[] = [];
    motherdrugsatinfantenrollmentOptions: any[] = [];
    primarycaregiverOptions: any[] = [];
    immunizationHistoryOptions: any[] = [];
    milestoneOptions: any[] = [];
    tbAssessmentOptions: any[] = [];

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

    isLinear: boolean = false;
    deliveryMatFormGroup: FormArray;
    visitDetailsFormGroup: FormArray;
    tbAssessmentFormGroup: FormArray;
    milestonesFormGroup: FormArray;
    immunizationHistoryFormGroup: FormArray;
    infantFeedingFormGroup: FormArray;

    heiOutcomeFormGroup: FormArray;
    nextAppointmentFormGroup: FormGroup;
    hivTestingFormGroup: any[];

    constructor(private route: ActivatedRoute,
                private heiService: HeiService) {
        this.deliveryMatFormGroup = new FormArray([]);
        this.visitDetailsFormGroup = new FormArray([]);
        this.tbAssessmentFormGroup = new FormArray([]);
        this.immunizationHistoryFormGroup = new FormArray([]);
        this.milestonesFormGroup = new FormArray([]);
        this.infantFeedingFormGroup = new FormArray([]);
        this.heiOutcomeFormGroup = new FormArray([]);
        this.hivTestingFormGroup = [];
        this.formType = 'hei';
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

        this.defaultParameters = {
            patientId: this.patientId,
            personId: this.personId,
            userId: this.userId,
            patientMasterVisitId: this.patientMasterVisitId } as DefaultParameters;

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
            console.log('test options');
            console.log(res);
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
            'testResults': this.heiHivTestingResultsOptions
        });

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

    onMilestonesNotify(formGroup: FormGroup): void {
        this.milestonesFormGroup.push(formGroup);
    }

    onMilsetoneTableData(milestoneData: MilestoneData[]): void {
        this.milestoneHistoryData.push(milestoneData);
    }

    onImmunizationHistory(formGroup: FormGroup): void {
        this.immunizationHistoryFormGroup.push(formGroup);
    }

    onImmunizationHistoryData(formData: ImmunizationHistoryTableData[]): void {
        this.immunizationHistoryTableData.push(formData);
    }

    onTbAssessment(formGroup: FormGroup) {
        this.tbAssessmentFormGroup.push(formGroup);
    }

    onHeiOutcomeNotify(formGroup: FormGroup) {
        this.heiOutcomeFormGroup.push(formGroup);
    }

    onHivTestingNotify(hivTests: any) {
        this.hivTestingFormGroup.push(hivTests);
    }

    onNextAppointmentNotify(formGroup: FormGroup) {
        this.nextAppointmentFormGroup = formGroup;
    }

    onCompleteEncounter() {
        console.log(this.deliveryMatFormGroup.value);
        console.log(this.visitDetailsFormGroup.value);
        console.log(this.hivTestingFormGroup);

        for (let i = 0; i < this.immunizationHistoryTableData.length; i++) {
            this.vaccination.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                Period: this.immunizationHistoryTableData[i].immunizationPeriodId,
                Vaccine: this.immunizationHistoryTableData[i].immunizationGivenId,
                VaccineStage: this.immunizationHistoryTableData[i].immunizationPeriodId,
                DeleteFlag: 0,
                CreatedBy: this.userId,
                CreateDate: new Date(),
                VaccineDate: new Date(this.immunizationHistoryTableData[i].dateImmunized),
                Active: 0,
                NextSchedule: new Date(this.immunizationHistoryTableData[i].nextScheduled)
            });
        }

        for (let i = 0; i < this.milestoneHistoryData.length; i++) {
            this.milestone.push({
                Id: 0,
                PatientId: this.patientId,
                PatientMasterVisitId: this.patientMasterVisitId,
                TypeAssessed: this.milestoneHistoryData[i].milestoneId,
                Achieved: this.milestoneHistoryData[i].achievedId,
                Status: this.milestoneHistoryData[i].statusId,
                Comment: this.milestoneHistoryData[i].comment,
                CreateDate: new Date(),
                CreatedBy: this.userId,
                DeleteFlag: 0
            });
        }

        const patientIcf = {
            Id: 0,
            PatientId: this.patientId,
            PatientMasterVisitId: this.patientMasterVisitId,
            CreateDate: new Date(),
            CreatedBy: this.userId,
            OnAntiTbDrugs: this.tbAssessmentFormGroup.value[0]['currentlyOnAntiTb'],
            Cough: this.tbAssessmentFormGroup.value[0]['coughAnyDuration'],
            Fever: this.tbAssessmentFormGroup.value[0]['fever'],
            WeightLoss: this.tbAssessmentFormGroup.value[0]['weightLoss'],
            ContactWithTb: this.tbAssessmentFormGroup.value[0]['contactTB'],

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

        // this.patientMasterVisitId = 2;

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

        this.heiService.saveHeiVisitDetails(this.patientId, this.patientMasterVisitId, this.visitDetailsFormGroup.value[0], this.userId)
            .subscribe(
                (result) => {
                    console.log(result);

                }
            );

        this.heiService.saveHieDelivery(this.patientId, this.patientMasterVisitId, this.userId,
            isMotherRegistered, this.deliveryMatFormGroup.value[0], this.deliveryMatFormGroup.value[1])
            .subscribe(
                (result) => {
                    console.log(result);
                }
            );

        this.heiService.saveImmunizationHistory(this.vaccination)
            .subscribe(
                (result) => {
                    console.log(result);
                }
            );

        this.heiService.saveMilestoneHistory(this.milestone)
            .subscribe(
                (result) => {
                    console.log(result);
                }
            );


        this.heiService.saveTbAssessment(patientIcf, patientIcfAction)
            .subscribe(
                (results) => {
                    console.log(results);
                }
            );
    }
}
