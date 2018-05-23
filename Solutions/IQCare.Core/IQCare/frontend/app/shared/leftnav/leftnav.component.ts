import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
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
    isEnrolled: boolean;

    constructor(private store: Store<AppState>) {
        // this.store.dispatch(new Consent.ConsentTesting(true));
        store.pipe(select('app')).subscribe(res => {
            if (res['consent'])
                this.consent = res['consent'];
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isPositive'])
                this.isPositive = res['isPositive'];
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['consentPartnerListing']) {
                this.hasConsentedPartnerListing = res['consentPartnerListing'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isReferred'])
                this.isReferred = res['isReferred'];
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isEnrolled'])
                this.isEnrolled = res['isEnrolled'];
        });
    }

    ngOnInit() {
    }
}
