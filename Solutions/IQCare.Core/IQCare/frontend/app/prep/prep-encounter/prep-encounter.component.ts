import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, OnInit } from '@angular/core';
import { FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-prep-encounter',
    templateUrl: './prep-encounter.component.html',
    styleUrls: ['./prep-encounter.component.css']
})
export class PrepEncounterComponent implements OnInit {
    isLinear: boolean = false;

    // Form Groups
    STIScreeningFormGroup: FormArray;
    CircumcisionStatusFormGroup: FormArray;
    FertilityIntentionsFormGroup: FormArray;

    yesnoOptions: LookupItemView[];
    stiScreeningOptions: LookupItemView[];
    yesNoUnknownOptions: LookupItemView[];
    familyPlanningMethodsOptions: LookupItemView[];
    planningPregnancyOptions: LookupItemView[];
    yesNoDontKnowOptions: LookupItemView[];
    pregnancyOutcomeOptions: LookupItemView[];

    STIScreeningAndTreatmentOptions: any[] = [];
    CircumcisionStatusOptions: any[] = [];
    FertilityIntentionsOptions: any[] = [];
    PregnancyOutcomeOptions: any[] = [];

    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.data.subscribe(
            (res) => {
                const { yesNoOptions, stiScreeningTreatmentOptions, yesNoUnknownOptions,
                    familyPlanningMethodsOptions, planningPregnancyOptions,
                    yesNoDontKnowOptions, pregnancyOutcomeOptions } = res;
                this.yesnoOptions = yesNoOptions['lookupItems'];
                this.stiScreeningOptions = stiScreeningTreatmentOptions['lookupItems'];
                this.yesNoUnknownOptions = yesNoUnknownOptions['lookupItems'];
                this.familyPlanningMethodsOptions = familyPlanningMethodsOptions['lookupItems'];
                this.planningPregnancyOptions = planningPregnancyOptions['lookupItems'];
                this.yesNoDontKnowOptions = yesNoDontKnowOptions['lookupItems'];
                this.pregnancyOutcomeOptions = pregnancyOutcomeOptions['lookupItems'];
            }
        );


        this.STIScreeningAndTreatmentOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'stiScreeningOptions': this.stiScreeningOptions
        });

        this.CircumcisionStatusOptions.push({
            'yesNoUnknownOptions': this.yesNoUnknownOptions,
            'yesnoOptions': this.yesnoOptions,
        });

        this.FertilityIntentionsOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'fpMethods': this.familyPlanningMethodsOptions,
            'planningPregnancy': this.planningPregnancyOptions
        });

        this.PregnancyOutcomeOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'yesNoDontKnowOptions': this.yesNoDontKnowOptions,
            'pregnancyOutcomeOptions': this.pregnancyOutcomeOptions
        });
    }

}
