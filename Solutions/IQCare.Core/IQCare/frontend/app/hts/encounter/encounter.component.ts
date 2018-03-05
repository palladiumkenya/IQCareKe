import { Component, Input, OnInit } from '@angular/core';
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
    testButton2: boolean = true;

    entryPoints: any[];
    yesNoOptions: any[];
    disabilities: any[];
    testedAs: any[];
    strategyOptions: any[];
    tbStatus: any[];


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
            $("#myWizard").on("actionclicked.fu.wizard", function(evt, data) {
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
                        if ($('#datastep1').parsley().validate()) {
                            // validated
                            self.onSubmitForm();
                        } else {
                            evt.preventDefault();
                            return;
                        }
                    }
                } else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        if ($('#datastep2').parsley().validate()) {
                            /* submit all forms */
                            self.onSubmitForm();
                        } else {
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
                }
            }
        });
    }

    onSubmitForm() {
        console.log('Try submit');
        this._encounterService.addEncounter(this.encounter, this.testing).subscribe(data => {
                console.log(data);
            }, err => {
                console.log(err);
            });
    }

    onAddingTestResult1() {
        /*console.log(this.testing);*/
        /* Push results to hiv results array */
        this.hivResults1.push(this.testing);

        if (this.testing.hivResultTest === 'Negative') {
            this.testButton1 = false;
            this.testButton2 = false;
            this.finalTestingResults.finalResultHiv1 = 'Negative';
            this.finalTestingResults.finalResult = 'Negative';
        } else if (this.testing.hivResultTest === 'Positive') {
            this.testButton1 = false;
            this.finalTestingResults.finalResultHiv1 = 'Positive';
        }
        /* re-set the model */
        this.testing = new Testing();
        /*Hide the modal after saving*/
        $('#myModal1').modal('hide');
    }

    onAddingTestResult2() {
        const firstTest = this.hivResults1.slice(-1)[0];
        console.log(firstTest);

        /* Push results to hiv results array */
        this.hivResults2.push(this.testing);
        /* Logic for testing */
        if (firstTest.hivResultTest === 'Positive' && this.testing.hivResultTest === 'Negative') {
            this.finalTestingResults.finalResultHiv2 = 'Negative';
            this.finalTestingResults.finalResult = 'Inconclusive';
            this.testButton2 = false;
        } else if (firstTest.hivResultTest === 'Positive' && this.testing.hivResultTest === 'Positive') {
            this.finalTestingResults.finalResultHiv2 = 'Positive';
            this.finalTestingResults.finalResult = 'Positive';
            this.testButton2 = false;
        }
        /* re-set the model */
        this.testing = new Testing();
        /*Hide the modal after saving*/
        $('#myModal2').modal('hide');
    }
}
