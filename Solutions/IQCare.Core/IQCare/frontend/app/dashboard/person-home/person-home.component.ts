import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { PersonHomeService } from '../services/person-home.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { PersonView } from '../../records/_models/personView';
import { MatTableDataSource } from '@angular/material';
import * as Consent from '../../shared/reducers/app.states';
import { Store } from '@ngrx/store';
import { EncounterDetails } from '../_model/HtsEncounterdetails';

@Component({
    selector: 'app-person-home',
    templateUrl: './person-home.component.html',
    styleUrls: ['./person-home.component.css']
})
export class PersonHomeComponent implements OnInit {

    [x: string]: any;

    public personId = 0;
    public personVitalWeight = 0;
    public person: PersonView;
    public personView$: Subscription;
    public personAllergies$: Subscription;
    public personAllergies: any;
    encounterDetail: EncounterDetails;
    htsencounters: any[];
    riskassessmentencounter: any[];
    riskencounters: any[];
    services: any[];
    personvitals: any[];
    chronic_illness_data: any[] = [];
    dataSource = new MatTableDataSource(this.chronic_illness_data);
    chronic_illness_displaycolumns = ['illness', 'onsetdate', 'treatment', 'dose'];
    constructor(private route: ActivatedRoute,
        private personService: PersonHomeService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private router: Router,
        public zone: NgZone,
        private store: Store<AppState>) {
        this.person = new PersonView();
        this.encounterDetail = new EncounterDetails();
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.personId = params['id'];
        });

        this.route.data.subscribe(res => {

            const { servicesArray } = res;
            const { HTSEncounterArray } = res;
            const { PersonVitalsArray } = res;
            const { RiskAssessmentArray } = res;
            console.log(HTSEncounterArray);
            this.services = servicesArray;
            this.htsencounters = HTSEncounterArray;
            this.personvitals = PersonVitalsArray;
            this.riskassessmentencounter = RiskAssessmentArray;
            if (this.personvitals.length > 0) {
                this.personVitalWeight = this.personvitals['0'].weight;
                console.log(this.personVitalWeight + 'Correct Weight');
            }


            this.riskencounters = this.riskassessmentencounter['encounters']
        });

        this.encounterDetail = this.htsencounters[0];
        /*console.log("HTSEncounters");
        console.log(this.htsencounters[0]);
        console.log(this.encounterDetail);*/


        localStorage.removeItem('patientEncounterId');
        localStorage.removeItem('patientMasterVisitId');
        localStorage.removeItem('selectedService');
        this.store.dispatch(new Consent.ClearState());

        // console.log('personId' + this.personId);
        this.getPatientDetailsById(this.personId);


    }



    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
            p => {
                // console.log(p);
                this.person = p;

                localStorage.setItem('personId', this.person.personId.toString());
                this.store.dispatch(new Consent.PersonId(this.person.personId));

                if (this.person.patientId && this.person.patientId > 0) {
                    this.store.dispatch(new Consent.PatientId(this.person.patientId));
                    localStorage.setItem('patientId', this.person.patientId.toString());
                }
            },
            (err) => {
                this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }


    onEdit() {
        this.zone.run(() => {
            this.router.navigate(['/record/person/update/' + this.personId], { relativeTo: this.route });
        });
    }
    getUserAllergies(patientId: number) {
        this.personAllergies$ = this.personService.getPatientAllergies(patientId).subscribe(
            personAllergies => {
                console.log(personAllergies);
                this.personAllergies = personAllergies;
            },
            (err) => {
                this.snotifyService.error('Error retreaving person allergies...',
                    this.notificationService.getConfig());
            },
            () => {
                // console.log(this.personView$);
            });
    }
}
