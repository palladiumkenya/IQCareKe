import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, NgZone, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { TestDialogComponent } from '../testdialog/testdialog.component';
import { EncounterService } from '../_services/encounter.service';
import { FinalTestingResults, Testing } from '../_models/testing';
import { select, Store } from '@ngrx/store';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AppStateService } from '../../shared/_services/appstate.service';
import { AppEnum } from '../../shared/reducers/app.enum';
import { ClientService } from '../../shared/_services/client.service';

@Component({
    selector: 'app-testing',
    templateUrl: './testing.component.html',
    styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit {
    testButton1: boolean = true;
    testButton2: boolean = false;
    isDisabled: boolean = false;
    isCoupleDiscordantDisabled: boolean = false;

    formTesting: FormGroup;

    hivTestKits: LookupItemView[];
    hivResultsOptions: any[];
    hivFinalResultsOptions: any[];
    yesNoOptions: any[];
    yesNoNA: any[];
    reasonsDeclined: any[];

    testing: Testing;
    hivResults1: Testing[];
    hivResults2: Testing[];
    hiv1: Testing[];
    hiv2: Testing[];

    // other kit
    otherLotNumber: string;
    otherKitexpiryDate: Date;
    // determine
    determineLotNumber: string;
    determineKitexpiryDate: Date;
    // first response
    firstResponseLotNumber: string;
    firstResponseKitexpiryDate: Date;

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
        private appStateService: AppStateService,
        private clientService: ClientService) {
        this.store.pipe(select('app')).subscribe(res => {
            this.isCoupleDiscordantDisabled = res['testedAs'];
        });
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
            finalResultsRemarks: new FormControl(this.finalTestingResults.finalResultsRemarks)
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
                } else if (options[i].key == 'YesNoNA') {
                    this.yesNoNA = options[i].value;
                } else if (options[i].key == 'ReasonsPartner') {
                    this.reasonsDeclined = options[i].value;
                }
            }

            const optionSelected = this.yesNoNA.filter(function (obj) {
                return obj.itemName == 'N/A';
            });

            this.clientService.getClientDetails(JSON.parse(localStorage.getItem('patientId')), 2).subscribe(res => {
                if (this.getAge(res['patientLookup'][0]['dateOfBirth']) < 15) {
                    this.formTesting.controls.acceptedPartnerListing.disable({ onlySelf: true });
                    this.formTesting.controls.acceptedPartnerListing.setValue(optionSelected[0]['itemId']);
                    this.formTesting.controls.reasonsDeclinePartnerListing.disable({ onlySelf: true });
                }
            });

            const otherKit = this.hivTestKits.filter(obj => obj.itemName == 'Other');
            const determineKit = this.hivTestKits.filter(obj => obj.itemName == 'Determine');
            const firstResponseKit = this.hivTestKits.filter(obj => obj.itemName == 'First Response');

            this.getFirstResponseLastUsed(firstResponseKit[0].itemId);

            this.getDetermineLastUsed(determineKit[0].itemId);

            this.getOtherKitLastUsed(otherKit[0].itemId);
        });
    }

    getOtherKitLastUsed(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.otherLotNumber = res.kitLotNumber;
                    this.otherKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    getDetermineLastUsed(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.determineLotNumber = res.kitLotNumber;
                    this.determineKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    getFirstResponseLastUsed(kitId: number) {
        this.encounterService.getLastUsedKit(kitId).subscribe(
            (res) => {
                if (res) {
                    this.firstResponseLotNumber = res.kitLotNumber;
                    this.firstResponseKitexpiryDate = res.expiryDate;
                }
            }
        );
    }

    getAge(dateString) {
        const today = new Date();
        const birthDate = new Date(dateString);
        let age = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }

    openDialog(screeningType: string) {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '75%';
        dialogConfig.width = '60%';

        dialogConfig.data = {
            screeningType: screeningType,
            hivTestKits: this.hivTestKits,
            hivResultsOptions: this.hivResultsOptions,

            otherLotNumber: this.otherLotNumber,
            determineLotNumber: this.determineLotNumber,
            firstResponseLotNumber: this.firstResponseLotNumber,

            otherKitexpiryDate: this.otherKitexpiryDate,
            determineKitexpiryDate: this.determineKitexpiryDate,
            firstResponseKitexpiryDate: this.firstResponseKitexpiryDate
        };

        const dialogRef = this.dialog.open(TestDialogComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {

                if (!data) {
                    return;
                }

                /* Get inconclusive value from array */
                const inconculusive = this.hivFinalResultsOptions.filter(function (obj) {
                    return obj.itemName == 'Inconclusive';
                });

                if (screeningType == 'Screening Test') {
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
                        this.formTesting.controls.finalResultHiv1.setValue(this.testing['hivResult']['itemId']);
                        this.testButton1 = false;

                        /* Check if the screening test was deleted and confirmatory test was not deleted */
                        if (this.formTesting.controls.finalResultHiv2.value) {
                            const confirmatoryTestPreviousValue = this.hivFinalResultsOptions.filter((obj) => {
                                return obj.itemId ==
                                    this.formTesting.controls.finalResultHiv2.value;
                            });

                            if (confirmatoryTestPreviousValue[0]['itemName'] == 'Positive') {
                                this.formTesting.controls.finalResult.setValue(inconculusive[0].itemId);
                            } else if (confirmatoryTestPreviousValue[0]['itemName'] == 'Negative') {
                                this.formTesting.controls.finalResult.setValue(this.testing['hivResult']['itemId']);
                            }
                        } else {
                            this.testButton2 = false;

                            this.isDisabled = true;
                            this.formTesting.controls.finalResult.setValue(this.testing['hivResult']['itemId']);
                            this.formTesting.controls.finalResultHiv2.disable({ onlySelf: true });
                        }
                    } else if (this.testing['hivResult']['itemName'] === 'Positive') {
                        this.formTesting.controls.finalResultHiv1.setValue(this.testing['hivResult']['itemId']);
                        this.testButton1 = false;

                        /* Check if the confirmatory record has not been deleted */
                        if (this.formTesting.controls.finalResultHiv2.value) {
                            const confirmatoryTestPreviousValue = this.hivFinalResultsOptions.filter((obj) => {
                                return obj.itemId ==
                                    this.formTesting.controls.finalResultHiv2.value;
                            });

                            if (confirmatoryTestPreviousValue[0]['itemName'] == 'Positive') {
                                this.formTesting.controls.finalResult.setValue(this.testing['hivResult']['itemId']);
                            } else if (confirmatoryTestPreviousValue[0]['itemName'] == 'Negative') {
                                this.formTesting.controls.finalResult.setValue(inconculusive[0].itemId);
                            }
                        } else {
                            this.testButton2 = true;

                            this.formTesting.controls.finalResultHiv2.enable({ onlySelf: false });
                            this.formTesting.controls.finalResultHiv2.setValue('');
                        }
                    }
                    /* re-set the model */
                    this.testing = new Testing();

                } else if (screeningType == 'Confirmatory Test') {
                    const firstTest = this.hivResults1.slice(-1)[0];

                    console.log(firstTest);
                    console.log(data);

                    if (firstTest.kitName['itemName'] == data.kitName['itemName']) {
                        console.log('test');
                        this.snotifyService.info('The same kitname has been used for screening and confirmatory test.' +
                            'Please select another kitname.', 'Testing', this.notificationService.getConfig());
                        return;
                    }
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

                    /* Logic for testing */
                    if (firstTest['hivResult']['itemName'] === 'Positive' && this.testing['hivResult']['itemName'] === 'Negative') {
                        this.formTesting.controls.finalResultHiv2.setValue(this.testing['hivResult']['itemId']);
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
        if (!this.formTesting.controls.finalResultsRemarks.value || this.formTesting.controls.finalResultsRemarks.value == ' ') {
            this.formTesting.controls.finalResultsRemarks.setValue('n/a');
        }

        console.log(this.formTesting);
        if (this.formTesting.valid) {
            this.finalTestingResults = { ...this.finalTestingResults, ...this.formTesting.value };
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

                    const options = this.hivFinalResultsOptions.filter(function (obj) {
                        return obj.itemId == finalRes;
                    });

                    const acceptListOption = this.yesNoNA.filter(function (obj) {
                        return obj.itemId == acceptList;
                    });

                    if (options[0] !== undefined && options[0]['itemName'] == 'Positive') {
                        this.appStateService.addAppState(AppEnum.IS_POSITIVE, JSON.parse(localStorage.getItem('personId')),
                            patientId, patientMasterVisitId, htsEncounterId).subscribe();
                    }

                    if (acceptListOption[0] !== undefined && acceptListOption[0]['itemName'] == 'Yes') {
                        this.appStateService.addAppState(AppEnum.CONSENT_PARTNER_LISTING, JSON.parse(localStorage.getItem('personId')),
                            patientId, patientMasterVisitId, htsEncounterId).subscribe();
                    }

                    this.snotifyService.success('Successfully saved', 'Testing', this.notificationService.getConfig());
                    this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
                }, (err) => {
                    this.snotifyService.error('Error saving testing ' + err, 'Testing', this.notificationService.getConfig());
                });
        } else {
            return false;
        }

    }

    onFinalResultsGivenChange() {
        if (this.isCoupleDiscordantDisabled) {
            this.formTesting.controls.coupleDiscordant.disable({ onlySelf: true });
        } else {
            this.formTesting.controls.coupleDiscordant.enable({ onlySelf: false });
            this.formTesting.controls.coupleDiscordant.setValue('');
        }
    }

    onAcceptedPartnerListingChange() {
        const acceptedPartnerListing = this.formTesting.controls.acceptedPartnerListing.value;
        const optionSelected = this.yesNoNA.filter(function (obj) {
            return obj.itemId == acceptedPartnerListing;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.formTesting.controls.reasonsDeclinePartnerListing.disable({ onlySelf: true });
        } else if (optionSelected[0].itemName == 'No') {
            this.formTesting.controls.reasonsDeclinePartnerListing.enable({ onlySelf: false });
            this.formTesting.controls.reasonsDeclinePartnerListing.setValue('');
        } else if (optionSelected[0].itemName == 'N/A') {
            this.formTesting.controls.reasonsDeclinePartnerListing.disable({ onlySelf: true });
            this.formTesting.controls.reasonsDeclinePartnerListing.setValue('');
        }
    }

    deleteTest(hivResult: Testing, type: number, index: number, event: any) {
        if (type == 1) {
            const result = this.snotifyService.confirm('Are you sure you want to delete?', 'Testing', {
                closeOnClick: true,
                position: SnotifyPosition.centerCenter,
                buttons: [
                    {
                        text: 'Yes', action: () => {

                            const hiv1Filtered = this.hiv1.filter((obj) => {
                                return obj.KitId !== hivResult.KitId
                                    && obj.KitLotNumber !== hivResult['lotNumber'];
                            });

                            this.hiv1 = hiv1Filtered;
                            this.hivResults1.splice(index, 1);

                            if (hivResult['hivResult']['itemName'] == 'Negative' || hivResult['hivResult']['itemName'] == 'Positive') {
                                this.formTesting.controls.finalResult.setValue('');
                                this.formTesting.controls.finalResultHiv1.setValue('');
                                this.testButton1 = true;
                            }
                        }, bold: false
                    },
                    { text: 'No', action: () => console.log('Clicked: No') }
                ]
            });
        } else {
            const result = this.snotifyService.confirm('Are you sure you want to delete?', 'Testing', {
                closeOnClick: true,
                position: SnotifyPosition.centerCenter,
                buttons: [
                    {
                        text: 'Yes', action: () => {
                            const hiv2Filtered = this.hiv2.filter((obj) => {
                                return obj.KitId !== hivResult.KitId
                                    && obj.KitLotNumber !== hivResult['lotNumber'];
                            });

                            this.hiv2 = hiv2Filtered;
                            this.hivResults2.splice(index, 1);

                            if (hivResult['hivResult']['itemName'] == 'Negative' || hivResult['hivResult']['itemName'] == 'Positive') {
                                this.formTesting.controls.finalResult.setValue('');
                                this.formTesting.controls.finalResultHiv2.setValue('');
                                this.testButton2 = true;
                            }
                        }, bold: false
                    },
                    { text: 'No', action: () => console.log('Clicked: No') }
                ]
            });
        }
    }
}
