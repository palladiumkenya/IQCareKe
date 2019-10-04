import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { PersonHomeService } from '../services/person-home.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { PersonView } from '../../records/_models/personView';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import * as Consent from '../../shared/reducers/app.states';
import { Store } from '@ngrx/store';
import { EncounterDetails } from '../_model/HtsEncounterdetails';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { LookupItemService } from '../../shared/_services/lookup-item.service';

import {AddWaitingListComponent} from '../../shared/add-waiting-list/add-waiting-list.component';
@Component({

    selector: 'app-person-home',
    templateUrl: './person-home.component.html',
    styleUrls: ['./person-home.component.css']
})
export class PersonHomeComponent implements OnInit {

    [x: string]: any;
    public carended: boolean;
    public isdead: boolean;
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
    exitreason: number;
    patientId: number;
    careenddetails: any[] = [];
    htshistory: any[] = [];
    personvitals: any[];
    careendoptions: LookupItemView[] = [];
    chronic_illness_data: any[] = [];
    dataSource = new MatTableDataSource(this.chronic_illness_data);
    chronic_illness_displaycolumns = ['illness', 'onsetdate', 'treatment', 'dose'];
    constructor(private route: ActivatedRoute,
        private personService: PersonHomeService,
        private lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private router: Router,
        public zone: NgZone,
        private dialog: MatDialog,
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
            const { ExitReasonsArray } = res;
            const { CarendedArray } = res;
            const { HTSEncounterHistoryArray } = res;

            this.careenddetails = CarendedArray;

            this.htshistory = HTSEncounterHistoryArray;
            this.services = servicesArray;            
            this.htsencounters = HTSEncounterArray;
            this.personvitals = PersonVitalsArray;
            this.riskassessmentencounter = RiskAssessmentArray;
            this.careendoptions = ExitReasonsArray['lookupItems'];


            if (this.personvitals.length > 0) {
                this.personVitalWeight = this.personvitals['0'].weight;
            }
            if (this.careenddetails != null) {
                this.exitreason = this.careenddetails['exitReason'];

                let careendeddetails: string;
                let val: number;

                val = this.careendoptions.findIndex(x => x.itemId == this.exitreason);
                if (this.careendoptions[val])
                careendeddetails = this.careendoptions[val].itemDisplayName;

                if (careendeddetails && careendeddetails.toLowerCase() == 'death') {
                    this.isdead = true;
                    this.carended = true;
                } else {
                    this.carended = true;
                    this.isdead = false;
                }
            } else {
                this.carended = false;
                this.isdead = false;
            }
            this.riskencounters = this.riskassessmentencounter['encounters'];
        });

        this.encounterDetail = this.htsencounters[0];

        const servicesRightOrder = [ 2, 1, 3, 5, 4, 6, 7 ];
        const ordered_array = this.mapOrder(this.services, servicesRightOrder, 'id');   
        this.services = ordered_array;
        
        localStorage.removeItem('patientEncounterId');
        localStorage.removeItem('patientMasterVisitId');
        localStorage.removeItem('selectedService');
        this.store.dispatch(new Consent.ClearState());
        this.getPatientDetailsById(this.personId);
    }

    mapOrder (array, order, key) {

        array.sort( function (a, b) {
            const A = a[key];
            const B = b[key];

            if (order.indexOf(A) > order.indexOf(B)) {
                return 1;
            } else {
                return -1;
            }

        });

        return array;
    }

    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personService.getPatientByPersonId(personId).subscribe(
            p => {
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

    addWaitingList() {
        const PersonId = this.person.personId;
        const PatientId = this.person.patientId;
      


        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;
        resultsDialogConfig.height = '100%';
        resultsDialogConfig.width = '100%';


        resultsDialogConfig.data = {
            patientId: PatientId,
            personId: PersonId
        };

        const dialogRef = this.dialog.open(AddWaitingListComponent, resultsDialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                console.log(data);
            });


    }

   
}
