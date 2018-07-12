import {Component, NgZone, OnInit} from '@angular/core';
import {EncounterService} from '../../hts/_services/encounter.service';
import {Observable} from 'rxjs';
import {DataSource} from '@angular/cdk/collections';
import * as Consent from '../../shared/reducers/app.states';
import {select, Store} from '@ngrx/store';
import {AppStateService} from '../../shared/_services/appstate.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    patientId: number;
    countPositive: number;
    isPositive: boolean = false;

    displayedColumns = ['encounterDate', 'testType', 'provider', 'resultOne',
        'resultTwo', 'finalResult', 'consent' , 'partnerListingConsent', 'edit'];
    dataSource = new EncountersDataSource(this.encounterService, this.patientId);

    constructor(private encounterService: EncounterService,
                private store: Store<AppState>,
                private appStateService: AppStateService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) {
        store.pipe(select('app')).subscribe(res => {
            localStorage.setItem('store', JSON.stringify(res));
        });
    }
    ngOnInit() {
        this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.dataSource = new EncountersDataSource(this.encounterService, this.patientId);
        this.getClientEncounters(this.patientId);
    }


    getClientEncounters(patientId: number) {
        this.encounterService.getEncounters(patientId).subscribe(data => {
            this.appStateService.initializeAppState();
        }, err => {
            console.log(err);
        });
    }

    onEdit(element) {
        localStorage.setItem('editEncounterId', element['encounterId']);

        this.zone.run(() => { this.router.navigate(['/hts'], { relativeTo: this.route }); });
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
