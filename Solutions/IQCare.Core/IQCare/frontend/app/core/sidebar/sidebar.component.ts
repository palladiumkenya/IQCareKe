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
    personId: number;
    patientId: number;

    constructor(private store: Store<AppState>) {
        store.pipe(select('app')).subscribe(res => {
            if (res['service']) {
                this.selectedService = res['service'];
            } else {
                this.selectedService = '';
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['consent']) {
                this.consent = res['consent'];
            } else {
                this.consent = false;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isPositive']) {
                this.isPositive = res['isPositive'];
            } else {
                this.isPositive = false;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['consentPartnerListing']) {
                this.hasConsentedPartnerListing = res['consentPartnerListing'];
            } else {
                this.hasConsentedPartnerListing = false;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isReferred']) {
                this.isReferred = res['isReferred'];
            } else {
                this.isReferred = false;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['isEnrolled']) {
                this.isEnrolled = res['isEnrolled'];
            } else {
                this.isEnrolled = false;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['PersonId']) {
                this.personId = res['PersonId'];
            } else {
                this.personId = 0;
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (res['PatientId']) {
                this.patientId = res['PatientId'];
            } else {
                this.patientId = 0;
            }
        });
    }

    ngOnInit() {
    }

}
