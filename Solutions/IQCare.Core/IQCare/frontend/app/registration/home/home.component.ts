import { Component, OnInit } from '@angular/core';
import {EncounterService} from '../../hts/_services/encounter.service';
import {Observable} from 'rxjs/Observable';
import {DataSource} from '@angular/cdk/collections';
import * as Consent from '../../shared/reducers/app.states';
import {select, Store} from '@ngrx/store';
import {AppStateService} from '../../shared/_services/appstate.service';

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
        'resultTwo', 'finalResult', 'consent' , 'partnerListingConsent'];
    dataSource = new EncountersDataSource(this.encounterService, this.patientId);

    constructor(private encounterService: EncounterService,
                private store: Store<AppState>,
                private appStateService: AppStateService) {
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
            // console.log(data);
            /*this.store.dispatch(new Consent.IsEnrolled(true));
            for (let i = 0; i < data.length; i++) {
                if (data[i]['finalResult'] == 'Positive') {
                    this.countPositive += 1;
                    this.isPositive = true;
                    this.store.dispatch(new Consent.IsPositive(true));
                }

                if (data[i]['partnerListingConsent'] == 'Yes') {
                    this.store.dispatch(new Consent.ConsentPartnerListing(true));
                }
            }*/
            this.appStateService.initializeAppState();
        }, err => {
            console.log(err);
        });
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
