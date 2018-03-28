import {Component, NgZone, OnInit} from '@angular/core';
import {Referral} from '../_models/referral';
import {LinkageReferralService} from '../_services/linkage-referral.service';
import {Tracing} from '../_models/tracing';
import {ActivatedRoute, Router} from '@angular/router';
import {Store} from '@ngrx/store';
import * as Consent from '../../shared/reducers/app.states';
declare var $: any;

@Component({
  selector: 'app-linkage-referral',
  templateUrl: './linkage-referral.component.html',
  styleUrls: ['./linkage-referral.component.css']
})
export class LinkageReferralComponent implements OnInit {
    referral: Referral;
    tracing: Tracing;
    tracingArray: Tracing[];
    tracingModeOptions: any[];
    tracingOutcomeOptions: any[];

    constructor(private _linkageReferralService: LinkageReferralService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>) {

    }

    ngOnInit() {
        this.referral = new Referral();
        this.tracing = new Tracing();
        this.tracingArray = [];

        this.getTracingOptions();

        const self = this;

        setTimeout(() => {
            $('#linkageWizard').on('actionclicked.fu.wizard', function(evt, data) {
                if (data.step === 1) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep1').parsley().destroy();
                        $('#datastep1').parsley({
                            excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden'
                        });

                        if ($('#datastep1').parsley().validate()) {
                            // validated
                        } else {
                            evt.preventDefault();
                            return;
                        }
                    }
                } else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        $('#datastep2').parsley().destroy();
                        $('#datastep2').parsley({
                            excluded: 'input[type=button], input[type=submit], input[type=reset], input[type=hidden], [disabled], :hidden'
                        });

                        if ($('#datastep2').parsley().validate()) {
                            /* submit all forms */
                            self.onSubmitForm();
                        } else {
                            console.log('Parseley Validated Error');
                            evt.preventDefault();
                            return;
                        }
                    }
                }
            })
                .on('changed.fu.wizard', function() {})
                .on('stepclicked.fu.wizard', function() {})
                .on('finished.fu.wizard', function(e) {});
        }, 0 );
    }

    onAddingTracing() {
        this.tracingArray.push(this.tracing);
        // console.log(this.tracingArray);
        this.tracing = new Tracing();
        /*Hide the modal after saving*/
        $('#tracingModal').modal('hide');
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
                }
            }

        }, err => {
            console.log(err);
        });
    }

    onSubmitForm() {
        this.referral.personId = JSON.parse(localStorage.getItem('personId'));
        // this.referral.facilityId = JSON.parse(localStorage.getItem('facilityId'));
        // this.referral.userId = JSON.parse(localStorage.getItem('userId'));
        this.referral.facilityId = 13050;
        this.referral.userId = 1;
        this.referral.serviceAreaId = 2;
        this.referral.referralReason = 1;
        this.referral.toFacility = 13050;

        this._linkageReferralService.addReferralTracing(this.referral, this.tracingArray).subscribe(data => {
            this.store.dispatch(new Consent.IsReferred(true));
            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
            console.log(err);
        });
    }

}
