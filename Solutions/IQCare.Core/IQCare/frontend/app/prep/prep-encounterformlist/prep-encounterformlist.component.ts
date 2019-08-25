import { Component, OnInit, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { PersonView } from '../../records/_models/personView';
import { SearchService } from '../../registration/_services/search.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientMasterVisitEncounter } from '../../pmtct/_models/PatientMasterVisitEncounter';
import { Subscription } from 'rxjs';
import { EncounterService } from '../../shared/_services/encounter.service';
import { PrepService } from '../_services/prep.service';

import { PrepCheckinComponent } from './../prep-checkin/prep-checkin.component';
import { MatTableDataSource, MatDialog, MatDialogConfig } from '@angular/material';

import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import * as moment from 'moment';
@Component({
    selector: 'app-prep-encounterformlist',
    templateUrl: './prep-encounterformlist.component.html',
    styleUrls: ['./prep-encounterformlist.component.css'],
    providers: [PersonHomeService, SearchService]
})
export class PrepEncounterformlistComponent implements OnInit {
    personId: number;
    public personView$: Subscription;
    Encounterformlistgroup: FormGroup;
    prepEncounterType: LookupItemView[];
    EncounterFormList: any[] = [];
    riskassessmentvisits: any[] = [];
    public person: PersonView;
    patientId: number;
    userId: number;
    serviceAreaId: number;
    enrolledServices: any[] = [];
    patientIdentifiers: any[];
    identifiers: any[] = [];
    services: any[] = [];
    constructor(
        private notificationService: NotificationService,
        private dialog: MatDialog,
        private prepService: PrepService,
        private personService: PersonHomeService,
        private route: ActivatedRoute,
        private encounterService: EncounterService,
        private snotifyService: SnotifyService,
        private searchService: SearchService,
        private _formBuilder: FormBuilder,
        public zone: NgZone,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.params.subscribe((params) => {
            const { personId, patientId, serviceId } = params;
            this.patientId = patientId;
            this.personId = personId;
            this.serviceAreaId = serviceId;
        });

        this.route.data.subscribe(
            (res) => {
                const { prepEncounterTypeOption } = res;
                this.prepEncounterType = prepEncounterTypeOption;
            });

        this.EncounterFormList = [];
        this.InitEncounterFormsList();
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.getPatientDetailsById(this.personId);
        this.getAllServices();
        this.getPersonEnrolledServices(this.personId);
        this.GetPrepRiskassessmentVisits();
        console.log(this.riskassessmentvisits);

        this.Encounterformlistgroup = this._formBuilder.group({
           // encounterforms: new FormControl('', [Validators.required]),
        });


       

    }

    Back() {
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/prep/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                    { relativeTo: this.route });
            });
        });
    }
    InitEncounterFormsList() {
        this.EncounterFormList.push({
            value: 'vitals',
            displayname: 'VITALS',
            disabled: false
        });

        this.EncounterFormList.push({
            value: 'hts',
            displayname: 'HTS',
            disabled: false
        });

        this.EncounterFormList.push({
            value: 'riskassessment',
            displayname: 'RISK ASSESSMENT FORM',
            disabled: false
        });

        this.EncounterFormList.push({
            value: 'prepencounter',
            displayname: 'PREP ENCOUNTER',
            disabled: false
        });


        this.EncounterFormList.push({
            value: 'monthlyrefill',
            displayname: 'MONTHLY REFILL',
            disabled: false
        });
        this.EncounterFormList.push({
            value: 'preptermination',
            displayname: 'PREP DISCONTINUATION FORM',
            disabled: false
        });





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
    GetPrepRiskassessmentVisits() {

        this.prepService.GetRiskAssessmentDetails(this.personId).subscribe(res => {
            if (res.length == 0)
                return;

            res.forEach(test => {
                this.riskassessmentvisits.push({
                    patientId: test.patientId,
                    patientMasterVisitId: test.patientMasterVisitId,
                    VisitDate: moment(test.visitDate).format('DD-MMM-YYYY'),
                    Clientwillingtakeprep: test.clientWillingTakingPrep,
                    AssessmentOutCome: test.assessmentOutCome
                });
            });

            if (this.riskassessmentvisits.length > 0) {
                if (this.riskassessmentvisits[0].Clientwillingtakeprep != null) {
                    let clienttakeprep: string;
                    clienttakeprep = this.riskassessmentvisits[0].Clientwillingtakeprep;
                    if (clienttakeprep.toString().toLowerCase() == 'no') {
                        this.EncounterFormList.forEach(x => {
                            if (x.value !== 'preptermination') {
                                x.disabled = true;
                            }
                        });
                    }


                }

                if (this.riskassessmentvisits[0].AssessmentOutCome != null) {
                    let assessmentoutcome: string;

                    assessmentoutcome = this.riskassessmentvisits[0].AssessmentOutCome;
                    if (assessmentoutcome.toString().toLowerCase() == 'norisk') {
                        this.EncounterFormList.forEach(x => {
                            if (x.value !== 'preptermination') {
                                x.disabled = true;
                            }
                        });
                    }

                }
            }



        }, (error) => {
            console.log(error + 'An error occurred loading risk assessment details');
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
    clickEncounter() {
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/prep/prepfollowupworkflow/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                    { relativeTo: this.route });
            });
        });

    }
    monthlyrefillencounter(){
        this.zone.run(() => {
            this.zone.run(() => {
                this.router.navigate(
                    ['/prep/prepmonthlyrefillworkflow/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                    { relativeTo: this.route });
            });
        });
    }

}
