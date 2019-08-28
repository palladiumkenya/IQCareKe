import { PrepCheckinComponent } from './../prep-checkin/prep-checkin.component';
import { Component, OnInit, NgZone } from '@angular/core';
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import { SearchService } from '../../registration/_services/search.service';
import { EncounterDetails } from '../../dashboard/_model/HtsEncounterdetails';
import { PersonView } from '../../records/_models/personView';
import { Subscription } from 'rxjs';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PrepService } from '../_services/prep.service';
import { EncounterService } from '../../shared/_services/encounter.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { EncounterDateSearch } from '../_models/EncounterDateSearch';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';

@Component({
    selector: 'app-prep-encounter-history',
    templateUrl: './prep-encounter-history.component.html',
    styleUrls: ['./prep-encounter-history.component.css'],
    providers: [EncounterService, PersonHomeService, SearchService]
})
export class PrepEncounterHistoryComponent implements OnInit {
    personId: number;
    patientId: number;
    serviceAreaId: number;
    userId: number;
    public person: PersonView;
    public personView$: Subscription;
    public personVitalWeight = 0;
    htsencounters: any[];
    search: EncounterDateSearch;
    prepEncounterType: LookupItemView[];
    encounterDetail: EncounterDetails;
    EligibilityInformation: any[] = [];
    iscareend: boolean;
    vitaldone: boolean;
    HTSEligible: boolean;
    public prep_history_table_data: PrepHistoryTableData[] = [];
    displayedColumns = ['encounter_date', 'formType', 'prep_status', 'next_appointment', 'provider', 'edit'];
    dataSource = new MatTableDataSource(this.prep_history_table_data);
    enrolledServices: any[] = [];
    riskencounters: any[];
    PatientCCCEnrolled: boolean = false;
    riskassessmentencounter: any[];
    personvitals: any[];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    enrolledService: any[] = [];
    riskdone: boolean = false;
    services: any[] = [];

    constructor(private prepService: PrepService,
        private dialog: MatDialog,
        private encounterService: EncounterService,
        private personService: PersonHomeService,
        private snotifyService: SnotifyService,
        private searchService: SearchService,
        private notificationService: NotificationService,
        private route: ActivatedRoute,
        public zone: NgZone,
        private router: Router) {
        this.search = new EncounterDateSearch();
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                const { personId, patientId, serviceId } = params;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceAreaId = serviceId;
            }
        );

        this.route.data.subscribe(
            (res) => {
                const { prepEncounterTypeOption } = res;
                this.prepEncounterType = prepEncounterTypeOption;
                const { HTSEncounterArray } = res;
                const { PersonVitalsArray } = res;
                const { RiskAssessmentArray } = res;

                this.htsencounters = HTSEncounterArray;
                this.personvitals = PersonVitalsArray;
                this.riskassessmentencounter = RiskAssessmentArray;
                this.riskencounters = this.riskassessmentencounter['encounters'];

                if (this.personvitals.length > 0) {
                    this.personVitalWeight = this.personvitals['0'].weight;
                }
            }
        );
        this.userId = JSON.parse(localStorage.getItem('appUserId'));

        this.encounterDetail = this.htsencounters[0];

        const prepEncounters = this.prepService.getPrepEncounterHistory(this.patientId, this.serviceAreaId);
        prepEncounters.subscribe(
            (result) => {
                // console.log(result);
                result.forEach(arrayValue => {
                    this.prep_history_table_data.push({
                        'behaviourrisk': 'Risk',
                        encounterType: arrayValue.encounterType,
                        prep_status: arrayValue.preStatus,
                        next_appointment: arrayValue.appointmentDate,
                        provider: arrayValue.providerName,
                        encounterStartTime: arrayValue.encounterStartTime,
                        patientEncounterId: arrayValue.id,
                        patientMasterVisitId: arrayValue.patientMasterVisitId
                    });
                });
                this.dataSource = new MatTableDataSource(this.prep_history_table_data);
            },
            (error) => {
                console.log(error);
            }
        );
        this.getAllServices();
        this.getPatientDetailsById(this.personId);
        this.getPersonEnrolledServices(this.personId);





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

    isServiceEligible(): boolean {
        let isEligible: boolean = false;
        isEligible = this.getHTSEligibility();
        this.HTSEligible = this.getHTSEligibility();
        if (isEligible == true) {
            isEligible = this.getPatientVitals();
            if (isEligible == true) {
                isEligible = this.getRiskDone();
            }
        }
        return isEligible;

    }
    navigateToRiskAssessment() {
        this.zone.run(() => {
            this.router.navigate(['/prep/riskassessment/' + this.patientId + '/' + this.personId
                + '/' + this.serviceAreaId], { relativeTo: this.route });
        });
    }
    navigateToCareEnd() {
        this.zone.run(() => {
            this.router.navigate(['/prep/prepcareend/' + this.patientId + '/' + this.personId
                + '/' + this.serviceAreaId], { relativeTo: this.route });
        });
    }
    navigateToTriage() {
        this.zone.run(() => {
            this.router.navigate(['/clinical/triage/' + this.patientId + '/' + this.personId], { relativeTo: this.route });
        });
    }
    getRiskDone(): boolean {
        let isEligible: boolean = false;

        if (this.riskencounters.length > 0) {
            this.riskdone = true;
            isEligible = true;

        } else {
            this.riskdone = false;
            isEligible = false;


        }

        return isEligible;
    }
    getPatientVitals(): boolean {
        let isEligible: boolean;

        if (this.personVitalWeight > 0 && this.personVitalWeight < 35) {

            isEligible = false;
            this.iscareend = true;
            if (this.EligibilityInformation.length > 0) {
                if (this.EligibilityInformation.includes('Weight less than 35') == false) {
                    this.EligibilityInformation.push('Weight less than 35');
                }
            } else {
                this.EligibilityInformation.push('Weight less than 35');
            }
        } else if (this.personVitalWeight == 0) {
            isEligible = false;
            this.vitaldone = false;
        } else {
            isEligible = true;
            this.vitaldone = true;
        }





        return isEligible;
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
    getHTSEligibility(): boolean {
        let isCCCEnrolled;
        if (this.enrolledServices && this.enrolledServices.length > 0) {
            isCCCEnrolled = this.enrolledServices.filter(obj => obj.serviceAreaId == 1);
        }
        let isEligible: boolean = false;

        if (this.encounterDetail != undefined) {
            if (this.encounterDetail.finalResult == 'Negative') {
                isEligible = true;

            } else if (this.encounterDetail.finalResult == 'Positive') {
                this.iscareend = true;
                isEligible = false;


            } else {
                isEligible = true;
            }


        } else {
            if (isCCCEnrolled != undefined) {
                if (isCCCEnrolled && isCCCEnrolled.length > 0) {
                    this.iscareend = true;


                    isEligible = false;
                } else {


                }
            }

        }

        return isEligible;


    }
    validationHTS(HTSEligible: Boolean): Boolean {

        let visibility = false;
        if (HTSEligible == false && this.iscareend == false) {
            visibility = true;
        } else {
            visibility = false;
        }

        return visibility;
    }
    validationTriage(vital: Boolean): Boolean {
        let visibility = false;

        if (vital == false && this.iscareend == false) {
            visibility = true;
        } else {
            visibility = false;
        }



        return visibility;
    }
    validationRiskAssessment(riskassessment: Boolean): Boolean {
        let visibility = false;

        if (riskassessment == false && this.iscareend == false) {
            visibility = true;
        } else {
            visibility = false;
        }


        return visibility;
    }
    validationCareEnded(careended: Boolean): Boolean {
        let visibility = false;

        if (careended == false) {
            visibility = true;
        } else {
            visibility = false;
        }


        return visibility;
    }





    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
            p => {
                this.person = p;




            },
            (err) => {
                this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }

    onPrepCheckIn() {
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

                        this.snotifyService.success('Successfully Checked-In Patient', 'CheckIn', this.notificationService.getConfig());
                        this.zone.run(() => {
                            this.router.navigate(['/prep/encounter/' + '/' + this.patientId + '/' + this.personId + '/'
                                + result['patientEncounterId'] + '/' + result['patientMasterVisitId']],
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
    doSearch() {
        this.prep_history_table_data = [];

        const prepEncounters = this.prepService.getPrepEncounterHistory(this.patientId, this.serviceAreaId
            , this.search.fromDate, this.search.toDate);
        prepEncounters.subscribe(
            (result) => {
                // console.log(result);
                result.forEach(arrayValue => {
                    this.prep_history_table_data.push({
                        'behaviourrisk': 'Risk',
                        encounterType: arrayValue.encounterType,
                        prep_status: arrayValue.preStatus,
                        next_appointment: arrayValue.appointmentDate,
                        provider: arrayValue.providerName,
                        encounterStartTime: arrayValue.encounterStartTime,
                        patientEncounterId: arrayValue.id,
                        patientMasterVisitId: arrayValue.patientMasterVisitId
                    });
                });
                this.dataSource = new MatTableDataSource(this.prep_history_table_data);
            },
            (error) => {
                console.log(error);
            }
        );
    }
    accessEncounter() {
        this.zone.run(() => {
            this.router.navigate(['/prep/prepformslist/' + '/' + this.patientId + '/' + this.personId + '/'
                + this.serviceAreaId],
                { relativeTo: this.route });
        });
    }

    onEdit(element) {
        if (element['encounterType'].toString() == 'PrEP Encounter') {
            this.zone.run(() => {
                this.router.navigate(['/prep/encounter/' + '/' + this.patientId + '/' + this.personId + '/'
                    + element['patientEncounterId'] + '/' + element['patientMasterVisitId'] + '/' + this.serviceAreaId + '/1'],
                    { relativeTo: this.route });
            });
        } else if (element['encounterType'].toString() === 'Behaviour Risk Assessment') {
            this.zone.run(() => {
                this.router.navigate(['/prep/riskassessment/' + '/' + this.patientId + '/' + this.personId + '/'
                    + this.serviceAreaId + '/' + element['patientMasterVisitId']],
                    { relativeTo: this.route });
            });

        } else if (element['encounterType'].toString() === 'MonthlyRefill') {
            this.zone.run(() => {
                this.router.navigate(['/prep/monthlyrefill/' + '/' + this.patientId + '/' + this.personId + '/'
                    + this.serviceAreaId + '/' + element['patientMasterVisitId']],
                    { relativeTo: this.route });
            });
        } else if (element['encounterType'].toString() === 'Care Ended') {
            this.zone.run(() => {
                this.router.navigate(['/prep/prepcareend/' + '/' + this.patientId + '/' + this.personId + '/'
                    + this.serviceAreaId + '/' + element['patientMasterVisitId'] + '/' + true],
                    { relativeTo: this.route });
            });
        }
    }

    onView(element) {
        alert('to be done');
    }
}

export interface PrepHistoryTableData {
    behaviourrisk?: string;
    encounterType?: string;
    prep_status?: string;
    next_appointment?: Date;
    provider?: string;
    encounterStartTime?: Date;
    patientEncounterId?: number;
    patientMasterVisitId?: number;
}
