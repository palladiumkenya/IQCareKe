"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var encounter_1 = require("../_models/encounter");
var encounter_service_1 = require("../_services/encounter.service");
var testing_1 = require("../_models/testing");
var EncounterComponent = /** @class */ (function () {
    function EncounterComponent(_encounterService) {
        this._encounterService = _encounterService;
        this.testButton1 = true;
        this.testButton2 = true;
    }
    EncounterComponent.prototype.ngOnInit = function () {
        this.encounter = new encounter_1.Encounter();
        this.testing = new testing_1.Testing();
        this.finalTestingResults = new testing_1.FinalTestingResults();
        this.hivResults1 = [];
        this.hivResults2 = [];
        this.getHtsOptions();
        var self = this;
        setTimeout(function () {
            $("#myWizard").on("actionclicked.fu.wizard", function (evt, data) {
                var currentStep = data.step;
                var nextStep = 0;
                var previousStep = 0;
                if (data.direction === 'next') {
                    nextStep = currentStep += 1;
                }
                else {
                    previousStep = nextStep -= 1;
                }
                if (data.step === 1) {
                    if (data.direction === 'previous') {
                        return;
                    }
                    else {
                        if ($('#datastep1').parsley().validate()) {
                            // validated
                            self.onSubmitForm();
                        }
                        else {
                            evt.preventDefault();
                            return;
                        }
                    }
                }
                else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    }
                    else {
                        if ($('#datastep2').parsley().validate()) {
                            /* submit all forms */
                            self.onSubmitForm();
                        }
                        else {
                            evt.preventDefault();
                            return;
                        }
                    }
                }
            })
                .on('changed.fu.wizard', function () { })
                .on('stepclicked.fu.wizard', function () { })
                .on('finished.fu.wizard', function (e) { });
        }, 0);
    };
    EncounterComponent.prototype.getHtsOptions = function () {
        var _this = this;
        this._encounterService.getHtsEncounterOptions().subscribe(function (options) {
            for (var i = 0; i < options.length; i++) {
                // console.log(options[i]);
                if (options[i].key == 'HTSEntryPoints') {
                    _this.entryPoints = options[i].value;
                }
                else if (options[i].key == 'YesNo') {
                    _this.yesNoOptions = options[i].value;
                }
                else if (options[i].key == 'Disabilities') {
                    _this.disabilities = options[i].value;
                }
                else if (options[i].key == 'TestedAs') {
                    _this.testedAs = options[i].value;
                }
                else if (options[i].key == 'Strategy') {
                    _this.strategyOptions = options[i].value;
                }
                else if (options[i].key == 'TBStatus') {
                    _this.tbStatus = options[i].value;
                }
            }
        });
    };
    EncounterComponent.prototype.onSubmitForm = function () {
        console.log('Try submit');
        this._encounterService.addEncounter(this.encounter, this.testing).subscribe(function (data) {
            console.log(data);
        }, function (err) {
            console.log(err);
        });
    };
    EncounterComponent.prototype.onAddingTestResult1 = function () {
        /*console.log(this.testing);*/
        /* Push results to hiv results array */
        this.hivResults1.push(this.testing);
        if (this.testing.hivResultTest === 'Negative') {
            this.testButton1 = false;
            this.testButton2 = false;
            this.finalTestingResults.finalResultHiv1 = 'Negative';
            this.finalTestingResults.finalResult = 'Negative';
        }
        else if (this.testing.hivResultTest === 'Positive') {
            this.testButton1 = false;
            this.finalTestingResults.finalResultHiv1 = 'Positive';
        }
        /* re-set the model */
        this.testing = new testing_1.Testing();
        /*Hide the modal after saving*/
        $('#myModal1').modal('hide');
    };
    EncounterComponent.prototype.onAddingTestResult2 = function () {
        var firstTest = this.hivResults1.slice(-1)[0];
        console.log(firstTest);
        /* Push results to hiv results array */
        this.hivResults2.push(this.testing);
        /* Logic for testing */
        if (firstTest.hivResultTest === 'Positive' && this.testing.hivResultTest === 'Negative') {
            this.finalTestingResults.finalResultHiv2 = 'Negative';
            this.finalTestingResults.finalResult = 'Inconclusive';
            this.testButton2 = false;
        }
        else if (firstTest.hivResultTest === 'Positive' && this.testing.hivResultTest === 'Positive') {
            this.finalTestingResults.finalResultHiv2 = 'Positive';
            this.finalTestingResults.finalResult = 'Positive';
            this.testButton2 = false;
        }
        /* re-set the model */
        this.testing = new testing_1.Testing();
        /*Hide the modal after saving*/
        $('#myModal2').modal('hide');
    };
    EncounterComponent = __decorate([
        core_1.Component({
            selector: 'app-encounter',
            templateUrl: './encounter.component.html',
            styleUrls: ['./encounter.component.css']
        }),
        __metadata("design:paramtypes", [encounter_service_1.EncounterService])
    ], EncounterComponent);
    return EncounterComponent;
}());
exports.EncounterComponent = EncounterComponent;
//# sourceMappingURL=encounter.component.js.map