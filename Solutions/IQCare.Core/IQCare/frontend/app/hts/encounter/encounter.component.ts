import { Component, OnInit } from '@angular/core';
import {Encounter} from '../_models/encounter';
import {FormGroup} from '@angular/forms';
import {EncounterService} from '../_services/encounter.service';
import {FinalTestingResults, Testing} from '../_models/testing';
import {tokenReference} from '@angular/compiler';

declare var $: any;

@Component({
  selector: 'app-encounter',
  templateUrl: './encounter.component.html',
  styleUrls: ['./encounter.component.css']
})
export class EncounterComponent implements OnInit {
    encounter: Encounter;
    testing: Testing;
    hivResults1: Testing[];
    hivResults2: Testing[];
    finalTestingResults: FinalTestingResults;

    testButton1: boolean = true;
    testButton2: boolean = false;
    isDisabled: boolean = false;
    isNoOfMonths: boolean = true;
    isDisabilitiesEnabled: boolean = true;
    isFinalResultDisabled: boolean = false;
    isFinalResultGivenDisabled: boolean = false;
    isCoupleDiscordantDisabled: boolean = false;
    isAcceptedPartnerListingDisabled: boolean = false;
    isReasonsDeclinedListingDisabled: boolean = false;

    entryPoints: any[];
    yesNoOptions: any[];
    disabilities: any[];
    testedAs: any[];
    strategyOptions: any[];
    tbStatus: any[];
    reasonsDeclined: any[];
    hivResultsOptions: any[];
    hivFinalResultsOptions: any[];
    hivTestKits: any[];


    constructor(private _encounterService: EncounterService) {
    }

    ngOnInit() {
        this.encounter = new Encounter();
        this.testing = new Testing();
        this.finalTestingResults = new FinalTestingResults();

        this.hivResults1 = [];
        this.hivResults2 = [];



        this.getHtsOptions();

        const self = this;

        setTimeout(() => {
            $('#myWizard').on('actionclicked.fu.wizard', function(evt, data) {
                var currentStep = data.step;
                var nextStep = 0;
                var previousStep = 0;

                if (data.direction === 'next') {
                    nextStep = currentStep += 1;
                } else {
                    previousStep = nextStep -= 1;
                }

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

    getHtsOptions() {
        this._encounterService.getHtsEncounterOptions().subscribe(options => {
            for (let i = 0; i < options.length; i++) {
                // console.log(options[i]);
                if (options[i].key == 'HTSEntryPoints') {
                    this.entryPoints = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'Disabilities') {
                    this.disabilities = options[i].value;
                } else if (options[i].key == 'TestedAs') {
                    this.testedAs = options[i].value;
                } else if (options[i].key == 'Strategy') {
                    this.strategyOptions = options[i].value;
                } else if (options[i].key == 'TBStatus') {
                    this.tbStatus = options[i].value;
                } else if (options[i].key == 'ReasonsPartner') {
                    this.reasonsDeclined = options[i].value;
                } else if (options[i].key == 'HIVResults') {
                    this.hivResultsOptions = options[i].value;
                } else if (options[i].key == 'HIVTestKits') {
                    this.hivTestKits = options[i].value;
                } else if (options[i].key == 'HIVFinalResults') {
                    this.hivFinalResultsOptions = options[i].value;
                }
            }
        });
    }

    onSubmitForm() {
        // console.log('Try submit');
        this._encounterService.addEncounter(this.encounter, this.finalTestingResults,
            this.hivResults1, this.hivResults2).subscribe(data => {
            console.log(data);
        }, err => {
            console.log(err);
        });
    }

    onAddingTestResult1() {
        // console.log(this.testing);
        /* Push results to hiv results array */
        this.testing.KitId = this.testing.kitName.itemId;
        this.testing.Outcome = this.testing.hivResultTest.itemId;
        this.testing.TestRound = 1;
        this.hivResults1.push(this.testing);

        if (this.testing.hivResultTest.itemName === 'Negative') {
            this.testButton1 = false;
            this.testButton2 = false;
            this.finalTestingResults.finalResultHiv1 = this.testing.hivResultTest.itemId;
            this.isDisabled = true;
            this.finalTestingResults.finalResult = this.testing.hivResultTest.itemId;
        } else if (this.testing.hivResultTest.itemName === 'Positive') {
            this.testButton1 = false;
            this.testButton2 = true;
            this.finalTestingResults.finalResultHiv1 = this.testing.hivResultTest.itemId;
        }
        /* re-set the model */
        this.testing = new Testing();
        /*Hide the modal after saving*/
        $('#myModal1').modal('hide');
    }

    onAddingTestResult2() {
        const firstTest = this.hivResults1.slice(-1)[0];
        // console.log(firstTest);

        /* Push results to hiv results array */
        this.testing.KitId = this.testing.kitName.itemId;
        this.testing.Outcome = this.testing.hivResultTest.itemId;
        this.testing.TestRound = 2;
        this.hivResults2.push(this.testing);
        /* Get inconclusive value from array */
        const inconculusive = this.hivFinalResultsOptions.filter(function( obj ) {
            return obj.itemName == 'Inconclusive';
        });

        /* Logic for testing */
        if (firstTest.hivResultTest.itemName === 'Positive' && this.testing.hivResultTest.itemName === 'Negative') {
            this.finalTestingResults.finalResultHiv2 = this.testing.hivResultTest.itemId;
            this.finalTestingResults.finalResult = inconculusive[0].itemId;
            this.testButton2 = false;
        } else if (firstTest.hivResultTest.itemName === 'Positive' && this.testing.hivResultTest.itemName === 'Positive') {
            this.finalTestingResults.finalResultHiv2 = this.testing.hivResultTest.itemId;
            this.finalTestingResults.finalResult = this.testing.hivResultTest.itemId;
            this.testButton2 = false;
        }
        /* re-set the model */
        this.testing = new Testing();
        /*Hide the modal after saving*/
        $('#myModal2').modal('hide');
    }

    everTestedChanged(everTested: number) {
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == everTested;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.isNoOfMonths = false;
        } else {
            this.isNoOfMonths = true;
            this.encounter.MonthsSinceLastTest = null;
        }
    }

    hasDisabilityChanged(hasDisability: number) {
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == hasDisability;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.isDisabilitiesEnabled = false;
        } else {
            this.isDisabilitiesEnabled = true;
            this.encounter.Disabilities = [];
        }
    }

    onAcceptedPartnerListingChange(acceptedPartnerListing: number) {
        const optionSelected = this.yesNoOptions.filter(function( obj ) {
            return obj.itemId == acceptedPartnerListing;
        });

        if (optionSelected[0].itemName == 'Yes') {
            this.isReasonsDeclinedListingDisabled = true;
        } else {
            this.isReasonsDeclinedListingDisabled = false;
            this.finalTestingResults.reasonsDeclinePartnerListing = null;
        }
    }

    onSecondProviderSelected(selectedOption: number) {
        if (selectedOption == 1) {
            this.isFinalResultDisabled = true;
            this.isFinalResultGivenDisabled = true;
            this.isCoupleDiscordantDisabled = true;
            this.isAcceptedPartnerListingDisabled = true;
            this.isReasonsDeclinedListingDisabled = true;
            this.isDisabled = true;
            this.testButton2 = false;
        } else {
            this.isFinalResultDisabled = false;
            this.isFinalResultGivenDisabled = false;
            this.isCoupleDiscordantDisabled = false;
            this.isAcceptedPartnerListingDisabled = false;
            this.isReasonsDeclinedListingDisabled = false;
            this.isDisabled = false;
            this.testButton2 = true;
        }
    }
}
