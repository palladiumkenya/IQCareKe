import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
    selectedService: string;
    consent: boolean;
    isPositive: boolean;
    isReferred: boolean;
    hasConsentedPartnerListing: boolean;
    isEnrolled: boolean;

    constructor(private store: Store<AppState>) {
        this.selectedService = localStorage.getItem('selectedService');

        store.pipe(select('app')).subscribe(res => {
            if (res['consent']) {
                this.consent = res['consent'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isPositive']) {
                this.isPositive = res['isPositive'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['consentPartnerListing']) {
                this.hasConsentedPartnerListing = res['consentPartnerListing'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isReferred']) {
                this.isReferred = res['isReferred'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isEnrolled']) {
                this.isEnrolled = res['isEnrolled'];
            }
        });
    }

    ngOnInit() {
    }

}
