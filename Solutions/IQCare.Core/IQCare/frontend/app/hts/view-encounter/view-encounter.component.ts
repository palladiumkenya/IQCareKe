import { Component, NgZone, OnInit } from '@angular/core';
import { EncounterService } from '../_services/encounter.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NotificationService } from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { EncounterDetails } from '../_models/encounterDetails';

@Component({
    selector: 'app-view-encounter',
    templateUrl: './view-encounter.component.html',
    styleUrls: ['./view-encounter.component.css']
})
export class ViewEncounterComponent implements OnInit {
    encounterDetail: EncounterDetails;

    constructor(private _encounterService: EncounterService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.encounterDetail = new EncounterDetails();
        this.getEncounterDetails();
    }


    getEncounterDetails() {
        this._encounterService.getEncounterDetails(JSON.parse(localStorage.getItem('viewEncounterId'))).subscribe((res) => {
            // console.log(res[0]);

            this.encounterDetail = { ...this.encounterDetail, ...res[0] };
            const personId = res[0]['personId'];

            this.getClientDisabilities(personId);
            // console.log(this.encounterDetail);
        }, (err) => {
            this.snotifyService.error('Error loading encounter ' + err, 'Encounter', this.notificationService.getConfig());
        });
    }

    public getClientDisabilities(personId: number) {
        this._encounterService.getClientDisability(personId).subscribe((res) => {
            // console.log(res);
            if (res.length > 0) {
                this.encounterDetail.HasDisability = 'Yes';
            } else {
                this.encounterDetail.HasDisability = 'No';
            }
        }, (err) => {
            this.snotifyService.error('Error loading disabilities ' + err, 'Disability', this.notificationService.getConfig());
        });
    }
}
