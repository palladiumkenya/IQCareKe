import { OrdVisitCommand } from './../_models/hei/OrdVisitCommand';
import { LabOrder } from './../_models/hei/LabOrder';
import { HeiService } from './../_services/hei.service';
import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { FormGroup, FormArray } from '@angular/forms';
import { ImmunizationHistoryTableData } from '../_models/hei/ImmunizationHistoryTableData';
import { Vaccination } from '../_models/hei/Vaccination';
import { MilestoneData } from '../_models/hei/MilestoneData';
import { Milestone } from '../_models/hei/Milestone';
import { PatientIcf } from '../_models/hei/PatientIcf';
import { PatientIcfAction } from '../_models/hei/PatientIcfAction';
import { DefaultParameters } from '../_models/hei/DefaultParameters';
import { forkJoin } from 'rxjs/index';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';

@Component({
    selector: 'app-hei',
    templateUrl: './hei.component.html',
    styleUrls: ['./hei.component.css']
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

    isLinear: boolean = false;
    deliveryMatFormGroup: FormArray;
    visitDetailsFormGroup: FormArray;
    tbAssessmentFormGroup: FormArray;
    milestonesFormGroup: FormArray;
    immunizationHistoryFormGroup: FormArray;
    infantFeedingFormGroup: FormArray;

    heiOutcomeFormGroup: FormArray;
    nextAppointmentFormGroup: FormArray;
    hivTestingFormGroup: any[];

    constructor(private route: ActivatedRoute,
        private heiService: HeiService,
        private zone: NgZone,
        private router: Router,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.deliveryMatFormGroup = new FormArray([]);
        this.visitDetailsFormGroup = new FormArray([]);
        this.tbAssessmentFormGroup = new FormArray([]);

        this.immunizationHistoryFormGroup = new FormArray([]);
        this.milestonesFormGroup = new FormArray([]);
        this.infantFeedingFormGroup = new FormArray([]);

        this.heiOutcomeFormGroup = new FormArray([]);
        this.nextAppointmentFormGroup = new FormArray([]);

        this.hivTestingFormGroup = [];
        this.formType = 'hei';
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                console.log(params);
                const { patientId, personId, serviceAreaId } = params;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceAreaId = serviceAreaId;
            }
        );

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.locationId = JSON.parse(localStorage.getItem('appLocationId'));
        this.patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
        this.patientEncounterId = JSON.parse(localStorage.getItem('patientEncounterId'));
        this.visitDate = new Date(localStorage.getItem('visitDate'));
        this.visitType = JSON.parse(localStorage.getItem('visitType'));

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
            'testResults': this.heiHivTestingResultsOptions
        });

        this.heiService.getPatientById(this.patientId).subscribe(
            (result) => {
                console.log(result);
                const { ptn_pk } = result;
                this.ptn_pk = ptn_pk;
            }
        );

        this.heiService.getHeiLabTests().subscribe(
            (result) => {
                const labTestsList = result['labTestsList'];
                for (let i = 0; i < labTestsList.length; i++) {
                    const key = labTestsList[i]['key'];
                    if (labTestsList[i]['key'] == 'PCR') {
                        this.pcrLabTest = labTestsList[i]['value'];
                    } else if (labTestsList[i]['key'] == 'Viral Load') {
                        this.viralLoadLabTest = labTestsList[i]['value'];
                    } else if (labTestsList[i]['key'] == 'HIV Rapid Test') {
                        this.antibodyLabTest = labTestsList[i]['value'];
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
        this.milestonesFormGroup.push(formGroup['form']); // = formGroup['form'];
        this.milestone_table_data.push(formGroup['data']);
    }

    onImmunizationHistory(formGroup: Object): void {
        this.immunizationHistoryFormGroup.push(formGroup['form']);
        this.immunization_table_data.push(formGroup['data']);
    }

    onTbAssessment(formGroup: FormGroup) {
        this.tbAssessmentFormGroup.push(formGroup);
    }

    onHeiOutcomeNotify(formGroup: FormGroup) {
        this.nextAppointmentFormGroup.push(formGroup);
    }

    onHivTestingNotify(hivTests: any) {
        this.hivTestingFormGroup.push(hivTests);
    }

    onNextAppointmentNotify(formGroup: FormGroup) {
        this.nextAppointmentFormGroup.push(formGroup);
    }

    onCompleteEncounter() {
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
                console.log(this.hivTestingFormGroup[i][j]);
                console.log(this.pcrLabTest);
                if (
                    this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '1st DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '2nd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == '3rd DNA PCR'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Repeat confirmatory PCR (for +ve)'
                    || this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Confirmatory PCR (for  +ve)'
                ) {
                    labTestId = this.pcrLabTest[0]['id'];
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.pcrLabTest[0]['name'];
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Baseline Viral Load (for +ve)') {
                    labTestId = this.viralLoadLabTest[0]['id'];
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.viralLoadLabTest[0]['name'];
                } else if (this.hivTestingFormGroup[i][j]['testtype']['itemName'] == 'Final Antibody') {
                    labTestId = this.antibodyLabTest[0]['id'];
                    latTestNotes = this.hivTestingFormGroup[i][j]['comments'];
                    labTestName = this.antibodyLabTest[0]['name'];
                }
            }
            laborder.LabTests.push({
                Id: labTestId,
                Notes: latTestNotes,
                LabTestName: labTestName
            });
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

        /*const visitDetailsData = {
            'Id': 0,
            'PatientMasterVisitId': this.patientMasterVisitId,
            'PatientId': this.patientId,
            'VisitDate': this.visitDetailsFormGroup.value[0]['visitDate'],
            'VisitType': this.visitDetailsFormGroup[0]['visitType'],
            'CreatedDate': new Date(),
            'CreatedBy': this.userId,
            'DeleteFlag': false
        };

        this.heiService.saveHeiVisitDetails(visitDetailsData)
            .subscribe(
                (result) => {
                    console.log(result);

                }
            );*/

        // const heiVisitDetails = this.heiService.saveHeiVisitDetails(visitDetailsData);
        const heiDelivery = this.heiService.saveHieDelivery(this.patientId, this.patientMasterVisitId, this.userId,
            isMotherRegistered, this.deliveryMatFormGroup.value[0], this.deliveryMatFormGroup.value[1]);
        const heiImmunization = this.heiService.saveImmunizationHistory(this.vaccination);
        const heiMilestone = this.heiService.saveMilestoneHistory(this.milestone);
        const heitbAssessment = this.heiService.saveTbAssessment(patientIcf, patientIcfAction);
        const heiOrdVisit = this.heiService.saveOrdVisit(ordVisitCommand, laborder);


        forkJoin([heiOrdVisit])
            .subscribe(
                (result) => {
                    console.log(result);

                    laborder.VisitId = result[0]['visit_Id'];
                    const heiLab = this.heiService.saveHeiLabOrder(laborder).subscribe(
                        (res) => {
                            const labOrderId = res['labOrderId'];

                            /*const completeHeiLabOrder = this.heiService.saveCompleteHeiLabOrder().subscribe(
                                (completeRes) => {

                                }
                            );*/
                        }
                    );

                },
                (error) => {
                    console.log(`error ` + error);
                },
                () => {
                    console.log(`complete`);
                    this.snotifyService.success('Successfully saved HEI encounter ', 'HEI', this.notificationService.getConfig());
                }
            );

        /*this.zone.run(() => {
            this.router.navigate(['/dashboard/personhome/' + this.personId], { relativeTo: this.route });
        });*/
    }
}
