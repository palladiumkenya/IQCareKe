import { Component, NgZone, OnInit } from '@angular/core';
import { EncounterService } from '../../hts/_services/encounter.service';
import { Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';
import * as Consent from '../../shared/reducers/app.states';
import { select, Store } from '@ngrx/store';
import { AppStateService } from '../../shared/_services/appstate.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    providers: [PersonHomeService]
})
export class HomeComponent implements OnInit {
    patientId: number;
    personId: number;
    isPositive: boolean = false;
    ageInMonths: number;

    displayedColumns = ['encounterDate', 'testType', 'provider', 'resultOne',
        'resultTwo', 'finalResult', 'consent', 'partnerListingConsent', 'serviceArea', 'edit'];
    dataSource = new EncountersDataSource(this.encounterService, this.patientId);

    constructor(private encounterService: EncounterService,
        private store: Store<AppState>,
        private appStateService: AppStateService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private personService: PersonHomeService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        store.pipe(select('app')).subscribe(res => {
            localStorage.setItem('store', JSON.stringify(res));
        });

        store.pipe(select('app')).subscribe(res => {
            this.isPositive = res['isPositive'];
        });

        localStorage.removeItem('editEncounterId');
        localStorage.removeItem('viewEncounterId');
    }
    ngOnInit() {
        this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.dataSource = new EncountersDataSource(this.encounterService, this.patientId);
        this.getClientEncounters(this.patientId);
        this.ageInMonths = parseInt(localStorage.getItem('ageInMonths'), 10);
    }


    getClientEncounters(patientId: number) {
        this.encounterService.getEncounters(patientId).subscribe(data => {
            this.appStateService.initializeAppState();
        }, err => {
            this.snotifyService.error('Fetching hts encounters ' + err, 'HTS Encounter',
                this.notificationService.getConfig());
        });
    }

    onEdit(element) {
        localStorage.setItem('editEncounterId', element['encounterId']);

        this.zone.run(() => { this.router.navigate(['/hts'], { relativeTo: this.route }); });
    }

    onView(element) {
        // console.log(element);

        localStorage.setItem('viewEncounterId', element['encounterId']);
        this.zone.run(() => { this.router.navigate(['/hts/viewencounter'], { relativeTo: this.route }); });
    }
}


export class EncountersDataSource extends DataSource<any> {
    constructor(private encounterService: EncounterService, private patientId: number) {
        super();
    }

    connect(): Observable<any[]> {
        return this.encounterService.getEncounters(this.patientId);
    }

    disconnect() {

    }
}
