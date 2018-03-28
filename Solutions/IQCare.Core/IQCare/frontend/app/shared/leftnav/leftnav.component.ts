import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import {Observable} from 'rxjs/Observable';
import * as Consent from '../reducers/app.states';

@Component({
  selector: 'app-leftnav',
  templateUrl: './leftnav.component.html',
  styleUrls: ['./leftnav.component.css']
})
export class LeftnavComponent implements OnInit {
    consent: boolean;
    isPositive: boolean;
    isReferred: boolean;
    hasConsentedPartnerListing: boolean;

    constructor(private store: Store<AppState>) {
        // this.store.dispatch(new Consent.ConsentTesting(true));
        store.pipe(select('app')).subscribe(res => {
            this.consent = res['consent'];
        });

        store.pipe(select('app' )).subscribe(res => {
            this.isPositive = res['isPositive'];
        });

        store.pipe(select('app')).subscribe(res => {
            this.hasConsentedPartnerListing = res['consentPartnerListing'];
        });

        store.pipe(select('app')).subscribe(res => {
            this.isReferred = res['isReferred'];
        });

        store.pipe(select('app')).subscribe(res => {
            console.log( 'isPositive', res);
        });
    }
    ngOnInit() {
    }
}
