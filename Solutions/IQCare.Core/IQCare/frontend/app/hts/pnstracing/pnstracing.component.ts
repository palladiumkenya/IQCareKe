import {Component, NgZone, OnInit} from '@angular/core';
import {PnstracingService} from '../_services/pnstracing.service';
import {PnsTracing} from '../_models/pnstracing';
import {ActivatedRoute, Router} from '@angular/router';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import {NotificationService} from '../../shared/_services/notification.service';
import {SnotifyService} from 'ng-snotify';

@Component({
  selector: 'app-pnstracing',
  templateUrl: './pnstracing.component.html',
  styleUrls: ['./pnstracing.component.css']
})
export class PnsTracingComponent implements OnInit {
    pnsTracing: PnsTracing;
    yesNoOptions: any[];
    tracingModeOptions: any[];
    pnsTracingOutcome: any[];
    tracingTypeOptions: any[];

    maxDate: any;
    minDate: any;

    constructor(private pnsTracingService: PnstracingService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService) {
        this.maxDate = new Date();
        this.minDate = new Date();
    }

    ngOnInit() {
        this.pnsTracing = new PnsTracing();
        this.yesNoOptions = [];
        this.tracingModeOptions = [];
        this.pnsTracingOutcome = [];

        this.getTracingOptions();
    }

    public getTracingOptions() {
        this.pnsTracingService.getTracingOptions().subscribe(data => {
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'TracingMode') {
                    this.tracingModeOptions = options[i].value;
                } else if (options[i].key == 'PnsTracingOutcome') {
                    this.pnsTracingOutcome = options[i].value;
                } else if (options[i].key == 'TracingType') {
                    this.tracingTypeOptions = options[i].value;
                }
            }
        });
    }

    onSubmit() {
        this.pnsTracing.PersonId = JSON.parse(localStorage.getItem('partnerId'));
        this.pnsTracing.UserId = JSON.parse(localStorage.getItem('appUserId'));

        console.log(this.pnsTracing);

        const tracingTypeValue = this.tracingTypeOptions.filter(function (obj) {
            return obj.itemName == 'Partner';
        });

        const tracingType = tracingTypeValue[0]['itemId'];
        this.pnsTracing.TracingType = tracingType;

        this.pnsTracingService.addPnsTracing(this.pnsTracing).subscribe(data => {
            console.log(data);
            const partnerPnsTraced = {
                'partnerId': this.pnsTracing.PersonId,
                'pnsTraced': true
            };
            this.store.dispatch(new Consent.IsPnsTracingDone(JSON.stringify(partnerPnsTraced)));

            this.snotifyService.success('Successful saving PNS screening',
                'PNS Tracing', this.notificationService.getConfig());

            this.zone.run(() => { this.router.navigate(['/hts/pns'], {relativeTo: this.route }); });
        }, err => {
            this.snotifyService.error('Error saving PNS tracing' + err,
                'PNS Tracing', this.notificationService.getConfig());
            console.log(err);
        });
    }
}
