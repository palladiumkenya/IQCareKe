import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EncounterDetails } from '../../dashboard/_model/HtsEncounterdetails';
import { PersonView } from '../../records/_models/personView';
import { Subscription } from 'rxjs';
import { SearchService } from '../../registration/_services/search.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import * as moment from 'moment';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { EncounterService } from '../../shared/_services/encounter.service';
import { PrepCheckinComponent } from './../prep-checkin/prep-checkin.component';
import { MatTableDataSource, MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PrepService } from '../_services/prep.service';
import { TriageService } from '../../clinical/_services/triage.service';

import { PersonHomeService } from '../../dashboard/services/person-home.service';
import { PrepConfirmationDialogComponent } from '../prep-confirmationdialog/prep-confirmationdialog';
@Component({
    selector: 'app-prep-followupworkflow',
    templateUrl: './prep-followupworkflow.component.html',
    styleUrls: ['./prep-followupworkflow.component.css'],
    providers: [PersonHomeService, SearchService, TriageService]
})
export class PrepFollowupworkflowComponent implements OnInit {
    public personId = 0;
    VisitCheckinDate: Date;
    EmrMode: string;
    encounterType: string;
    prepFormsView: any;
    patientId: number;
    public personVitalWeight = 0;
    public person: PersonView;
    public personView$: Subscription;
    encounterDetail: EncounterDetails;
    personvitals: any[];
    htsencounters: any[];
    riskassessmentencounter: any[];
    prepEncounterType: LookupItemView[];
    riskencounter: any[];
    serviceAreaId: number;
    htsdone: boolean = true;
    htsrisk: boolean = false;
    vitalsdone: boolean = true;
    vitalrisk: boolean = false;
    riskassessmentdone: boolean = true;
    preprisk: boolean = true;
    followupdone: boolean = true;
    Eligible: boolean = true;
    disabled: boolean = false;
    htshistory: any[];
    riskassessmentlist: any[] = [];
    followuplist: any[] = [];
    refilllist: any[] = [];
    EligibilityInformation: any[] = [];
    VitalsEligibilityInformation: any[] = [];
    RiskEligibilityInformation: any[] = [];
    enrolledServices: any[] = [];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    services: any[] = [];
    prep_history_table_data: any[] = [];
    userId: number;
    FormSettings: any[] = [];
    followupvisible: boolean = true;
    monthlyrefillvisible: boolean = true;
    CheckinDate: String;
    HtsEncountersList: any[] = [];
    vitalsDataTable: any[] = [];
    dialogRef: MatDialogRef<PrepConfirmationDialogComponent>;
    VitalDone: boolean = false;
    HTSDone: boolean = false;
    RiskDone: boolean = false;
    FollowDone: boolean = false;
    MonthlyrefillDone: boolean = false;
    constructor(private route: ActivatedRoute,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private encounterService: EncounterService,
        private searchService: SearchService,
        private prepService: PrepService,
        private router: Router,
        private triageService: TriageService,
        private personService: PersonHomeService,
        public zone: NgZone
    ) { }

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.personId = params['personId'];
            this.patientId = params['patientId'];

            this.serviceAreaId = params['serviceId'];
            // this.getCorrectDisplayForms(this.patientId);


        });
        this.route.data.subscribe(res => {
            const { HTSEncounterArray } = res;
            const { RiskAssessmentArray } = res;
            const { PersonVitalsArray } = res;
            const { HTSEncounterHistoryArray } = res;
            const { prepEncounterTypeOption } = res;
            const { FormSettingsArray } = res;

            console.log(FormSettingsArray);

            this.prepEncounterType = prepEncounterTypeOption;



            if (localStorage.getItem('PrepVisitDate') !== null
                && localStorage.getItem('PrepVisitDate') !== undefined) {
                this.CheckinDate = moment(moment(localStorage.getItem('PrepVisitDate')).toDate()).format('DD-MM-YYYY').toString();
            }

            this.htsencounters = HTSEncounterArray;
            this.personvitals = PersonVitalsArray;
            this.riskassessmentencounter = RiskAssessmentArray;
            this.htshistory = HTSEncounterHistoryArray;
            this.FormSettings = FormSettingsArray;

            if (this.FormSettings != null && this.FormSettings !== undefined) {
               
                if (this.FormSettings['encounterType'] !== null   && this.FormSettings['encounterType'] !== undefined) {
                    this.encounterType = this.FormSettings['encounterType'].toString();
                    if (this.encounterType.toString().toLowerCase() == 'monthlyrefill') {
                        this.followupvisible = false;
                        this.monthlyrefillvisible = true;
                    } else if (this.encounterType.toString().toLowerCase() == 'followup') {
                        this.followupvisible = true;
                        this.monthlyrefillvisible = false;
                    } else {
                        this.followupvisible = true;
                        this.monthlyrefillvisible = true;
                    }
                }
                if (this.FormSettings['prepFormsView'] !== null  && this.FormSettings['prepFormsView'] !== undefined) {
                    this.prepFormsView = this.FormSettings['prepFormsView'];
                }



            

            }



            if (this.personvitals.length > 0) {
                this.personVitalWeight = this.personvitals['0'].weight;

            }


            this.riskencounter = this.riskassessmentencounter['encounters'];
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.encounterDetail = this.htsencounters[0];


        this.getAllServices();

        this.getPatientDetailsById(this.personId);
        this.isTriageEligible();
        this.getPersonEnrolledServices(this.personId);
      
        this.getEncounterDone();


    }
    RefillLink() {
        this.zone.run(() => {
            this.router.navigate(['/prep/monthlyrefill/' + '/' + this.patientId + '/' + this.personId + '/'
                + this.serviceAreaId],
                { relativeTo: this.route });
        });
    }
    CheckOut() {
        this.dialogRef = this.dialog.open(PrepConfirmationDialogComponent, {
            disableClose: false
        });
        this.dialogRef.componentInstance.confirmMessage = "Are you Sure you want to CheckOut?";
        this.dialogRef.afterClosed().subscribe(result => {
            if (result) {

                if (result = true) {
                    let servicecheckoutid: number;
                    let emrmode: number;
                    servicecheckoutid = parseInt(localStorage.getItem('PrepCheckInId').toString(), 10);
                    this.VisitCheckinDate = moment(localStorage.getItem('PrepVisitDate')).toDate();

                    emrmode = parseInt(localStorage.getItem('PrepCheckinEmrModenumber').toString(), 10);
                    this.prepService.PatientCheckout(this.patientId, servicecheckoutid, this.serviceAreaId, this.userId
                        , this.VisitCheckinDate, emrmode,
                        2, false).subscribe((res) => {
                            let servicecheckId: number;
                            if (res['id'] != null) {
                                servicecheckId = parseInt(res['id'], 10);
                                if (servicecheckId > 0) {
                                    this.snotifyService.success(
                                        res['message'], 'PatientCheck Out ',
                                        this.notificationService.getConfig());

                                    localStorage.removeItem('PrepCheckinEmrMode');
                                    localStorage.removeItem('PrepCheckInId');
                                    localStorage.removeItem('PrepCheckinEmrMode');
                                    localStorage.removeItem('PrepVisitDate');
                                    localStorage.removeItem('prepCheckinPatientId');
                                    localStorage.removeItem('PrepCheckinEmrModenumber');
                                    localStorage.removeItem('PrepDateRecorded');


                                    this.zone.run(() => {
                                        this.router.navigate(['/prep/prepformslist/' + '/' + this.patientId + '/' + this.personId + '/'
                                            + this.serviceAreaId],
                                            { relativeTo: this.route });
                                    });
                                }
                            }
                        });
                }

            }
            this.dialogRef = null;
        });
    }

    public getEncounterDone() {
        this.VisitCheckinDate = moment(localStorage.getItem('PrepVisitDate')).toDate();
        this.prepService.getHtsEncounterDetailsBypersonIdVisitDate(this.personId, this.VisitCheckinDate).subscribe((res) => {
            if (res == null) {
                return;
            }

            if (res.length == 0)
                return;

            res.forEach(encounter => {
                this.HtsEncountersList.push({
                    encounterDate: moment(moment(encounter.encounterDate).toDate()).format('DD-MM-YYYY').toString(),
                    testType: encounter.testType,
                    provider: encounter.provider,
                    resultOne: encounter.resultOne,
                    resultTwo: encounter.resultTwo,
                    finalResult: encounter.finalResult,
                    consent: encounter.consent,
                    partnerListingConsent: encounter.partnerListingConsent,
                    serviceArea: encounter.serviceArea
                });
            });

            if (this.HtsEncountersList.length > 0) {
                this.htsdone = true;
            } else {
                this.htsdone = false;
            }
            console.log(this.HtsEncountersList[0].finalResult);
        });
        this.triageService.GetPatientVitalsByVisitDate(this.patientId, this.VisitCheckinDate).subscribe((res) => {



            if (res == null) {
                return;
            }

            this.vitalsDataTable = [];

            res.forEach(info => {
                this.vitalsDataTable.push({
                    id: info.id,
                    visitDate: moment(moment(info.visitDate).toDate()).format('DD-MM-YYYY').toString(),
                    height: info.height,
                    weight: info.weight,
                    bmi: info.bmi,
                    headCircumference: info.headCircumference,
                    muac: info.muac,
                    weightForAge: info.weightForAge,
                    weightForHeight: info.weightForHeight,
                    bmiZ: info.bmiZ,
                    diastolic: info.bpDiastolic,
                    systolic: info.bpSystolic,
                    temperature: info.temperature,
                    respiratoryRate: info.respiratoryRate,
                    heartRate: info.heartRate,
                    spo2: info.spo2,
                    comment: info.comment,
                    patientId: info.patientId,
                    patientMasterVisitId: info.patientMasterVisitId
                });
            });

            if (this.vitalsDataTable.length > 0) {
                this.vitalsdone = true;
            } else {
                this.vitalsdone = false;
            }
        });


        this.prep_history_table_data = [];

        const prepEncounters = this.prepService.getPrepEncounterbyVisitDate(this.patientId, this.serviceAreaId
            , this.VisitCheckinDate);
        prepEncounters.subscribe(
            (result) => {
                if (result == null) {
                    return;
                }

                if (result.length == 0)
                    return;

                // console.log(result);
                result.forEach(arrayValue => {
                    this.prep_history_table_data.push({
                        'behaviourrisk': 'Risk',
                        encounterType: arrayValue.encounterType,
                        prep_status: arrayValue.preStatus,
                        visitDate :  moment(moment(arrayValue.visitDate).toDate()).format('DD-MM-YYYY').toString(),
                        next_appointment: arrayValue.appointmentDate,
                        provider: arrayValue.providerName,
                        encounterStartTime: arrayValue.encounterStartTime,
                        patientEncounterId: arrayValue.id,
                        patientMasterVisitId: arrayValue.patientMasterVisitId
                    });
                });

                if (this.prep_history_table_data.length > 0) {
                    this.riskassessmentlist = this.prep_history_table_data.filter(x => x.encounterType == 'Behaviour Risk Assessment');

                    if (this.riskassessmentlist.length > 0) {
                        this.RiskDone = true;
                    }
                    this.followuplist = this.prep_history_table_data.filter(x => x.encounterType == 'PrEP Encounter');

                    if (this.followuplist.length > 0) {
                        this.FollowDone = true;
                    }
                    this.refilllist = this.prep_history_table_data.filter(x => x.encounterType == 'MonthlyRefill');

                    if (this.refilllist.length > 0) {
                        this.MonthlyrefillDone = true;
                    }



                }
            },
            (error) => {
                console.log(error);
            }
        );
    }
    public getCorrectDisplayForms(patientId: number) {
        this.VisitCheckinDate = moment(localStorage.getItem('PrepVisitDate')).toDate();
        this.EmrMode = localStorage.getItem('PrepCheckinEmrMode').toString();
        this.prepService.getCorrectDisplayForm(patientId, this.VisitCheckinDate, this.EmrMode).subscribe((res) => {
            console.log('result');

            if (res != null && res !== undefined) {
               
                if (res['encounterType'] !== null) {
                    this.encounterType = res['encounterType'].toString();
                }
                if (res['prepFormsView'] !== null) {
                    this.prepFormsView = res['prepFormsView'];
                }
                console.log(res);
                console.log(this.prepFormsView);
                console.log(this.encounterType);
                if (this.encounterType.toString().toLowerCase() == "monthlyrefill") {
                    this.followupvisible = false;
                    this.monthlyrefillvisible = true;
                }
                else if (this.encounterType.toString().toLowerCase() == "followup") {
                    this.followupvisible = true;
                    this.monthlyrefillvisible = false;
                } else {
                    this.followupvisible = true;
                    this.monthlyrefillvisible = true;
                }

            }
        },
            (error) => {
                this.snotifyService.error('Error getting the correct forms ' + error, 'DisplayForms', this.notificationService.getConfig());
            },
            () => {

            }
        );
    }
    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
            p => {
                this.person = p;

                localStorage.setItem('personId', this.person.personId.toString());


                if (this.person.patientId && this.person.patientId > 0) {

                    localStorage.setItem('patientId', this.person.patientId.toString());

                }
            },
            (err) => {
                this.snotifyService.error('Error getting patient details ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }
    getAllServices() {
        this.personService.getAllServices().subscribe((res) => {
            this.services = res;
            /*console.log(this.services);
            console.log(this.services);*/
            let index: number;
            index = this.services.findIndex(x => x.code == 'HTS');
            /*console.log(index);*/
        });
    }

    getPersonEnrolledServices(personId: number) {
        this.personService.getPersonEnrolledServices(personId).subscribe((res) => {

            this.enrolledServices = res['personEnrollmentList'];
            // console.log(this.enrolledServices);

            if (this.enrolledServices && this.enrolledServices.length > 0) {
                this.patientId = this.enrolledServices[0]['patientId'];
            }
            this.patientIdentifiers = res['patientIdentifiers'];
            this.identifiers = res['identifiers'];
        });

    }
    isPersonServiceEnrolled(service: string) {
        let index: number;
        index = this.services.findIndex(x => x.code == service);

        if (this.enrolledServices && this.enrolledServices.length > 0) {
            let returnValue = false;

            for (let i = 0; i < this.enrolledServices.length; i++) {
                if (this.enrolledServices[i].serviceAreaId == this.services[index]['id']) {
                    returnValue = true;
                }

            }

            if (returnValue == false) {
                localStorage.setItem('ageNumber', this.person.ageNumber.toString());
                this.zone.run(() => {
                    this.router.navigate(['/dashboard/enrollment/hts/' + this.personId + '/' + this.services[index]['id'] + '/'
                        + this.services[index]['code']],
                        { relativeTo: this.route });
                });
            } else {

                localStorage.removeItem('personId');
                localStorage.removeItem('patientId');
                localStorage.removeItem('partnerId');
                localStorage.removeItem('htsEncounterId');
                localStorage.removeItem('patientMasterVisitId');
                localStorage.removeItem('isPartner');
                localStorage.removeItem('editEncounterId');
                localStorage.removeItem('ageInMonths');

                this.searchService.lastHtsEncounter(this.personId).subscribe((res) => {
                    if (res['encounterId']) {
                        localStorage.setItem('htsEncounterId', res['encounterId']);
                    }
                    if (res['patientMasterVisitId'] > 0) {
                        localStorage.setItem('patientMasterVisitId', res['patientMasterVisitId']);
                    }

                    this.zone.run(() => {
                        localStorage.setItem('personId', this.personId.toString());
                        localStorage.setItem('patientId', this.patientId.toString());
                        localStorage.setItem('serviceAreaId', this.services[index]['id'].toString());
                        localStorage.setItem('ageInMonths', this.person.ageInMonths);
                        this.router.navigate(['/registration/home/'], { relativeTo: this.route });
                    });
                });

            }
        }
    }

    DiscontinuationLink() {
        this.zone.run(() => {
            this.router.navigate(['/prep/prepcareend/' + '/' + this.patientId + '/' + this.personId + '/'
                + this.serviceAreaId],
                { relativeTo: this.route });
        });
    }
    TriageLink() {
        this.zone.run(() => {
            this.router.navigate(['/clinical/triage/' + this.patientId + '/' + this.personId], { relativeTo: this.route });
        });
    }

    RiskAssessmentLink() {
        this.zone.run(() => {
            this.router.navigate(['/prep/riskassessment/' + '/' + this.patientId + '/' + this.personId + '/'
                + this.serviceAreaId],
                { relativeTo: this.route });
        });
    }
    FollowupLink() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {

        };

        const dialogRef = this.dialog.open(PrepCheckinComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
                    EncounterDate: data.visitdate,
                    PatientId: this.patientId,
                    EncounterType: this.prepEncounterType[0].itemId,
                    ServiceAreaId: this.serviceAreaId,
                    UserId: this.userId
                };

                this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).subscribe(
                    (result) => {
                        localStorage.setItem('visitDate', data.visitdate);
                        // localStorage.setItem('visitType', JSON.stringify(data.visitType));

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/prep/encounter/' + '/' + this.patientId + '/' + this.personId + '/'
                                + result['patientEncounterId'] + '/' + result['patientMasterVisitId'] + '/' + this.serviceAreaId],
                                { relativeTo: this.route });
                        });
                    },
                    (error) => {
                        this.snotifyService.error('Error checking in ' + error, 'CheckIn', this.notificationService.getConfig());
                    },
                    () => {

                    }
                );
            }
        );


    }

    isTriageEligible() {
        if (this.personVitalWeight > 0 && this.personVitalWeight < 35) {

            this.vitalsdone = true;
            this.vitalrisk = true;
            this.VitalsEligibilityInformation = [];
            if (this.VitalsEligibilityInformation.length > 0) {
                if (this.VitalsEligibilityInformation.includes('Weight less than 35') == false) {
                    this.VitalsEligibilityInformation.push('Weight less than 35');
                }
            } else {
                this.VitalsEligibilityInformation.push('Weight less than 35');
            }
        } else if (this.personVitalWeight == 0) {

            this.vitalsdone = false;
            this.vitalrisk = false;
            if (this.VitalsEligibilityInformation.length > 0) {
                if (this.VitalsEligibilityInformation.includes('Vitals not done') == false) {
                    this.VitalsEligibilityInformation.push('Vitals not done');
                }
            } else {
                this.VitalsEligibilityInformation.push('Vitals not done');
            }
        } else {
            this.vitalsdone = true;
            this.vitalrisk = false;
            this.vitalrisk = false;
            if (this.VitalsEligibilityInformation.length > 0) {
                if (this.VitalsEligibilityInformation.includes('Vitals  done') == false) {
                    this.VitalsEligibilityInformation.push('Vitals done');
                }
            } else {
                this.VitalsEligibilityInformation.push('Vitals  done');
            }
        }
    }
    isRiskAssessmentEligible() {
        if (this.riskencounter.length > 0) {

            if (this.riskencounter[0].assessmentOutCome !== null) {
                if (this.riskencounter[0].assessmentOutCome.toString().toLowerCase() == 'norisk') {
                    this.riskassessmentdone = true;
                    this.preprisk = false;
                    this.RiskEligibilityInformation = [];
                    if (this.RiskEligibilityInformation.length > 0) {
                        if (this.RiskEligibilityInformation.includes('AssessmentOutcome is  no risk') == false) {
                            this.RiskEligibilityInformation.push('AssessmentOutcome is  no risk');
                        }
                    } else {
                        this.RiskEligibilityInformation.push('AssessmentOutcome is  no risk');
                    }
                } else {
                    this.riskassessmentdone = true;
                    if (this.RiskEligibilityInformation.length > 0) {
                        if (this.RiskEligibilityInformation.includes('Risk Assessment   Done') == false) {
                            this.RiskEligibilityInformation.push('Risk Assessment   Done');
                        }
                    } else {
                        this.RiskEligibilityInformation.push('Risk Assessment  Done');
                    }

                }
            } else if (this.riskencounter[0].clientWillingTakingPrep !== null) {
                if (this.riskencounter[0].clientWillingTakingPrep.toString().toLowerCase() == 'no') {
                    this.riskassessmentdone = true;
                    this.preprisk = false;
                    this.RiskEligibilityInformation = [];
                    if (this.RiskEligibilityInformation.length > 0) {
                        if (this.RiskEligibilityInformation.includes('Client Not willing to take prep') == false) {
                            this.RiskEligibilityInformation.push('Client Not willing to take prep');
                        }
                    } else {
                        this.RiskEligibilityInformation.push('Client Not willing to take prep');
                    }
                } else {
                    this.riskassessmentdone = true;
                    if (this.RiskEligibilityInformation.length > 0) {
                        if (this.RiskEligibilityInformation.includes('Risk Assessment   Done') == false) {
                            this.RiskEligibilityInformation.push('Risk Assessment   Done');
                        }
                    } else {
                        this.RiskEligibilityInformation.push('Risk Assessment  Done');
                    }
                }
            } else {
                this.riskassessmentdone = true;
                if (this.RiskEligibilityInformation.length > 0) {
                    if (this.RiskEligibilityInformation.includes('Risk Assessment   Done') == false) {
                        this.RiskEligibilityInformation.push('Risk Assessment   Done');
                    }
                } else {
                    this.RiskEligibilityInformation.push('Risk Assessment  Done');
                }
            }



        } else {
            this.riskassessmentdone = false;
            if (this.RiskEligibilityInformation.length > 0) {
                if (this.RiskEligibilityInformation.includes('Risk Assessment Not  Done') == false) {
                    this.RiskEligibilityInformation.push('Risk Assessment Not  Done');
                }
            } else {
                this.RiskEligibilityInformation.push('Risk Assessment Not Done');
            }

        }
    }


    isFollowupEligible() {
        this.Eligible = true;
        this.disabled = false;
        if (this.htsdone == false) {
            this.Eligible = false;
            this.disabled = true;
        } else if (this.htsrisk == true) {
            this.Eligible = false;
            this.disabled = true;
        } else if (this.riskassessmentdone == false) {
            this.Eligible = false;
            this.disabled = true;
        } else if (this.preprisk == false) {
            this.Eligible = false;
            this.disabled = true;
        } else if (this.vitalsdone == false) {
            this.Eligible = false;
        } else if (this.vitalrisk == true) {

            this.Eligible = false;
            this.disabled = true;
        }




    }


    isEligible() {
        if (this.encounterDetail != undefined) {
            if (this.encounterDetail.finalResult == undefined) {
                this.htsdone = false;
                this.EligibilityInformation = [];
                if (this.EligibilityInformation.length > 0) {
                    if (this.EligibilityInformation.includes('HTS not done') == false) {
                        this.EligibilityInformation.push('HTS not done');
                    }
                } else {
                    this.EligibilityInformation.push('HTS not done');
                }
            } else if (this.htshistory.length <= 0) {
                this.htsdone = false;
                this.EligibilityInformation = [];
                if (this.EligibilityInformation.length > 0) {
                    if (this.EligibilityInformation.includes('At least one hts must be done') == false) {
                        this.EligibilityInformation.push('At least one hts must be done');
                    }
                } else {
                    this.EligibilityInformation.push('At least one hts must be done');
                }

            } else if (this.htshistory.length > 0) {
                this.htsdone = true;
                if (this.htshistory[0].finalResult == 'Positive') {
                    this.htsrisk = true;
                    this.EligibilityInformation = [];
                    if (this.EligibilityInformation.length > 0) {
                        if (this.EligibilityInformation.includes('Not Eligible') == false) {
                            this.EligibilityInformation.push('Not Eligible');
                        }
                    } else {
                        this.EligibilityInformation.push('Not Eligible');
                    }

                }

                let htsdate: Date;
                htsdate = moment(this.htshistory[0].encounterDate).toDate();
                if (htsdate != null && htsdate != undefined) {
                    let diffc: number;

                    diffc = moment(new Date()).diff(htsdate, 'days') + 1;

                    if (diffc > 3) {

                        this.htsdone = true;
                        this.EligibilityInformation = [];
                        if (this.EligibilityInformation.length > 0) {
                            if (this.EligibilityInformation.includes('HTS not done') == false) {
                                this.EligibilityInformation.push('HTS not done');
                            }
                        } else {
                            this.EligibilityInformation.push('HTS not done');
                        }
                    } else {
                        this.htsdone = true
                        if (this.EligibilityInformation.length > 0) {
                            if (this.EligibilityInformation.includes('HTS  done') == false) {
                                this.EligibilityInformation.push('HTS  done');
                            }
                        } else {
                            this.EligibilityInformation.push('HTS  done');
                        }

                    }
                }
            } else {
                this.htsdone = true
                if (this.EligibilityInformation.length > 0) {
                    if (this.EligibilityInformation.includes('HTS  done') == false) {
                        this.EligibilityInformation.push('HTS  done');
                    }
                } else {
                    this.EligibilityInformation.push('HTS  done');
                }

            }
        } else {
            this.htsdone = false;
            this.EligibilityInformation = [];
            if (this.EligibilityInformation.length > 0) {
                if (this.EligibilityInformation.includes('HTS not done') == false) {
                    this.EligibilityInformation.push('HTS not done');
                }
            } else {
                this.EligibilityInformation.push('HTS not done');
            }

        }

    }
    Back() {
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/prep/prepformslist/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                    { relativeTo: this.route });
            });
        });
    }

}
