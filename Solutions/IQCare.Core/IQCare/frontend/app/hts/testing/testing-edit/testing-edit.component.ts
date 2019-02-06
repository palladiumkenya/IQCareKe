import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, NgZone } from '@angular/core';
import { EncounterService } from '../../_services/encounter.service';
import { Store, select } from '@ngrx/store';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-testing-edit',
    templateUrl: './testing-edit.component.html',
    styleUrls: ['./testing-edit.component.css']
})
export class TestingEditComponent implements OnInit {
    form: FormGroup;
    patientId: number;
    patientMasterVisitId: number;
    htsEncounterId: number;
    isCoupleDiscordantDisabled: boolean = false;

    hivTestKits: LookupItemView[];
    hivResultsOptions: LookupItemView[];
    hivFinalResultsOptions: LookupItemView[];
    yesNoOptions: LookupItemView[];
    yesNoNA: LookupItemView[];
    reasonsDeclined: LookupItemView[];

    constructor(private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private _encounterService: EncounterService,
        private store: Store<AppState>,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) {
        this.store.pipe(select('app')).subscribe(res => {
            this.isCoupleDiscordantDisabled = res['testedAs'];
        });
    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                // console.log(params);
                const { htsEncounterId, patientId, patientMasterVisitId } = params;
                this.htsEncounterId = htsEncounterId;
                this.patientId = patientId;
                this.patientMasterVisitId = patientMasterVisitId;
            }
        );

        this.form = this.fb.group({
            finalResultHiv1: new FormControl('', [Validators.required]),
            finalResultHiv2: new FormControl('', [Validators.required]),
            finalResult: new FormControl('', [Validators.required]),
            finalResultGiven: new FormControl('', [Validators.required]),
            coupleDiscordant: new FormControl('', [Validators.required]),
            acceptedPartnerListing: new FormControl('', [Validators.required]),
            reasonsDeclinePartnerListing: new FormControl('', [Validators.required]),
            finalResultsRemarks: new FormControl()
        });

        // this.form.get('finalResult').disable({ onlySelf: false });

        this._encounterService.getCustomOptions().subscribe(data => {
            const options = data['lookupItems'];

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'HIVResults') {
                    this.hivResultsOptions = options[i].value;
                } else if (options[i].key == 'HIVTestKits') {
                    // this.hivTestKits = options[i].value;
                } else if (options[i].key == 'HIVFinalResults') {
                    this.hivFinalResultsOptions = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'YesNoNA') {
                    this.yesNoNA = options[i].value;
                } else if (options[i].key == 'ReasonsPartner') {
                    this.reasonsDeclined = options[i].value;
                }
            }
        });

        this._encounterService.getEncounter(this.htsEncounterId).subscribe(
            (res) => {
                // console.log(res);
                const { encounter, htsResults, consentToListPartners } = res;

                if (htsResults.length > 0) {
                    this.form.get('finalResultHiv1').setValue(htsResults[0].roundOneTestResult);
                    this.form.get('finalResultHiv2').setValue(htsResults[0].roundTwoTestResult);
                    this.form.get('finalResult').setValue(htsResults[0].finalResult);

                    this.form.get('finalResultsRemarks').setValue(htsResults[0].encounterResultRemarks);
                }

                if (encounter.length > 0) {
                    this.form.get('finalResultGiven').setValue(encounter[0].finalResultGiven);
                    // testedAs
                    if (this.isCoupleDiscordantDisabled) {
                        this.form.get('coupleDiscordant').disable({ onlySelf: true });
                    }
                    this.form.get('coupleDiscordant').setValue(htsResults[0].coupleDiscordant);
                }

                if (consentToListPartners.length > 0) {
                    this.form.get('acceptedPartnerListing').setValue(consentToListPartners[0].consentValue);
                    this.form.get('reasonsDeclinePartnerListing').setValue(consentToListPartners[0].declineReason);
                }
            }
        );
    }

    onSubmit() {
        // console.log(this.form.value);
        const coupleDiscordant = this.form.value["coupleDiscordant"] != undefined ? this.form.value["coupleDiscordant"] : "";
        const finalResultGiven = this.form.value["finalResultGiven"] != undefined ? this.form.value["finalResultGiven"] : "";
        const finalResultHiv1 = this.form.value["finalResultHiv1"] != undefined ? this.form.value["finalResultHiv1"] : "";
        const finalResultHiv2 = this.form.value["finalResultHiv2"] != undefined ? this.form.value["finalResultHiv2"] : "";
        const finalResult = this.form.value["finalResult"] != undefined ? this.form.value["finalResult"] : "";
        const finalResultsRemarks = this.form.value["finalResultsRemarks"];
        const acceptedPartnerListing = this.form.value["acceptedPartnerListing"] != undefined ? this.form.value["acceptedPartnerListing"] : "";
        const reasonsDeclinePartnerListing = this.form.value["reasonsDeclinePartnerListing"] != undefined ? this.form.value["reasonsDeclinePartnerListing"] : "";
        const userId = JSON.parse(localStorage.getItem('appUserId'));

        this._encounterService.updateTesting(this.patientId, this.patientMasterVisitId, userId, 2, this.htsEncounterId, coupleDiscordant,
            finalResultGiven, finalResultHiv1, finalResultHiv2, finalResult, acceptedPartnerListing,
            reasonsDeclinePartnerListing, finalResultsRemarks).subscribe(
                (result) => {
                    this.snotifyService.success('Successfully edited testing', 'Testing', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
                },
                (error) => {
                    this.snotifyService.error('Error editing encounter' + error, 'Testing', this.notificationService.getConfig());
                }
            );
    }

    onAcceptedToListPartnersChange(event) {
        if (event.isUserInput && event.source.selected && (event.source.viewValue == 'Yes' || event.source.viewValue == 'N/A')) {
            this.form.get('reasonsDeclinePartnerListing').disable({ onlySelf: false });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.form.get('reasonsDeclinePartnerListing').enable({ onlySelf: false });
            this.form.get('reasonsDeclinePartnerListing').setValue('');
        }
    }

    onHivResultsChange(event) {
        if (event.isUserInput && event.source.selected) {
            const resultVal = this.hivResultsOptions.filter(obj => obj.itemId == event.source.value);
            if (resultVal.length > 0) {
                // console.log(finalResultHiv1);
                if (resultVal[0].itemName == 'Invalid') {
                    this.snotifyService.info('You should not update a final result to Invalid.', 'Testing', this.notificationService.getConfig());
                    return;
                }
            }
            // console.log(this.form.get('finalResultHiv1').value);
            // console.log(this.form.get('finalResultHiv2').value);
        }
    }
}
