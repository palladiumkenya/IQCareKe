import { Component, OnInit, NgZone, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { OvcService } from '../../_services/ovc.service';
import { ActivatedRoute, Router } from '@angular/router';

import { Enrollment } from '../../../registration/_models/enrollment';
import { MatDialog, MatDialogConfig, MatPaginator } from '@angular/material';
import { InlineSearchComponent } from '../../../records/inline-search/inline-search.component';
import { RecordsService } from '../../../records/_services/records.service';
import { Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';
import { NgxSpinnerService } from 'ngx-spinner';


@Component({
    selector: 'app-ovc-encounter',
    templateUrl: './ovc-encounter.component.html',
    styleUrls: ['./ovc-encounter.component.css']
})
export class OvcEncounterComponent implements OnInit {

    personId: number;
    patientId: number;
    serviceCode: string;
    serviceId: number;
    userId: number;

    constructor(
        private _formBuilder: FormBuilder,
        private ovcService: OvcService,
        private dialog: MatDialog,
        private notificationService: NotificationService,
        private snotifyService: SnotifyService,
        private route: ActivatedRoute,

        private spinner: NgxSpinnerService,
        public zone: NgZone,
        private router: Router
    ) { }

    ngOnInit() {



        this.route.params.subscribe(
            p => {
                const { personId, patientId, serviceId } = p;
                this.personId = personId;

                this.serviceId = serviceId;
                this.patientId = patientId;
                localStorage.setItem('patientId', this.patientId.toString());

            }
        );
    }

    Back() {
        this.zone.run(() => {
            this.router.navigate(
                ['/dashboard/personhome/' + this.personId],
                { relativeTo: this.route });
        });
    }

    OVCTerminationLink() {

        this.zone.run(() => {
            this.router.navigate(['/ccc/ovccareend' + '/' + this.patientId + '/' + this.personId + '/'
                + this.serviceId],
                { relativeTo: this.route });
        });

    }

}
