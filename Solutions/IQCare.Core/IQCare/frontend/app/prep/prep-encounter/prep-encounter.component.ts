import { LookupItemView } from './../../shared/_models/LookupItemView';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-prep-encounter',
    templateUrl: './prep-encounter.component.html',
    styleUrls: ['./prep-encounter.component.css']
})
export class PrepEncounterComponent implements OnInit {
    isLinear: boolean = true;

    // Form Groups
    STIScreeningFormGroup: FormArray;
    CircumcisionStatusFormGroup: FormArray;
    FertilityIntentionsFormGroup: FormArray;
    ChronicIllnessFormGroup: Object[];
    PrepStatusFormGroup: FormArray;
    AppointmentFormGroup: FormArray;

    yesnoOptions: LookupItemView[];
    stiScreeningOptions: LookupItemView[];
    yesNoUnknownOptions: LookupItemView[];
    familyPlanningMethodsOptions: LookupItemView[];
    planningPregnancyOptions: LookupItemView[];
    yesNoDontKnowOptions: LookupItemView[];
    pregnancyOutcomeOptions: LookupItemView[];
    prepContraindicationsOptions: LookupItemView[];
    prepStatusOptions: LookupItemView[];

    STIScreeningAndTreatmentOptions: any[] = [];
    CircumcisionStatusOptions: any[] = [];
    FertilityIntentionsOptions: any[] = [];
    PregnancyOutcomeOptions: any[] = [];
    PrepStatusOptions: any[] = [];

    constructor(private route: ActivatedRoute) {
        this.STIScreeningFormGroup = new FormArray([]);
        this.CircumcisionStatusFormGroup = new FormArray([]);
        this.FertilityIntentionsFormGroup = new FormArray([]);
        this.ChronicIllnessFormGroup = [];
        this.PrepStatusFormGroup = new FormArray([]);
        this.AppointmentFormGroup = new FormArray([]);
    }

    ngOnInit() {
        this.route.data.subscribe(
            (res) => {
                const { yesNoOptions, stiScreeningTreatmentOptions, yesNoUnknownOptions,
                    familyPlanningMethodsOptions, planningPregnancyOptions,
                    yesNoDontKnowOptions, pregnancyOutcomeOptions, prepContraindicationsOptions,
                    prepStatusOptions } = res;
                this.yesnoOptions = yesNoOptions['lookupItems'];
                this.stiScreeningOptions = stiScreeningTreatmentOptions['lookupItems'];
                this.yesNoUnknownOptions = yesNoUnknownOptions['lookupItems'];
                this.familyPlanningMethodsOptions = familyPlanningMethodsOptions['lookupItems'];
                this.planningPregnancyOptions = planningPregnancyOptions['lookupItems'];
                this.yesNoDontKnowOptions = yesNoDontKnowOptions['lookupItems'];
                this.pregnancyOutcomeOptions = pregnancyOutcomeOptions['lookupItems'];
                this.prepStatusOptions = prepStatusOptions['lookupItems'];
                this.prepContraindicationsOptions = prepContraindicationsOptions['lookupItems'];
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

        this.PrepStatusOptions.push({
            'yesnoOptions': this.yesnoOptions,
            'prepStatusOptions': this.prepStatusOptions,
            'prepContraindicationsOptions': this.prepContraindicationsOptions
        });
    }

    onPrepStiScreeningTreatmentNotify(formGroup: FormGroup): void {
        this.STIScreeningFormGroup.push(formGroup);
    }

    onCircumcisionStatusNotify(formGroup: FormGroup): void {
        this.CircumcisionStatusFormGroup.push(formGroup);
    }

    onFertilityIntentionNotify(formGroup: FormGroup): void {
        this.FertilityIntentionsFormGroup.push(formGroup);
    }

    onPregnancyOutcomeNotify(formGroup: FormGroup): void {
        this.FertilityIntentionsFormGroup.push(formGroup);
    }

    onChroniIllnessNotify(chronicIllnesses: any[]): void {
        this.ChronicIllnessFormGroup.push(chronicIllnesses);
    }

    onAllergiesNotify(allergies: any[]): void {
        this.ChronicIllnessFormGroup.push(allergies);
    }

    onAdverseEventsNotify(adverseEvents: any[]): void {
        this.ChronicIllnessFormGroup.push(adverseEvents);
    }

    onPrepStatusNotify(formGroup: FormGroup): void {
        this.PrepStatusFormGroup.push(formGroup);
    }
}
