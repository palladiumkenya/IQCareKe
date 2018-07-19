import { MatDialogConfig, MatDialog } from '@angular/material';
import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Referral } from '../_models/referral';
import { LinkageReferralService } from '../_services/linkage-referral.service';
import { Tracing } from '../_models/tracing';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { AppStateService } from '../../shared/_services/appstate.service';
import { AppEnum } from '../../shared/reducers/app.enum';
import { TracingComponent } from '../tracing/tracing.component';

@Component({
    selector: 'app-linkage-referral',
    templateUrl: './linkage-referral.component.html',
    styleUrls: ['./linkage-referral.component.css']
})
export class LinkageReferralComponent implements OnInit {
    form: FormGroup;
    referral: Referral;
    tracing: Tracing;
    tracingArray: Tracing[];
    tracingModeOptions: any[];
    tracingOutcomeOptions: any[];
    tracingTypeOptions: any[];
    facilities: any[];
    filteredOptions: Observable<any[]>;
    myControl: FormControl = new FormControl();
    referralReasons: any[];
    minDateEnrolled: any;

    isEdit: boolean = false;

    constructor(private _linkageReferralService: LinkageReferralService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private store: Store<AppState>,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private appStateService: AppStateService,
        private _formBuilder: FormBuilder,
        private dialog: MatDialog) {

        this.minDateEnrolled = new Date();

        this.myControl.valueChanges.pipe(
            debounceTime(400)
        ).subscribe(data => {
            this._linkageReferralService.filterFacilities(data).subscribe(res => {
                this.filteredOptions = res['facilityList'];
            });
        });
    }

    ngOnInit() {
        this.referral = new Referral();
        this.tracing = new Tracing();
        this.tracingArray = [];

        this.form = this._formBuilder.group({
            dateToBeEnrolled: new FormControl(this.referral.dateToBeEnrolled, [Validators.required])
        });

        // Fetch previous referral if it exists
        this.getClientReferral();

        this.getTracingOptions();
        this.getReferralReasons();
    }

    getClientReferral() {
        const personId = JSON.parse(localStorage.getItem('personId'));
        this._linkageReferralService.getClientReferral(personId).subscribe(
            (res) => {
                if (res.length > 0) {
                    this.form.controls.dateToBeEnrolled.setValue(res[0]['referralDate']);
                    this._linkageReferralService.getFacility(res[0]['toFacility']).subscribe(
                        (result) => {
                            if (result.length > 0) {
                                console.log(result);
                                this.filteredOptions = result;
                                this.myControl.setValue(result[0]);
                            }
                        }
                    );
                    this.isEdit = true;
                } else {
                    this.isEdit = false;
                }
            },
            (error) => {

            }
        );
    }

    newTrace() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '75%';
        dialogConfig.width = '60%';

        dialogConfig.data = {
            tracingMode: this.tracingModeOptions,
            tracingOutcome: this.tracingOutcomeOptions
        };

        const dialogRef = this.dialog.open(TracingComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                this.tracing.tracingDate = data.tracingDate;
                this.tracing.outcome = data.outcome;
                this.tracing.mode = data.mode;

                this.tracingArray.push(this.tracing);
                this.tracing = new Tracing();
            }
        );
    }

    getReferralReasons() {
        this._linkageReferralService.getReferralReasons().subscribe(res => {
            console.log(res);
            this.referralReasons = res['lookupItems'][0]['value'];
        }, err => {

        });
    }

    displayFn(facility?: any): string | undefined {
        return facility ? facility.name : undefined;
    }

    getTracingOptions() {
        this._linkageReferralService.getTracingOptions().subscribe(data => {
            // console.log(data);
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'TracingMode') {
                    this.tracingModeOptions = options[i].value;
                } else if (options[i].key == 'TracingOutcome') {
                    this.tracingOutcomeOptions = options[i].value;
                } else if (options[i].key == 'TracingType') {
                    this.tracingTypeOptions = options[i].value;
                }
            }

        }, err => {
            console.log(err);
        });
    }

    onSubmitForm() {
        console.log(this.myControl);
        if (!this.form.valid) {
            return;
        }

        if (!this.myControl.value || !this.myControl.value.hasOwnProperty('mflCode')) {
            this.snotifyService.error('Please select a valid facility', 'Referral', this.notificationService.getConfig());
            return;
        }

        this.referral.personId = JSON.parse(localStorage.getItem('personId'));
        this.referral.facilityId = JSON.parse(localStorage.getItem('appPosID'));
        this.referral.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.referral.serviceAreaId = 2;
        this.referral.referredTo = this.myControl.value.mflCode;
        this.referral.dateToBeEnrolled = this.form.value.dateToBeEnrolled;

        const optionSelected = this.referralReasons.filter(function (obj) {
            return obj.itemName == 'CCCEnrollment';
        });

        const tracingTypeValue = this.tracingTypeOptions.filter(function (obj) {
            return obj.itemName == 'Enrolment';
        });

        this.referral.referralReason = optionSelected[0]['itemId'];
        const tracingType = tracingTypeValue[0]['itemId'];


        if (this.isEdit) {
            this.addNewReferralTracing(tracingType, true);
        } else {
            this.addNewReferralTracing(tracingType, false);
        }
    }

    addNewReferralTracing(tracingType: any, isEdit: boolean) {
        this._linkageReferralService.addReferralTracing(this.referral, this.tracingArray, tracingType, isEdit).subscribe(data => {
            this.store.dispatch(new Consent.IsReferred(true));

            this.store.pipe(select('app')).subscribe(res => {
                localStorage.setItem('store', JSON.stringify(res));
            });

            this.appStateService.addAppState(AppEnum.IS_REFERRED, this.referral.personId,
                JSON.parse(localStorage.getItem('patientId')), null, null).subscribe();

            this.snotifyService.success('Successfully Referral', 'Referral', this.notificationService.getConfig());

            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
            console.log(err);
            this.snotifyService.error('Error saving referral ' + err, 'Referral', this.notificationService.getConfig());
        });
    }

}
