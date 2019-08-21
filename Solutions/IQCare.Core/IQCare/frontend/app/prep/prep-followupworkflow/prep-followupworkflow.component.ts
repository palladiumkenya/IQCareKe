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
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';
import { LookupItemView } from '../../shared/_models/LookupItemView';

import { PersonHomeService } from '../../dashboard/services/person-home.service';
@Component({
    selector: 'app-prep-followupworkflow',
    templateUrl: './prep-followupworkflow.component.html',
    styleUrls: ['./prep-followupworkflow.component.css'],
    providers: [PersonHomeService, SearchService]
})
export class PrepFollowupworkflowComponent implements OnInit {
    public personId = 0;

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
    EligibilityInformation: any[] = [];
    VitalsEligibilityInformation: any[] = [];
    RiskEligibilityInformation: any[] = [];
    enrolledServices: any[] = [];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    services: any[] = [];
    userId: number;


    constructor(private route: ActivatedRoute,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private encounterService: EncounterService,
        private searchService: SearchService,
        private router: Router,
        private personService: PersonHomeService,
        public zone: NgZone
    ) { }

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.personId = params['personId'];
            this.patientId = params['patientId'];

            this.serviceAreaId = params['serviceId'];


        });
        this.route.data.subscribe(res => {
            const { HTSEncounterArray } = res;
            const { RiskAssessmentArray } = res;
            const { PersonVitalsArray } = res;
            const { HTSEncounterHistoryArray } = res;
            const { prepEncounterTypeOption } = res;
            this.prepEncounterType = prepEncounterTypeOption;



            this.htsencounters = HTSEncounterArray;
            this.personvitals = PersonVitalsArray;
            this.riskassessmentencounter = RiskAssessmentArray;
            this.htshistory = HTSEncounterHistoryArray;

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
        this.isRiskAssessmentEligible();
        this.isEligible();
        this.isFollowupEligible();

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
