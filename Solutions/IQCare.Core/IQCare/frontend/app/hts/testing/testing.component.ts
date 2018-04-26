import {AfterViewInit, Component, NgZone, OnInit} from '@angular/core';
import {MatDialog, MatDialogConfig} from '@angular/material';
import {TestDialogComponent} from '../testdialog/testdialog.component';
import {EncounterService} from '../_services/encounter.service';
import {FinalTestingResults, Testing} from '../_models/testing';
import {select, Store} from '@ngrx/store';
import {ActivatedRoute, Router} from '@angular/router';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {AppStateService} from '../../shared/_services/appstate.service';
import {AppEnum} from '../../shared/reducers/app.enum';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit, AfterViewInit {
    testButton1: boolean = true;
    testButton2: boolean = false;
    isDisabled: boolean = false;
    isCoupleDiscordantDisabled: boolean = false;

    formTesting: FormGroup;

    hivTestKits: any[];
    hivResultsOptions: any[];
    hivFinalResultsOptions: any[];
    yesNoOptions: any[];
    reasonsDeclined: any[];

    testing: Testing;
    hivResults1: Testing[];
    hivResults2: Testing[];
    hiv1: Testing[];
    hiv2: Testing[];

    finalTestingResults: FinalTestingResults;

    constructor(private dialog: MatDialog,
                private encounterService: EncounterService,
                private store: Store<AppState>,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private snotifyService: SnotifyService,
                private notificationService: NotificationService,
                private fb: FormBuilder,
                private appStateService: AppStateService) {
        this.store.pipe(select('app')).subscribe(res => {
            this.isCoupleDiscordantDisabled = res['testedAs'];
        });
    }

    ngAfterViewInit() {
    }

    ngOnInit() {
        this.finalTestingResults = new FinalTestingResults();
        this.testing = new Testing();

        this.hivResults1 = [];
        this.hivResults2 = [];

        this.hiv1 = [];
        this.hiv2 = [];

        this.formTesting = this.fb.group({
            finalResultHiv1: new FormControl(this.finalTestingResults.finalResultHiv1, [Validators.required]),
            finalResultHiv2: new FormControl(this.finalTestingResults.finalResultHiv2, [Validators.required]),
            finalResult: new FormControl(this.finalTestingResults.finalResult, [Validators.required]),
            finalResultGiven: new FormControl(this.finalTestingResults.finalResultGiven, [Validators.required]),
            coupleDiscordant: new FormControl(this.finalTestingResults.coupleDiscordant, [Validators.required]),
            acceptedPartnerListing: new FormControl(this.finalTestingResults.acceptedPartnerListing, [Validators.required]),
            reasonsDeclinePartnerListing: new FormControl(this.finalTestingResults.reasonsDeclinePartnerListing, [Validators.required]),
            finalResultsRemarks: new FormControl(this.finalTestingResults.finalResultsRemarks,[Validators.required]),
        });

        this.encounterService.getCustomOptions().subscribe(data => {
            const options = data['lookupItems'];

            for (let i = 0; i < options.length; i++) {
                if (options[i].key == 'HIVResults') {
                    this.hivResultsOptions = options[i].value;
                } else if (options[i].key == 'HIVTestKits') {
                    this.hivTestKits = options[i].value;
                } else if (options[i].key == 'HIVFinalResults') {
                    this.hivFinalResultsOptions = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'ReasonsPartner') {
                    this.reasonsDeclined = options[i].value;
                }
            }
        });
    }

    openDialog(screeningType: string) {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose =  true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '75%';
        dialogConfig.width = '60%';

        dialogConfig.data = {
            screeningType: screeningType,
            hivTestKits: this.hivTestKits,
            hivResultsOptions: this.hivResultsOptions
        };

        const dialogRef = this.dialog.open(TestDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {

                if (!data) {
                    return;
                }

                if (screeningType == 'Screening Test') {
                    // console.log(data['kitName']);
                    /* Push results to hiv results array */
                    this.testing = data;
                    this.testing.KitId = data['kitName']['itemId'];
                    this.testing.Outcome = data['hivResult']['itemId'];
                    this.testing.TestRound = 1;

                    // Set object variables
                    const test = new Testing();
                    test.KitLotNumber = data['lotNumber'];
                    test.ExpiryDate = data['expiryDate'];
                    test.KitId = this.testing.KitId;
                    test.Outcome = this.testing.Outcome;
                    test.TestRound = this.testing.TestRound;

                    this.hivResults1.push(this.testing);
                    this.hiv1.push(test);

                    if (this.testing['hivResult']['itemName'] === 'Negative') {
                        console.log('testing');
                        this.testButton1 = false;
                        this.testButton2 = false;
                        this.formTesting.controls.finalResultHiv1.setValue(this.testing['hivResult']['itemId']);
                        // this.finalTestingResults.finalResultHiv1 = this.testing['hivResult']['itemId'];
                        this.isDisabled = true;
                        this.formTesting.controls.finalResult.setValue(this.testing['hivResult']['itemId']);
                        this.formTesting.controls.finalResultHiv2.disable({onlySelf: true});
                        // this.finalTestingResults.finalResult = this.testing['hivResult']['itemId'];
                    } else if (this.testing['hivResult']['itemName'] === 'Positive') {
                        this.testButton1 = false;
                        this.testButton2 = true;
                        // this.finalTestingResults.finalResultHiv1 = this.testing['hivResult']['itemId'];
                        this.formTesting.controls.finalResultHiv1.setValue(this.testing['hivResult']['itemId']);
                        this.formTesting.controls.finalResultHiv2.enable({onlySelf: false});
                        this.formTesting.controls.finalResultHiv2.setValue('');
                    }
                    /* re-set the model */
                    this.testing = new Testing();

                } else if (screeningType == 'Confirmatory Test') {
                    const firstTest = this.hivResults1.slice(-1)[0];
                    console.log(firstTest);

                    /* Push results to hiv results array */
                    this.testing = data;
                    this.testing.KitId = data['kitName']['itemId'];
                    this.testing.Outcome = data['hivResult']['itemId'];
                    this.testing.TestRound = 2;

                    // Set object variables
                    const test = new Testing();
                    test.KitLotNumber = data['lotNumber'];
                    test.ExpiryDate = data['expiryDate'];
                    test.KitId = this.testing.KitId;
                    test.Outcome = this.testing.Outcome;
                    test.TestRound = this.testing.TestRound;

                    this.hivResults2.push(this.testing);
                    this.hiv2.push(test);

                    /* Get inconclusive value from array */
                    console.log(this.hivFinalResultsOptions);
                    const inconculusive = this.hivFinalResultsOptions.filter(function( obj ) {
                        return obj.itemName == 'Inconclusive';
                    });

                    /* Logic for testing */
                    if (firstTest['hivResult']['itemName'] === 'Positive' && this.testing['hivResult']['itemName'] === 'Negative') {
                        // this.finalTestingResults.finalResultHiv2 = this.testing['hivResult']['itemId'];
                        this.formTesting.controls.finalResultHiv2.setValue(this.testing['hivResult']['itemId']);
                        // this.finalTestingResults.finalResult = inconculusive[0].itemId;
                        this.formTesting.controls.finalResult.setValue(inconculusive[0].itemId);
                        this.testButton2 = false;
                    } else if (firstTest['hivResult']['itemName'] === 'Positive' && this.testing['hivResult']['itemName'] === 'Positive') {
                        // this.finalTestingResults.finalResultHiv2 = this.testing['hivResult']['itemId'];
                        this.formTesting.controls.finalResultHiv2.setValue(this.testing['hivResult']['itemId']);
                        // this.finalTestingResults.finalResult = this.testing['hivResult']['itemId'];
                        this.formTesting.controls.finalResult.setValue(this.testing['hivResult']['itemId']);
                        this.testButton2 = false;
                    }
                    /* re-set the model */
                    this.testing = new Testing();
                }
            }
        );
    }

    onSubmit() {
        console.log(this.formTesting);
        if (this.formTesting.valid) {
            this.finalTestingResults = {...this.finalTestingResults, ...this.formTesting.value};
            console.log(this.finalTestingResults);

            const htsEncounterId = JSON.parse(localStorage.getItem('htsEncounterId'));
            const patientId = JSON.parse(localStorage.getItem('patientId'));
            const patientMasterVisitId = JSON.parse(localStorage.getItem('patientMasterVisitId'));
            const serviceAreaId = JSON.parse(localStorage.getItem('serviceAreaId'));
            const providerId = JSON.parse(localStorage.getItem('appUserId'));
            const finalRes = this.finalTestingResults.finalResult;
            const acceptList = this.finalTestingResults.acceptedPartnerListing;

            this.encounterService.addTesting(this.finalTestingResults, this.hiv1, this.hiv2,
                htsEncounterId, providerId, patientId, patientMasterVisitId, serviceAreaId).subscribe(data => {

                    const options = this.hivFinalResultsOptions.filter(function( obj ) {
                        return obj.itemId == finalRes;
                    });

                    const acceptListOption = this.yesNoOptions.filter(function (obj) {
                        return obj.itemId == acceptList;
                    });

                    if (options[0]['itemName'] == 'Positive') {
                        this.appStateService.addAppState(AppEnum.IS_POSITIVE, JSON.parse(localStorage.getItem('personId')),
                            patientId, patientMasterVisitId, htsEncounterId).subscribe();
                    }

                    if (acceptListOption[0]['itemName'] == 'Yes') {
                        this.appStateService.addAppState(AppEnum.CONSENT_PARTNER_LISTING, JSON.parse(localStorage.getItem('personId')),
                            patientId, patientMasterVisitId, htsEncounterId).subscribe();
                    }

                    this.snotifyService.success('Successfully saved', 'Testing', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/registration/home'], {relativeTo: this.route }); });
            });
        } else {
            console.log(this.formTesting);
            return false;
        }

    }

    onFinalResultsGivenChange() {
        if (this.isCoupleDiscordantDisabled) {
            console.log('hehe');
            this.formTesting.controls.coupleDiscordant.disable({onlySelf: true});
        } else {
            this.formTesting.controls.coupleDiscordant.enable({onlySelf: false});
            this.formTesting.controls.coupleDiscordant.setValue('');
            console.log('haha');
        }
    }

    onAcceptedPartnerListingChange() {
        const acceptedPartnerListing = this.formTesting.controls.acceptedPartnerListing.value;
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == acceptedPartnerListing;
        });

        if (optionSelected[0].itemName == 'Yes') {
            // this.isReasonsDeclinedListingDisabled = true;
            this.formTesting.controls.reasonsDeclinePartnerListing.disable({onlySelf: true});
        } else {
            // this.isReasonsDeclinedListingDisabled = false;
            // this.finalTestingResults.reasonsDeclinePartnerListing = null;
            this.formTesting.controls.reasonsDeclinePartnerListing.enable({onlySelf: false});
            this.formTesting.controls.reasonsDeclinePartnerListing.setValue('');
        }
    }
}
