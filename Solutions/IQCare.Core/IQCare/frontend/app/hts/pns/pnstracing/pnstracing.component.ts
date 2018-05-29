import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import * as Consent from '../../../shared/reducers/app.states';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { AppEnum } from '../../../shared/reducers/app.enum';
import { AppStateService } from '../../../shared/_services/appstate.service';
import { PnstracingService } from '../../_services/pnstracing.service';
import { PnsTracing } from '../../_models/pnstracing';

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

    form: FormGroup;

    constructor(private pnsTracingService: PnstracingService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private store: Store<AppState>,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private appStateService: AppStateService,
        private fb: FormBuilder) {
        this.maxDate = new Date();
        this.minDate = new Date();
    }

    ngOnInit() {
        this.pnsTracing = new PnsTracing();
        this.yesNoOptions = [];
        this.tracingModeOptions = [];
        this.pnsTracingOutcome = [];

        this.getTracingOptions();

        this.form = this.fb.group({
            TracingDate: new FormControl(this.pnsTracing.TracingDate, [Validators.required]),
            TracingOutcome: new FormControl(this.pnsTracing.TracingOutcome, [Validators.required]),
            DateBookedTesting: new FormControl(this.pnsTracing.DateBookedTesting, [Validators.required]),
            TracingMode: new FormControl(this.pnsTracing.TracingMode, [Validators.required]),
            Consent: new FormControl(this.pnsTracing.Consent, [Validators.required])
        });
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
        if (this.form.valid) {
            this.pnsTracing = { ...this.form.value };
            this.pnsTracing.PersonId = JSON.parse(localStorage.getItem('partnerId'));
            this.pnsTracing.UserId = JSON.parse(localStorage.getItem('appUserId'));

            console.log(this.pnsTracing);

            const tracingTypeValue = this.tracingTypeOptions.filter(function (obj) {
                return obj.itemName == 'Partner';
            });

            const tracingType = tracingTypeValue[0]['itemId'];
            this.pnsTracing.TracingType = tracingType;

            this.pnsTracingService.addPnsTracing(this.pnsTracing).subscribe(data => {
                const partnerPnsTraced = {
                    'partnerId': this.pnsTracing.PersonId,
                    'pnsTraced': true
                };
                this.store.dispatch(new Consent.IsPnsTracingDone(JSON.stringify(partnerPnsTraced)));
                this.appStateService.addAppState(AppEnum.PNS_TRACING, JSON.parse(localStorage.getItem('personId')),
                    JSON.parse(localStorage.getItem('patientId')), null, null, JSON.stringify({
                        'partnerId': this.pnsTracing.PersonId,
                        'pnsTraced': true
                    })).subscribe();

                this.snotifyService.success('Successful saving PNS screening',
                    'PNS Tracing', this.notificationService.getConfig());

                this.zone.run(() => { this.router.navigate(['/hts/pns/pnslist'], { relativeTo: this.route }); });
            }, err => {
                this.snotifyService.error('Error saving PNS tracing' + err,
                    'PNS Tracing', this.notificationService.getConfig());
                console.log(err);
            });
        } else {
            return;
        }
    }

    onConsentChange() {
        const consentValue = this.form.controls.Consent.value;
        const selectedOption = this.yesNoOptions.filter(function (obj) {
            return obj.itemId == consentValue;
        });

        if (selectedOption[0]['itemName'] == 'No') {
            this.form.controls.DateBookedTesting.disable({ onlySelf: true });
            this.form.controls.DateBookedTesting.setValue('');
        } else {
            this.form.controls.DateBookedTesting.enable({ onlySelf: false });
        }
    }

    onTracingOutcomeChange() {
        const tracingOutcomeValue = this.form.controls.TracingOutcome.value;
        const selectedOption = this.pnsTracingOutcome.filter(function (obj) {
            return obj.itemId == tracingOutcomeValue;
        });

        if (selectedOption[0]['itemName'] == 'Not Contacted') {
            // disable consent since they were not contacted
            this.form.controls.Consent.disable({ onlySelf: true });
            this.form.controls.Consent.setValue('');
            // disable date booked for testing since they were not contacted
            this.form.controls.DateBookedTesting.disable({ onlySelf: true });
            this.form.controls.DateBookedTesting.setValue('');
        } else {
            // re-enable
            this.form.controls.Consent.enable({ onlySelf: false });
            this.form.controls.DateBookedTesting.enable({ onlySelf: false });
        }
    }

}
